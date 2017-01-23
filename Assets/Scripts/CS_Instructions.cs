using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Instructions : MonoBehaviour {

	[SerializeField] bool isOnCabinet;

	[SerializeField] GameObject cabinet;
	[SerializeField] GameObject keyboard;
	[SerializeField] GameObject xBox;
	[SerializeField] GameObject ps4;
	[SerializeField] GameObject warning;

	private bool Xbox_360_Controller = false;
	private bool PS4_Controller = false;

	void Start () {
		if (isOnCabinet) {
			
			cabinet.SetActive (true);

			keyboard.SetActive (false);
			xBox.SetActive (false);
			ps4.SetActive (false);
			warning.SetActive (false);

		} else
			cabinet.SetActive (false);
	}

	void Update () {
		if (isOnCabinet)
			return;

		string[] names = Input.GetJoystickNames();

		Xbox_360_Controller = false;
		PS4_Controller = false;
		for (int x = 0; x < names.Length; x++)
		{
			Debug.Log (names [x].Length);
			if (names[x].Length == 55) {
				Debug.Log ("XBOX 360 CONTROLLER IS CONNECTED");
				Xbox_360_Controller = true;
			}

			if(names[x].Length == 50){ //LAURENZ CHANGE THIS 0 TO A NUMBER
				Debug.Log ("PS4 CONTROLLER IS CONNECTED");
				PS4_Controller = true;
			}
		}

		if (Xbox_360_Controller == true && PS4_Controller == true) {
			
			warning.SetActive(true);

			keyboard.SetActive (false);
			xBox.SetActive(false);
			ps4.SetActive (false);

		} else if (Xbox_360_Controller == true) {

			xBox.SetActive(true);

			keyboard.SetActive (false);
			ps4.SetActive (false);
			warning.SetActive(false);

		} else if (PS4_Controller == true){

			ps4.SetActive (true);

			xBox.SetActive(false);
			keyboard.SetActive (false);
			warning.SetActive(false);

		} else {
			
			keyboard.SetActive (true);

			xBox.SetActive(false);
			ps4.SetActive (false);
			warning.SetActive(false);

		}
	}
}
