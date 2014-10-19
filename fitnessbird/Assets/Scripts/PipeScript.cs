using UnityEngine;
using System.Collections;

public class PipeScript : MonoBehaviour {

	public void ResizePipe (bool fromBottom) {
		int multiply = fromBottom ? -1
								  :  1;
		Transform head = transform.FindChild ("Head");
		Transform body = transform.FindChild ("Body");
		head.transform.localPosition = new Vector3(0
		                                          ,multiply * head.transform.localScale.y
		                                          ,0);
		float anchorPoint = fromBottom ? 0
									   : 100;
		body.transform.position = new Vector3 (body.transform.position.x
		                                      ,anchorPoint
		                                      ,body.transform.position.z);
		body.transform.localScale = new Vector3 (body.transform.localScale.x
		                                        ,transform.position.y - anchorPoint
		                                        ,body.transform.localScale.z);
	}
}
