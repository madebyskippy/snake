using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	GameObject manager;
	public int team; //which player the ball is for
	AudioSource audioSource;
	private bool justPlayedHit;
	[SerializeField] AudioClip hit;


	// Use this for initialization
	void Start () {
		manager = GameObject.Find ("Manager");
		audioSource = GetComponent<AudioSource>();
		justPlayedHit = false;
	}
	
	// Update is called once per frame
	void Update () {


		
	}

	public void setTeam(int t, Color c){
		team = t;
		//set sprite color
		GetComponent<SpriteRenderer>().color = c;
	}

	void OnCollisionEnter2D(Collision2D collider){
		if (collider.gameObject.tag == "Ground") {
			Debug.Log("hit stuff");
			manager.SendMessage ("BallFell", team);
		}
		if (collider.gameObject.tag == "Basket") {
			if (transform.position.y > collider.gameObject.transform.position.y) {
				Debug.Log("hit basket");
				manager.SendMessage ("scored", team);

			}
		}
		if (collider.gameObject.tag == "Snake" || collider.gameObject.tag == "Ball") {
			if (justPlayedHit == false) {
				audioSource.clip = hit;
				justPlayedHit = true;
				audioSource.pitch = Random.Range (1f, 1.3f);
				audioSource.Play (); 
				Invoke("ResetJustPlayed", 0.5f);
			}
		}
	}

	void ResetJustPlayed () {
		justPlayedHit = false;
	}
}
