using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _05_CoinRotation : MonoBehaviour {

	public float RotationSpeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (0,RotationSpeed*Time.deltaTime,0);
	}
}
