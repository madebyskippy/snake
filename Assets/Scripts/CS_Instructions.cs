using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Instructions : MonoBehaviour {

	public GameObject keyboard;
	public GameObject xBox;

	private bool Xbox_360_Controller = false;

	void Update()
	{
		string[] names = Input.GetJoystickNames();

		Xbox_360_Controller = false;
		for (int x = 0; x < names.Length; x++)
		{
			Debug.Log (names [x].Length);
			if (names[x].Length == 55) {
				Debug.Log ("XBOX 360 CONTROLLER IS CONNECTED");
				Xbox_360_Controller = true;
			}
		}


		if (Xbox_360_Controller == true) {
//			Debug.Log ("controller");
			//do something
			xBox.SetActive(true);
			keyboard.SetActive (false);
		}
		else {
//			Debug.Log ("keyboard");
			//do something
			xBox.SetActive(false);
			keyboard.SetActive (true);

		}
	}
}
