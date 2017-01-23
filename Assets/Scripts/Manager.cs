using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Manager : MonoBehaviour {


	public int numPlayers = 2; //how many people are playing
	public Color[] playerColors = new Color[2];
	public int totalScore;

	GameObject snake;
	public GameObject basket;

	//ui stuff
	public Text[] playerInstruc = new Text[2]; //the "ready?" text
	public GameObject scorePie;
	public GameObject scorePiece;
	public Text time;

	[SerializeField] AudioClip marimba;
	[SerializeField] AudioClip bass;

	//time stuff
	int timeMax = 60; //in seconds
	float timeLeft;

	//ball stuff
	public GameObject ballSample;
	public Vector3 ballStartPosition;
	GameObject[] currentBalls;
	public GameObject[] particles = new GameObject[2];

	//input stuff
	private string myControllerSuffix = "";

	private bool aPressed;
	private bool bPressed;

	AudioSource audioSource;



	// Use this for initialization
	void Start () {
//		backgroundKeyboard.SetActive(false);
		snake = GameObject.Find ("Snake");
		currentBalls = new GameObject[numPlayers];
//		score = new int[numPlayers];
		reset ();
		timeLeft = 5f;//(float)timeMax;

		audioSource = GetComponent<AudioSource>();

		if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer) {
			myControllerSuffix += "Mac";
		}

		aPressed = false;
		bPressed = false;

		SnakeData.Instance.resetScore ();

		for (int i = 0; i < numPlayers; i++) {
			makeBall (i);
		}
	}
	
	// Update is called once per frame
	void Update () {
		timeLeft -= Time.deltaTime;
		if (timeLeft < 10) {
			time.text = "0:0"+(int)timeLeft;
		} else {
			time.text = "0:"+(int)timeLeft;
		}
		if (timeLeft < 0) {
			endGame ();
		}
	}

	void reset(){
		playerInstruc [0].text = "0";
		playerInstruc [1].text = "0";
		scorePiece.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 90f);
		timeLeft = (float)timeMax;
		totalScore = 0;
		scorePie.transform.localScale = new Vector3 (1.5f, 1.5f, 1.1f);
	}

	void endGame(){
		SceneManager.LoadScene("Menu");
	}

	void makeBall(int team){
		currentBalls [team] = Instantiate (ballSample, ballStartPosition + new Vector3(12f-12f*team,0f,0f), Quaternion.identity) as GameObject;
		currentBalls [team].GetComponent<Ball> ().setTeam (team,playerColors[team]);
	}

	//ball fell out of bounds
	public void BallFell(int team){
		CS_AudioManager.Instance.PlaySFX (bass, Random.Range (0.8f, 1.2f), Random.Range (0.8f, 1.2f));
		Instantiate (particles [team], currentBalls [team].transform.position, Quaternion.identity);
		Destroy (currentBalls [team]);
		makeBall (team);
	}

	public void scored(int team){
		audioSource.Play ();
		Instantiate (particles [team], currentBalls [team].transform.position, Quaternion.identity);
		Destroy (currentBalls [team]);
		makeBall (team);
//		score [team] += 1;
		SnakeData.Instance.increaseScore (1-team);
		totalScore += 1;
		playerInstruc [team].text = "" + SnakeData.Instance.getScore (team);//score [team];
		float targetAngle =  180f *  SnakeData.Instance.getScore (0) / totalScore;
		scorePiece.transform.rotation = Quaternion.Euler(0.0f, 0.0f, targetAngle);
		Sequence sq = DOTween.Sequence();
		sq.Append(scorePie.transform.DOScale(new Vector2(1.8f, 1.8f), 0.15f));
		sq.Append(scorePie.transform.DOScale(new Vector2(1.1f, 1.1f), 0.1f));
		sq.Append(scorePie.transform.DOScale(new Vector2(1.6f, 1.6f), 0.1f));
		sq.Append(scorePie.transform.DOScale(new Vector2(1.5f, 1.5f), 0.1f));
	}
}
