using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Menu : MonoBehaviour {


	public GameObject title;
	public GameObject instructions;
	public Text[] playerInstruc = new Text[2]; //the "ready?" text
	public Text[] playerScore = new Text[2]; //the actual text
	public GameObject scorePie;
	public GameObject scorePiece;


	//input stuff
	private string myControllerSuffix = "";

	private bool aPressed;
	private bool bPressed;

	AudioSource audioSource;
	[SerializeField] AudioClip marimba;
	[SerializeField] AudioClip bass;

	// Use this for initialization
	void Start () {
		playerScore [1].text = ""+SnakeData.Instance.getScore (0);	
		playerScore [0].text = ""+SnakeData.Instance.getScore (1);		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("SubmitA"+myControllerSuffix)){//Input.GetKeyDown (KeyCode.Q)) { //temp for right trigger
			Debug.Log ("player 1 pressed");
			playerInstruc [0].text = "ready!!";
			if (aPressed == false) {
				aPressed = true;
				CS_AudioManager.Instance.PlaySFX (marimba, Random.Range (0.8f, 1.2f), Random.Range (0.8f, 1.2f));
			}
		}
		if (Input.GetButtonDown("SubmitB"+myControllerSuffix)){//Input.GetKeyDown (KeyCode.P)) { //temp for left trigger
			Debug.Log ("player 2 pressed");
			playerInstruc [1].text = "ready!!";
			if (bPressed == false) {
				bPressed = true;
				CS_AudioManager.Instance.PlaySFX (marimba, Random.Range (0.8f, 1.2f), Random.Range (0.8f, 1.2f));
			}
		}
		if (aPressed && bPressed) {
			Invoke ("startGame", 1.0f);
		}
	}


	void startGame(){
//		title.SetActive(false);
//		instructions.SetActive(false);
//		playerInstruc [0].text = "0";
//		playerInstruc [1].text = "0";
		SceneManager.LoadScene("Game");
	}
}
