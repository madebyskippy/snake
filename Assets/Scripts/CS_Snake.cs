using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
	private List<float> myAnchorsPullA = new List<float> ();
	private List<float> myAnchorsPullB = new List<float> ();

	private GameObject[] keyAnchors = new GameObject[10];
	private bool[] keyAnchorsIsPulling = new bool[10];
	[SerializeField] float keyPullLength = 3;

	[SerializeField] GameObject head;
	[SerializeField] GameObject tongue;
	[SerializeField] GameObject eyeSample;
	private GameObject[] eyes;

	private Rigidbody2D[] tongueBones;
	private HingeJoint2D hingeTongue0;
	private SpringJoint2D springTongue0;
	private FixedJoint2D fixedTongue0;
	private Rigidbody2D headBone;

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
			int t_PullNumber = 0;
			myAnchorsPullA.Add (t_PullNumber);
			myAnchorsPullB.Add (t_PullNumber);
		}

		for (int i = 0; i < keyAnchors.Length; i++) {
			keyAnchors [i] = myAnchors [(myAnchors.Count / keyAnchors.Length) * i + (myAnchors.Count / keyAnchors.Length) / 2];
		}

//		Debug.Log (keyAnchors);

		// Head
		head = Instantiate(head, myBodyParts[0].transform);
		head.transform.localPosition = Vector3.zero;

		tongue = Instantiate(tongue, myBodyParts[0].transform);
		tongue.transform.localScale=new Vector3 (0.6f,0.6f,1f);
		tongue.transform.localPosition = new Vector3 (-3.2f, -0.22f, 0f);//Vector3.zero;
		tongueBones = tongue.GetComponentsInChildren<Rigidbody2D>();
		headBone = myBodyParts [0].GetComponent<Rigidbody2D> ();
//		Debug.Log (tongueBones[0]);

		hingeTongue0 = tongueBones[0].GetComponent<HingeJoint2D>();
		springTongue0 = tongueBones [0].GetComponent<SpringJoint2D> ();
		fixedTongue0 = tongueBones [0].GetComponent<FixedJoint2D> ();

//		hingeTongue0.connectedBody = headBone;
//		springTongue0.connectedBody = headBone;
		fixedTongue0.connectedBody = headBone;

		eyes = new GameObject[2];
		for (int i = 0; i < eyes.Length; i++) {
			eyes [i] = Instantiate (eyeSample, head.transform) as GameObject;
			eyes [i].GetComponent<SpriteRenderer> ().color = new Color (0f, 0f, 0f, 1f);
			eyes [i].transform.localScale = new Vector3 (0.125f, 0.125f, 1f);
			eyes [i].transform.SetParent(head.transform);
		}

		eyes [0].transform.localPosition = new Vector3 (-0.4f, 0.3f, -1f);
		eyes [1].transform.localPosition = new Vector3 (-0.4f, -0.3f, -1f);
		Blinking ();
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
			myAnchors [i].GetComponent<SpringJoint2D> ().distance = (
			    myAnchorDeltaPositionY +
			    myAnchorMovement_Amplitude * Mathf.Sin (myAnchorMovement_Ratio * (Time.time / myAnchorMovement_Period - i / (float)myBodyTotal)) +
			    (myAnchorsPullA [i] + myAnchorsPullB [i]) * keyPullLength
			);
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

	public List<GameObject> GetAnchors () {
		return myAnchors;
	}
		
	//Player: 1=A, 2=B
	public void PullAnchor (int g_step, int g_player, float g_amount) {
		if (g_player == 1)
			myAnchorsPullA [g_step] = g_amount * -1;
		else if (g_player == 2)
			myAnchorsPullB [g_step] = g_amount * -1;
		else
			Debug.Log ("WHAT PLAYER ARE YOU CALLING? " + g_player);
	}

	public void ReleaseAnchor (int g_step, int g_player) {
		PullAnchor (g_step, g_player, 0);
	}

	public void PullSpring (int g_key) {
//		keyAnchors [g_key].GetComponent<SpringJoint2D> ().distance = myAnchorDeltaPositionY - keyPullLength;
		keyAnchorsIsPulling [g_key] = true;
	}

	public void ReleaseSpring (int g_key) {
//		keyAnchors [g_key].GetComponent<SpringJoint2D> ().distance = myAnchorDeltaPositionY;
		keyAnchorsIsPulling [g_key] = false;


	}

	public void highlightSnakePart(int g){
//		myBodyParts[g].GetComponent<SpriteRenderer>().color = new Color(149f/255f,255f/255f,182f/255f,1f);
		myBodyParts[g].transform.localScale = new Vector3(1.5f,1.5f,1f);
		myBodyParts[g].transform.DOScale (new Vector3(1f,1f,1f),0.25f);
//		StartCoroutine(normalColorSnakePart(g, 0.25f));
	}

	IEnumerator normalColorSnakePart(int g, float delayTime){
		yield return new WaitForSeconds(delayTime);
//		myBodyParts[g].GetComponent<SpriteRenderer>().color = new Color(109f/255f,215f/255f,142f/255f,1f);
		myBodyParts[g].transform.localScale = new Vector3(1f,1f,1f);
	}

	public void Blinking(){
		float rate = UnityEngine.Random.Range (1, 4);
		eyes [0].transform.DOScaleX (0, 0.1f).SetLoops (2, LoopType.Yoyo).SetDelay (rate);
		eyes[1].transform.DOScaleX(0, 0.1f).SetLoops(2, LoopType.Yoyo).SetDelay(rate)
			.OnComplete(()=>Blinking());
	}


}
