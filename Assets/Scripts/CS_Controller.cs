using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Controller : MonoBehaviour {
	private int myStep;
	[SerializeField] CS_Snake mySnake;

	// Use this for initialization
	void Start () {
		myStep = 0;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetAxis ("Horizontal") > 0) {
			mySnake.ReleaseAnchor (myStep);
			myStep++;
			if (myStep >= mySnake.GetAnchors ().Count)
				myStep = mySnake.GetAnchors ().Count - 1;
		}

		if (Input.GetAxis ("Horizontal") < 0) {
			mySnake.ReleaseAnchor (myStep);
			myStep--;
			if(myStep < 0)
				myStep = 0;
		}

		if (Input.GetAxis ("Vertical") <= 0) {
			mySnake.ReleaseAnchor (myStep);
		} else {
			mySnake.PullAnchor (myStep);
		}

		mySnake.highlightSnakePart (myStep);

	}
}
