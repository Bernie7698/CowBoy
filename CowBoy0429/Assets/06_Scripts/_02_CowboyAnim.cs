using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _02_CowboyAnim : MonoBehaviour {

	private Animator animatorCowboy;    //宣告腳色動畫
	public float RunAnimSpeed, JumpLeftAnimSpeed, JumpRightAnimSpeed;   //動畫播放速度
	public bool BoolRun, BoolJump, BoolJumpLeft, BoolJumpRight, BoolOver;  //控制動畫開關
	// Use this for initialization
	void Start () {
		animatorCowboy = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		//動畫開關
		animatorCowboy.SetBool ("BoolRun",BoolRun);
		animatorCowboy.SetBool ("BoolJump",BoolJump);
		animatorCowboy.SetBool ("BoolJumpLeft",BoolJumpLeft);
		animatorCowboy.SetBool ("BoolJumpRight",BoolJumpRight);
		animatorCowboy.SetBool ("BoolOver",BoolOver);  
		//動畫速度控制
		animatorCowboy.SetFloat("RunAnimSpeed",RunAnimSpeed );
		animatorCowboy.SetFloat("JumpLeftAnimSpeed",JumpLeftAnimSpeed );
		animatorCowboy.SetFloat("JumpRightAnimSpeed",JumpRightAnimSpeed );

	}
	void JumpLREnd(){
		BoolJumpLeft = false;
		BoolJumpRight = false;
	}
}
