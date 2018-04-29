using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _08_CowboyControlMouse : MonoBehaviour {
	
	private bool Challenge;        //宣告闖關挑戰判斷
	public float CowboyRunSpeed;   //腳色跑步速度
	private bool OnFloor;          //是否站在地板上
	public float JumpHigh;         //宣告腳色跳躍作用力
	private int  CowboyRunWay;     //腳色所在跑到位置
	public float ChangingRunWaySpeed;   //切換跑到速度
	public GameObject JumSound, RunSmoke;   //跳躍聲音, 跑步煙霧效果
	public GameObject CoinEffects, GetCoinSound, HitSound;     //宣告獲得金幣特效, 獲得金幣特效, 撞到牆特效

	public static float StartReciptrocalTime;    //宣告開始闖關的倒數時間
	public GameObject StartReciptrocalTimeText;  //宣告開始闖關的倒數文字物件

	Vector2 MouseDownPos, MouseUpPos;    //紀錄滑鼠壓下與放開座標
	float HorizontalDistance, VerticalDistance;    //紀錄滑鼠移動的垂直與水平距離


	// Use this for initialization
	void Start () {
		Challenge = true;
		transform.position = new Vector3 (0, 0.25f, 0);
		CowboyRunWay = 0;
		StartReciptrocalTime = 3;
		//預設煙霧特效為關閉狀態
		RunSmoke.GetComponent<ParticleSystem> ().enableEmission = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Challenge == true) {
			if (StartReciptrocalTime > 0.1) {
				StartReciptrocalTime -= Time.deltaTime;   //開始倒數計時
				StartReciptrocalTimeText.GetComponent<Text> ().text = "" + Mathf.Ceil (StartReciptrocalTime);
			} else {
				StartReciptrocalTimeText.GetComponent<Text> ().text = "";
				ChallengeStart ();
			}
		}
	}
	//執行闖關挑戰
	void ChallengeStart(){
		//角色跑步動作開啟
		GetComponent<_02_CowboyAnim>().BoolRun = true;
		//角色往前移動, 增加 z 軸座標
		transform.Translate(0,0,CowboyRunSpeed*Time.deltaTime);
		ControlMouse();
		if (transform.position.x != CowboyRunWay) {
			//移動到角色所該到的跑道
			CheckCowboyPosx ();
			//如果角色站在地板上
			if (OnFloor == true) {
				//煙霧特效發射
				RunSmoke.GetComponent<ParticleSystem> ().enableEmission = true;
			} else {
				//煙霧特效關閉
				RunSmoke.GetComponent<ParticleSystem> ().enableEmission = false;
			}
		} 
	}
	void ControlMouse(){
		if (Input.GetMouseButtonDown (0)) {
			MouseDownPos = Input.mousePosition;
		}
		if (Input.GetMouseButtonUp (0)) {
			MouseUpPos = Input.mousePosition;
			DirectionChoose ();
		}
	}
	//滑鼠按下和離開之間的移動距離方向選擇
	void DirectionChoose(){
		//水平距離為滑鼠壓下與放開的 x 軸差
		HorizontalDistance = MouseUpPos.x - MouseDownPos.x;
		//垂直距離為滑鼠壓下與放開的 y 軸差
		VerticalDistance = MouseUpPos.y - MouseDownPos.y;
		if (Mathf.Abs (HorizontalDistance) > Mathf.Abs (VerticalDistance)) {
			LeftRight ();
		} else {
			JumpMove ();
		}
	}
	void JumpMove(){
		//如果按下空白鍵而且角色在地板上
		if (VerticalDistance>0 && OnFloor == true) {
			GetComponent<_02_CowboyAnim> ().BoolJump = true;
			//往上跳, 增加 y 軸作用力
			GetComponent<Rigidbody> ().AddForce (0, JumpHigh, 0);
			OnFloor = false;
			//播放跳躍聲音
			Instantiate (JumSound, transform.position, Quaternion.identity);
		}
	}
	//角色左右跳控制
	void LeftRight(){
		if (HorizontalDistance<0 && CowboyRunWay > -1) {
			GetComponent<_02_CowboyAnim> ().BoolJumpLeft = true;
			CowboyRunWay -= 1;
			Instantiate (JumSound, transform.position, Quaternion.identity);
		}
		if (HorizontalDistance>0 && CowboyRunWay < 1) {
			GetComponent<_02_CowboyAnim> ().BoolJumpRight = true;
			CowboyRunWay += 1;
			Instantiate (JumSound, transform.position, Quaternion.identity);
		}
	}
	//移動角色位置
	void CheckCowboyPosx(){
		float ChangingRunway = Mathf.Lerp (transform.position.x,
			CowboyRunWay,ChangingRunWaySpeed*Time.deltaTime);
		transform.position = new Vector3 (ChangingRunway,transform.position.y, 
			transform.position.z);
	}
	void OnCollisionEnter(Collision col){
		if (col.collider.tag == "Floor") {
			//關閉角色跳躍動作
			GetComponent<_02_CowboyAnim> ().BoolJump = false;
			//角色站在地板上
			OnFloor = true;
		}

		if(col.collider.tag == "Wall"){
			GetComponent<_02_CowboyAnim> ().BoolOver = true;
			Challenge = false;
			//遊戲結束
			_01_GameControl.GameOverbool = true;
			//產生撞牆聲音
			Instantiate (HitSound, transform.position,Quaternion.identity);
			//煙霧特效關閉
			RunSmoke.GetComponent<ParticleSystem> ().enableEmission = false;

		}
		if (col.collider.tag == "Coin") {
			_01_GameControl.CoinNumber += 1;
			//刪除金幣物件
			Destroy (col.gameObject);
			//產生金幣特效
			Instantiate (CoinEffects, transform.position + new Vector3(0,1f,0),Quaternion.identity);
			//產生吃到硬幣的聲音
			Instantiate (GetCoinSound, transform.position,Quaternion.identity);
		}
	}
}
