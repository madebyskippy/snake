using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	GameObject manager;
	public int team; //which player the ball is for

	// Use this for initialization
	void Start () {
		manager = GameObject.Find ("Manager");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setTeam(int t){
		team = t;
		//set sprite color
		GetComponent<SpriteRenderer>().color = new Color(0.2f*(team+1),0.5f*(team+1),0.5f*(team+1));
	}

	void OnCollisionEnter2D(Collision2D collider){
		if (collider.gameObject.tag == "Ground") {
			Debug.Log("hit stuff");
			manager.SendMessage ("BallFell", team);
		}
	}
}
