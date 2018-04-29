using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _06_MapCollisionDetection : MonoBehaviour {

	void OnTriggerEnter(Collider col){
		if (col.tag == "Map") {
			col.tag = "RunMap";
		}
	}
	void OnTriggerExit(Collider col){
		if (col.tag == "RunMap") {
			Destroy (col.gameObject);
		}
	}
}
