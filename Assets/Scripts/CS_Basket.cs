using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Basket : MonoBehaviour {
	[SerializeField] GameObject myBasketNetSample;
	[SerializeField] GameObject myBasketCircle_TL;
	[SerializeField] GameObject myBasketCircle_TR;
	[SerializeField] GameObject myBasketCircle_BL;
	[SerializeField] GameObject myBasketCircle_BR;

	private GameObject myBasketNet_L;
	private GameObject myBasketNet_R;
	private GameObject myBasketNet_B;
	// Use this for initialization
	void Start () {
		myBasketNet_L = Instantiate (myBasketNetSample, this.transform) as GameObject;
		myBasketNet_R = Instantiate (myBasketNetSample, this.transform) as GameObject;
		myBasketNet_B = Instantiate (myBasketNetSample, this.transform) as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateNet (myBasketNet_L, myBasketCircle_TL, myBasketCircle_BL);
		UpdateNet (myBasketNet_R, myBasketCircle_TR, myBasketCircle_BR);
		UpdateNet (myBasketNet_B, myBasketCircle_BL, myBasketCircle_BR);
	}

	private void UpdateNet (GameObject g_Net, GameObject g_CircleA, GameObject g_CircleB) {
		Vector2 t_direction = g_CircleA.transform.position - g_CircleB.transform.position;
		Vector2 t_position = (g_CircleA.transform.position + g_CircleB.transform.position) / 2;

		Quaternion t_quaternion = Quaternion.Euler 
			(0, 0, Vector2.Angle (Vector2.up, t_direction) * Vector3.Cross (Vector3.up, (Vector3)t_direction).normalized.z);

		g_Net.transform.position = t_position;
		g_Net.transform.rotation = t_quaternion;
		g_Net.transform.localScale = new Vector3 (g_Net.transform.localScale.x, t_direction.magnitude, 1);

	}
}
