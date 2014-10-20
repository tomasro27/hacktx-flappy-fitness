using UnityEngine;
using System.Collections;
using System.Collections.Generic;


using Parse;

public class PlayerMovement : MonoBehaviour {

	public float forwardSpeed= 3.5f;
	private bool upswing = false;
	private bool crest = false;
	private bool downswing = true;
	public float jumpSpeed = 5f;
	public bool dead = false;
	public int score = 0;
	public TextMesh scoreDisplay;

	// Use this for initialization
	void Start () {
		ParseUser.LogInAsync ("HACKTX", "hacktx").ContinueWith (t =>
				{
						if (t.IsFaulted || t.IsCanceled) {
						} else {
								// Login was successful.
						}
				});
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
								//print ("upswing" + (transform.GetComponentsInChildren<ThalmicMyo> () [0].accelerometer.magnitude - 1));
						} else if (upswing && transform.GetComponentsInChildren<ThalmicMyo> () [0].accelerometer.magnitude - 1 < -.25f) {
								upswing = false;
								crest = true;
								//print ("crest" + (transform.GetComponentsInChildren<ThalmicMyo> () [0].accelerometer.magnitude - 1));
						
						} else if (crest && transform.GetComponentsInChildren<ThalmicMyo> () [0].accelerometer.magnitude - 1 > .25f) {
								crest = false;
								downswing = true;
								//print ("downswing" + (transform.GetComponentsInChildren<ThalmicMyo> () [0].accelerometer.magnitude - 1));
						}
				} else {
						if (transform.GetComponentsInChildren<ThalmicMyo> () [0].pose == Thalmic.Myo.Pose.Fist) {
								if (transform.GetComponentsInChildren<ThalmicMyo> () [0].accelerometer.magnitude - 1 > .5f) {
										Application.LoadLevel (Application.loadedLevel);
								}
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
		//	var query = ParseObject.GetQuery("GameScore")
		//		.WhereEqualTo("playerName", "Dan Stemkoski");
		//query.FindAsync().ContinueWith(t =>
		 //                              {
		//	ParseObject results = t.Result;
		//});
		IList list = ParseUser.CurrentUser.Get<List<object>> ("scores");
		list.Add (score);
		ParseUser.CurrentUser.Remove ("scores");
		ParseUser.CurrentUser.Add ("scores", list);
		ParseUser.CurrentUser.SaveAsync ();
	}

	void OnTriggerEnter(Collider other) {
		score++;
		scoreDisplay.text = score.ToString ();
		}
	}
