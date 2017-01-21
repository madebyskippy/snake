using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Snake : MonoBehaviour {
	[SerializeField] GameObject myBodySample;
	[SerializeField] Vector2 myBodyDeltaPosition;
	[SerializeField] int myBodyTotal = 100;
	private List<GameObject> myBodyParts = new List<GameObject> ();

	[SerializeField] GameObject myBodyConnectionSample;
	private List<GameObject> myBodyConnections = new List<GameObject> ();

	[SerializeField] GameObject myAnchorSample;
	[SerializeField] float myAnchorDeltaPositionY;
	[SerializeField] float myAnchorMovement_Amplitude = 1;
	[SerializeField] float myAnchorMovement_Period = 12;
	[SerializeField] float myAnchorMovement_Ratio = 2;

	private List<GameObject> myAnchors = new List<GameObject> ();

	private GameObject[] keyAnchors = new GameObject[10];
	private bool[] keyAnchorsIsPulling = new bool[10];
	[SerializeField] float keyPullLength = 3;

	// Use this for initialization
	void Start () {

		//Init Body

		GameObject t_bodyPartOne = Instantiate (myBodySample, this.transform.position, Quaternion.identity) as GameObject;
		t_bodyPartOne.transform.SetParent (this.transform);
		t_bodyPartOne.GetComponent<SpringJoint2D> ().enabled = false;
		myBodyParts.Add (t_bodyPartOne);

		for (int i = 1; i < myBodyTotal; i++) {
			GameObject t_bodyPart = Instantiate (myBodySample, this.transform.position, Quaternion.identity) as GameObject;
			t_bodyPart.transform.SetParent (this.transform);
			myBodyParts.Add (t_bodyPart);

			myBodyParts [i].transform.position = (Vector3)myBodyDeltaPosition + myBodyParts [i - 1].transform.position;
			//myBodyParts [i].GetComponent<HingeJoint2D> ().connectedAnchor = myBodyDeltaPosition;
			//myBodyParts [i].GetComponent<HingeJoint2D> ().connectedBody = myBodyParts [i - 1].GetComponent<Rigidbody2D> ();
			myBodyParts [i].GetComponent<SpringJoint2D> ().connectedAnchor = myBodyDeltaPosition;
			myBodyParts [i].GetComponent<SpringJoint2D> ().connectedBody = myBodyParts [i - 1].GetComponent<Rigidbody2D> ();
		}

		//Init Body Connection

		for (int i = 1; i < myBodyParts.Count; i++) {
			GameObject t_bodyConnection = Instantiate (myBodyConnectionSample, this.transform);
			myBodyConnections.Add (t_bodyConnection);

		}

		//Init Anchor

		for (int i = 0; i < myBodyParts.Count; i++) {
			GameObject t_Anchor = Instantiate (myAnchorSample, this.transform);
			t_Anchor.transform.position = myBodyParts [i].transform.position + myAnchorDeltaPositionY * Vector3.up;
			t_Anchor.GetComponent<SpringJoint2D> ().connectedBody = myBodyParts [i].GetComponent<Rigidbody2D> ();
			t_Anchor.GetComponent<SpringJoint2D> ().distance = myAnchorDeltaPositionY;
//			t_Anchor.GetComponent<SpringJoint2D> ().distance = myAnchorDeltaPositionY +
//				myAnchorMovement_Amplitude * Mathf.Sin (360 * (Time.time / myAnchorMovement_Period - i / (float)myBodyTotal));

//			t_Anchor.GetComponent<SpringJoint2D> ().connectedAnchor = Vector3.zero;

			myAnchors.Add (t_Anchor);
		}

		for (int i = 0; i < keyAnchors.Length; i++) {
			keyAnchors [i] = myAnchors [(myAnchors.Count / keyAnchors.Length) * i + (myAnchors.Count / keyAnchors.Length) / 2];
		}

//		Debug.Log (keyAnchors);

	}
	
	// Update is called once per frame
	void Update () {
		//Update Body Connection

		for (int i = 0; i < myBodyConnections.Count; i++) {
			Vector2 t_direction = myBodyParts [i].transform.position - myBodyParts [i + 1].transform.position;
			Vector2 t_position = (myBodyParts [i].transform.position + myBodyParts [i + 1].transform.position) / 2;

			Quaternion t_quaternion = Quaternion.Euler (0, 0, 
				Vector2.Angle (Vector2.up, t_direction) * Vector3.Cross (Vector3.up, (Vector3)t_direction).normalized.z);

			myBodyConnections [i].transform.position = t_position;
			myBodyConnections [i].transform.rotation = t_quaternion;
			myBodyConnections [i].transform.localScale = new Vector3 (1, t_direction.magnitude, 1);
		}

		UpdateAnchor ();
	}

	private void UpdateAnchor () {
		for (int i = 0; i < myAnchors.Count; i++) {
//			myAnchors [i].GetComponent<SpringJoint2D> ().distance = 10f;
			//myAnchors [i].GetComponent<SpriteRenderer> ().color = Color.black;
			myAnchors [i].GetComponent<SpringJoint2D> ().distance = myAnchorDeltaPositionY +
				myAnchorMovement_Amplitude * Mathf.Sin (myAnchorMovement_Ratio * (Time.time / myAnchorMovement_Period - i / (float)myBodyTotal));

//			Debug.Log (myAnchors [i].GetComponent<SpringJoint2D> ().distance);
		}

		for (int i = 0; i < keyAnchors.Length; i++) {
			if (keyAnchorsIsPulling [i] == true) {
				keyAnchors [i].GetComponent<SpringJoint2D> ().distance = 
					keyAnchors [i].GetComponent<SpringJoint2D> ().distance - keyPullLength;
			}
		}
	}

	public List<GameObject> GetBodyParts () {
		return myBodyParts;
	}

	public void PullSpring (int g_key) {
//		keyAnchors [g_key].GetComponent<SpringJoint2D> ().distance = myAnchorDeltaPositionY - keyPullLength;
		keyAnchorsIsPulling [g_key] = true;
	}

	public void ReleaseSpring (int g_key) {
//		keyAnchors [g_key].GetComponent<SpringJoint2D> ().distance = myAnchorDeltaPositionY;
		keyAnchorsIsPulling [g_key] = false;
	}
}
