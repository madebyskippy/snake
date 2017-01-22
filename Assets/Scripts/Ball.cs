using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	GameObject manager;
	public int team; //which player the ball is for
	AudioSource audioSource;
	private bool justPlayedHit;
	[SerializeField] AudioClip hit1;
	[SerializeField] AudioClip hit2;
	[SerializeField] AudioClip hit3;
	[SerializeField] AudioClip hit4;
	[SerializeField] AudioClip hit5;
	[SerializeField] AudioClip hit6;
	[SerializeField] AudioClip hit7;
	[SerializeField] AudioClip hit8;



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
		GetComponent<TrailRenderer> ().startColor = c;
	}

	void OnCollisionEnter2D(Collision2D collider){
		if (collider.gameObject.tag == "Ground") {
			Debug.Log("hit stuff");
			manager.SendMessage ("BallFell", team);
		}
		if (collider.gameObject.tag == "Basket") {
			if (transform.position.y > collider.gameObject.transform.position.y) {
//				Debug.Log("hit basket");
				manager.SendMessage ("scored", team);

			}
		}
		if (collider.gameObject.tag == "Snake" || collider.gameObject.tag == "Ball") {
			if (justPlayedHit == false) {

//				Camera.main.WorldToViewportPoint (transform.position);

				if (transform.position.x <= -9f) {
					CS_AudioManager.Instance.PlaySFX (hit1);
				}
				else if (transform.position.x <= -6f) {
					CS_AudioManager.Instance.PlaySFX (hit2);
				}
				else if (transform.position.x <= -3f) {
					CS_AudioManager.Instance.PlaySFX (hit3);
				}
				else if (transform.position.x <= 0f) {
					CS_AudioManager.Instance.PlaySFX (hit4);
				}
				else if (transform.position.x <= 3f) {
					CS_AudioManager.Instance.PlaySFX (hit5);
				}
				else if (transform.position.x <= 6f) {
					CS_AudioManager.Instance.PlaySFX (hit6);
				}
				else if (transform.position.x <= 9f) {
					CS_AudioManager.Instance.PlaySFX (hit7);
				}
				else {
					CS_AudioManager.Instance.PlaySFX (hit8);
				}

					justPlayedHit = true;
					audioSource.pitch = Random.Range (1f, 1.3f);
					 
					Invoke ("ResetJustPlayed", 0.5f);
				 
			}
		}
	}

	void ResetJustPlayed () {
		justPlayedHit = false;
	}
}
