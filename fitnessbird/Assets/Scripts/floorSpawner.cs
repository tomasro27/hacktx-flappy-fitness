using UnityEngine;
using System.Collections;

public class floorSpawner : MonoBehaviour {
	
	public ArrayList floors;
	public Transform player;
	public GameObject floorPrefab;
	public int numOfFloors = 100;
 	float currentPosition;

	
	// Use this for initialization
	void Start () {
		floors = new ArrayList ();
		currentPosition = player.position.x;
		for(int x = 0; x < numOfFloors; x++) {
			floors.Add (Instantiate (floorPrefab, new Vector3 (0, 0, currentPosition), player.rotation));
			currentPosition += 10;
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}