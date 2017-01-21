using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Snake : MonoBehaviour {
	[SerializeField] GameObject myBodySample;
	private List<GameObject> myBodyParts = new List<GameObject> ();
	[SerializeField] Vector2 myBodyDeltaPosition;
	[SerializeField] int myBodyTotal = 100;
	// Use this for initialization
	void Start () {
		for (int i = 0; i < myBodyTotal; i++) {
			GameObject t_bodyPart = Instantiate (myBodySample, this.transform) as GameObject;
			myBodyParts.Add (t_bodyPart);
		}

		for (int i = 1; i < myBodyParts.Count; i++) {
			myBodyParts [i].transform.position = (Vector3)myBodyDeltaPosition + myBodyParts [i - 1].transform.position;
			myBodyParts [i].GetComponent<HingeJoint2D> ().connectedAnchor = myBodyDeltaPosition;
			myBodyParts [i].GetComponent<HingeJoint2D> ().connectedBody = myBodyParts [i - 1].GetComponent<Rigidbody2D> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
