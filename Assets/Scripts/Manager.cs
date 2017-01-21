using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

	public GameObject[] keys = new GameObject[1]; //1 for now

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < keys.Length; i++) {
			if (Input.GetKeyDown (KeyCode.A + i)) {
				keys [i].transform.localScale = new Vector3 (1.2f, 1.2f, 1f);
				keys [i].GetComponent<Rigidbody2D> ().AddForce (new Vector2(0f,1000f));
			}
			if (Input.GetKeyUp (KeyCode.A + i)) {
				keys [i].transform.localScale = new Vector3 (1f, 1f, 1f);
			}
		}
	}
}
