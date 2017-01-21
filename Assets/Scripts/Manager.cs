using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {

	public int numPlayers = 2; //how many people are playing
	public Color[] playerColors = new Color[2];
	public int[] score;
	public int totalScore;

	GameObject snake;
	public GameObject basket;

	//ui stuff
	int mode; //0=start screen; 1=game; 2=end screen
	public GameObject backgroundKeyboard;
	public GameObject title;
	public GameObject instructions;
	public GameObject scorePiece;
	public Text time;

	//time stuff
	int timeMax = 60; //in seconds
	float timeLeft;

	//ball stuff
	public GameObject ballSample;
	public Vector3 ballStartPosition;
	GameObject[] currentBalls;

	//input stuff
	public GameObject[] keys = new GameObject[10]; //for now
	int[] keyNum;// = new int[5]; //keeps track of the letters (since it doesn't go in order)

	// Use this for initialization
	void Start () {
		backgroundKeyboard.SetActive(false);
		snake = GameObject.Find ("Snake");;
		timeLeft = (float)timeMax;
		keyNum = new int[10]{1, 19, 4, 6, 7,8,10,11,12,-37};
		currentBalls = new GameObject[numPlayers];
		totalScore = 0;
		score = new int[numPlayers];
		for (int i=0; i<numPlayers; i++){
			score[i]=0;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (mode == 0) { //start
			if (Input.GetKeyDown (KeyCode.Space)) {
				mode = 1; //start game
				backgroundKeyboard.SetActive(false);
				title.SetActive(false);
				instructions.SetActive(false);
				basket.SetActive (true);
				for (int i = 0; i < numPlayers; i++) {
					makeBall (i);
				}
			}
		} else if (mode == 1) { //gameplay
			for (int i = 0; i < keys.Length; i++) {
				if (Input.GetKeyDown (KeyCode.A + keyNum[i]-1)) {
					//				keys [i].transform.localScale = new Vector3 (1.2f, 1.2f, 1f);
//					keys [i].GetComponent<Rigidbody2D> ().AddForce (new Vector2(0f,20000f));
					snake.GetComponent<CS_Snake>().PullSpring(i);
				}
				if (Input.GetKeyUp (KeyCode.A + keyNum[i]-1)) {
//					keys [i].transform.localScale = new Vector3 (1f, 1f, 1f);

					snake.GetComponent<CS_Snake>().ReleaseSpring(i);
				}
			}
			timeLeft -= Time.deltaTime;
			if (timeLeft < 10) {
				time.text = "0:0"+(int)timeLeft;
			} else {
				time.text = "0:"+(int)timeLeft;
			}
			if (timeLeft < 0) {
				mode = 2;
			}
		} else if (mode == 2) { //end
			Debug.Log("game end");
		}
	}

	void makeBall(int team){
		currentBalls [team] = Instantiate (ballSample, ballStartPosition + new Vector3(0f,2f*team,0f), Quaternion.identity) as GameObject;
		currentBalls [team].GetComponent<Ball> ().setTeam (team,playerColors[team]);
	}

	//ball fell out of bounds
	public void BallFell(int team){
		Destroy (currentBalls [team]);
		makeBall (team);
	}

	public void scored(int team){
		Destroy (currentBalls [team]);
		makeBall (team);
		score [team] += 1;
		totalScore += 1;
		Debug.Log(scorePiece.transform.eulerAngles.x);
		Debug.Log(scorePiece.transform.eulerAngles.y);
		Debug.Log(scorePiece.transform.eulerAngles.z);
		float targetAngle =  180f * score [0] / totalScore;
		scorePiece.transform.rotation = Quaternion.Euler(0.0f, 0.0f, targetAngle);
	}
}
