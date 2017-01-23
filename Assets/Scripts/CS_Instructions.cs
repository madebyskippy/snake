using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Instructions : MonoBehaviour {

	public GameObject keyboard;
	public GameObject xBox;
	public GameObject ps4;

	private bool Xbox_360_Controller = false;
	private bool PS4_Controller = false;

	void Update()
	{
		string[] names = Input.GetJoystickNames();

		Xbox_360_Controller = false;
		PS4_Controller = false;
		for (int x = 0; x < names.Length; x++)
		{
			Debug.Log (names [x].Length);
			if (names[x].Length == 55) {
				Debug.Log ("XBOX 360 CONTROLLER IS CONNECTED");
				Xbox_360_Controller = true;
			}else if(names[x].Length == 0){ //LAURENZ CHANGE THIS 0 TO A NUMBER
				Debug.Log ("PS4 CONTROLLER IS CONNECTED");
				PS4_Controller = true;
			}
		}


		if (Xbox_360_Controller == true) {
//			Debug.Log ("controller");
			//do something
			xBox.SetActive(true);
			keyboard.SetActive (false);
			ps4.SetActive (false);
		}
		else if (PS4_Controller == true){
			xBox.SetActive(false);
			keyboard.SetActive (false);
			ps4.SetActive (true);
		}
		else {
//			Debug.Log ("keyboard");
			//do something
			xBox.SetActive(false);
			keyboard.SetActive (true);
			ps4.SetActive (false);
		}
	}
}
