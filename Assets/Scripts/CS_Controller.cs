using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Controller : MonoBehaviour {
	[SerializeField] CS_Snake mySnake;
	[SerializeField] int myPlayerNumber;
	[SerializeField] GameObject mySelection;
	private string myControllerSuffix = "";
	private int myStep;

	[SerializeField] float mySpeed = 0.02f; //How many time does it take to move to the next point
	private float myAccumulation = 0;

	// Use this for initialization
	void Start () {
		myStep = 0;
		mySelection = Instantiate (mySelection, this.transform);

		if(myPlayerNumber == 1){
			myControllerSuffix = "A";
		} else if (myPlayerNumber == 2) {
			myControllerSuffix = "B";
		}


		if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer) {
			myControllerSuffix += "Mac";
		}
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log ("Horizontal" + myControllerSuffix);

		myAccumulation += Input.GetAxis ("Horizontal" + myControllerSuffix) * Time.deltaTime;
//		Debug.Log (myAccumulation);

		for (int i = 0; i < 100; i++) {
			if (myAccumulation > mySpeed) {
				myAccumulation -= mySpeed;
				MoveRight ();
			} else if (myAccumulation < mySpeed * -1) {
				myAccumulation += mySpeed;
				MoveLeft ();
			}
		}

		mySelection.transform.position = mySnake.GetBodyParts () [myStep].transform.position;

		mySnake.PullAnchor (myStep, myPlayerNumber, Input.GetAxis ("Vertical" + myControllerSuffix));

		mySnake.highlightSnakePart (myStep);


//		Debug.Log (myStep);

	}

	private void MoveRight () {
		mySnake.ReleaseAnchor (myStep, myPlayerNumber);
		myStep++;
		if (myStep >= mySnake.GetAnchors ().Count)
			myStep = mySnake.GetAnchors ().Count - 1;
	}

	private void MoveLeft () {
		mySnake.ReleaseAnchor (myStep, myPlayerNumber);
		myStep--;
		if(myStep < 0)
			myStep = 0;
	}
}
