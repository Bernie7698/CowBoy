using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _03_DestroyMe : MonoBehaviour {

	public float DestroyTime;

	// Update is called once per frame
	void Update () {
		Destroy (gameObject, DestroyTime);   //幾秒後銷毀物件
	}
}
