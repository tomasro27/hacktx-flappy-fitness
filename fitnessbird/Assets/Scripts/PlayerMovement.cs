using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float forwardSpeed= 3.5f;
	private bool upswing = false;
	private bool crest = false;
	private bool downswing = true;
	public float jumpSpeed = 5f;
	public bool dead = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!dead) {
						rigidbody.velocity = new Vector3 (0, rigidbody.velocity.y, forwardSpeed);
						//print(transform.GetComponentsInChildren<ThalmicMyo> ()[0].accelerometer.magnitude - 1);
						if (downswing && transform.GetComponentsInChildren<ThalmicMyo> () [0].accelerometer.magnitude - 1 > .25f) {
								upswing = true;
								downswing = false;
								Jump ();
								print ("upswing" + (transform.GetComponentsInChildren<ThalmicMyo> () [0].accelerometer.magnitude - 1));
						} else if (upswing && transform.GetComponentsInChildren<ThalmicMyo> () [0].accelerometer.magnitude - 1 < -.25f) {
								upswing = false;
								crest = true;
								print ("crest" + (transform.GetComponentsInChildren<ThalmicMyo> () [0].accelerometer.magnitude - 1));
						
						} else if (crest && transform.GetComponentsInChildren<ThalmicMyo> () [0].accelerometer.magnitude - 1 > .25f) {
								crest = false;
								downswing = true;
								print ("downswing" + (transform.GetComponentsInChildren<ThalmicMyo> () [0].accelerometer.magnitude - 1));
						}
				} else {
			if(transform.GetComponentsInChildren<ThalmicMyo> () [0].pose == Thalmic.Myo.Pose.Fist) {
				print ("FIST!");
			}
			}
			}
			
	void Jump() {
		rigidbody.velocity = new Vector3 (0, jumpSpeed, rigidbody.velocity.z);
	}

	void OnCollisionEnter (Collision col) {
		dead = true;
		MeshRenderer[] childList = transform.GetComponentsInChildren<MeshRenderer> ();
		for (int i = 0; i < childList.Length; i++) {
			childList [i].enabled = true;
		}
	}
}
