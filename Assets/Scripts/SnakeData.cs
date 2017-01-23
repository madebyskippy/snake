using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeData : MonoBehaviour {
	private static SnakeData instance = null;

	int[] score;

	//========================================================================
	public static SnakeData Instance {
		get { 
			return instance;
		}
	}

	void Awake () {
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
		} else {
			instance = this;
			score = new int[2];
			score [0] = 0;
			score [1] = 0;
		}

		DontDestroyOnLoad(this.gameObject);
	}
	//========================================================================

	// Use this for initialization
	void Start () {
		score = new int[2];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void increaseScore(int team){
		score [team] += 1;
	}

	public int getScore(int team){
		return score [team];
	}
}
