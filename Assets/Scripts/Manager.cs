using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

	public GameObject[] keys = new GameObject[10]; //for now
	int[] keyNum;// = new int[5]; //keeps track of the letters (since it doesn't go in order)

	// Use this for initialization
	void Start () {
		keyNum = new int[10]{1, 19, 4, 6, 7,8,10,11,12,-37};
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < keys.Length; i++) {
			if (Input.GetKeyDown (KeyCode.A + keyNum[i]-1)) {
				keys [i].transform.localScale = new Vector3 (1.2f, 1.2f, 1f);
				keys [i].GetComponent<Rigidbody2D> ().AddForce (new Vector2(0f,20000f));
			}
			if (Input.GetKeyUp (KeyCode.A + keyNum[i]-1)) {
				keys [i].transform.localScale = new Vector3 (1f, 1f, 1f);
			}
		}
	}
}
