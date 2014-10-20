using UnityEngine;
using System.Collections;

public class PipeSpawner : MonoBehaviour {

	public float distanceBetweenPipeSets = 6;
	public float distanceOfPipeGap = 4;
	public float maxPipeHeightChange = 2;
	public GameObject pipePrefab;
	public GameObject triggerPrefab;
	public Transform player;
	public int numOfPipePairs = 100;
	private float prevHeight = 5;
	private float prevDist;
	private ArrayList pipes;

	// Use this for initialization
	void Start () {
		pipes = new ArrayList ();
		prevDist = distanceBetweenPipeSets *1.5f;
		for (int i = 0; i < numOfPipePairs; i++) {
						spawnPipePair (prevDist);
			prevDist += distanceBetweenPipeSets;
				}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void spawnPipePair (float zLocation) {
		prevHeight -= 3;
		prevHeight = Random.Range (prevHeight - maxPipeHeightChange, prevHeight + maxPipeHeightChange);
		prevHeight = Mathf.Clamp (prevHeight, 0, 15);
		prevHeight += 3;
		GameObject pipe = (GameObject) Instantiate (pipePrefab
		                                            , new Vector3 (0
		               , prevHeight - (distanceOfPipeGap / 2)
		               , zLocation)
		                                            , player.rotation);
		pipe.transform.GetComponent<PipeScript> ().ResizePipe(true);
		pipes.Add(pipe);
		pipe = (GameObject) Instantiate (pipePrefab
		                                 , new Vector3 (0
		               , prevHeight + (distanceOfPipeGap / 2)
		               , zLocation)
		                                 , player.rotation);
		pipe.transform.GetComponent<PipeScript> ().ResizePipe(false);
		pipes.Add(pipe);
		pipe = (GameObject)Instantiate (triggerPrefab
		                                 , new Vector3 (0, prevHeight, zLocation)
		                                 , player.rotation);
		pipe.transform.localScale = new Vector3 (1, distanceOfPipeGap, 1);
		}

}
