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
//	public GameObject backgroundKeyboard;
	public GameObject title;
	public GameObject instructions;
	public Text[] playerInstruc = new Text[2]; //the "ready?" text
	public GameObject scorePie;
	public GameObject scorePiece;
	public Text end;
	public Text time;

	//time stuff
	int timeMax = 5; //in seconds
	float timeLeft;

	//ball stuff
	public GameObject ballSample;
	public Vector3 ballStartPosition;
	GameObject[] currentBalls;

	//input stuff
	public GameObject[] keys = new GameObject[10]; //for now
	int[] keyNum;// = new int[5]; //keeps track of the letters (since it doesn't go in order)
	bool onePlayerPressed;

	// Use this for initialization
	void Start () {
//		backgroundKeyboard.SetActive(false);
		snake = GameObject.Find ("Snake");
		keyNum = new int[10]{1, 19, 4, 6, 7,8,10,11,12,-37};
		currentBalls = new GameObject[numPlayers];
		score = new int[numPlayers];
		reset ();
		timeLeft = 5f;//(float)timeMax;
		onePlayerPressed = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (mode == 0) { //menu state
			if (Input.GetKeyDown (KeyCode.Q)) { //temp for right trigger
				playerInstruc [0].text = "ready!!";
				if (onePlayerPressed) {
					Invoke ("startGame", 1.0f);
				} else {
					onePlayerPressed = true;
				}
			}
			if (Input.GetKeyDown (KeyCode.P)) { //temp for left trigger
				playerInstruc [1].text = "ready!!";
				if (onePlayerPressed) {
					Invoke ("startGame", 1.0f);
				} else {
					onePlayerPressed = true;
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
				mode = 0;
				endGame ();
			}
		}
	}

	void reset(){
		//		end.text = "";
		scorePiece.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 90f);
		timeLeft = (float)timeMax;
		totalScore = 0;
		for (int i=0; i<numPlayers; i++){
			score[i]=0;
		}
		scorePie.transform.localScale = new Vector3 (1.1f, 1.1f, 1.1f);
	}

	void startGame(){
		if (mode != 1) {
			reset ();
			mode = 1; //start game
			//				backgroundKeyboard.SetActive(false);
			title.SetActive(false);
			instructions.SetActive(false);
			basket.SetActive (true);
			for (int i = 0; i < numPlayers; i++) {
				//					for (int j = 0; j < 10; j++) {
				makeBall (i);
				//					}
			}
		}
	}

	void endGame(){
		playerInstruc [0].text = "ready?";
		playerInstruc [1].text = "ready?";
		onePlayerPressed = false;
		basket.SetActive (false);
		instructions.SetActive (true);
		title.SetActive(true);
		scorePie.transform.localScale = new Vector3 (2.5f, 2.5f, 2.5f);
//		end.text = "game over \n \n S P A C E  to play again";
		for (int i = 0; i < currentBalls.Length; i++) {
			Destroy (currentBalls [i]);
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
