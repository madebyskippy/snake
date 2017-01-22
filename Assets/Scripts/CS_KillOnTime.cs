using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_KillOnTime : MonoBehaviour {
	[SerializeField] float myDeathTime = 5;
	// Use this for initialization
	void Start () {
		Destroy (this.gameObject, myDeathTime);
	}
}
