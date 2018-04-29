using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _01_GameControl : MonoBehaviour {

	//宣告判斷遊戲是否結束
	public static bool GameOverbool;
	//吃到的錢幣數目
	public static int CoinNumber;
	//宣告金幣數量顯示文字介面
	public GameObject CoinNumberText;

	public GameObject[] Maps;
	private int MapDistance;

	public GameObject RunDistanceText;    //玩家跑步距離文字物件
	public GameObject MessageMaskUI, PlayStopButton;   //宣告訊息介面遮罩物件, 訊息介面暫停/開始按鈕物件
	public Sprite UI_02, UI_03;     //訊息介面暫停/ 開始物件的兩張切換圖片
	private int Score;        //總分計算
	//闖關失敗介面的遮罩物件, 金幣數量文字物件, 跑步距離文字物件, 分數結算文字物件
	public GameObject EndMaskUI, EndCoinNumberText, EndRunDistanceText, EndScoreText;

	public GameObject Cowboy;   //宣告牛仔物件 (目的抓取牛仔 z 軸座標)

	// Use this for initialization
	void Start () {
		//預設遊戲不再結束狀態
		GameOverbool = false;
		CoinNumber = 0;
		MapDistance= 0;
		MessageMaskUI.SetActive (false);   //預設訊息介面為關閉狀態
		EndMaskUI.SetActive (false);       //預設闖關失敗介面為關閉狀態
	}
	
	// Update is called once per frame
	void Update () {
		UGUIShow ();
		ProduceMap ();
		//如果遊戲結束
		if (GameOverbool == true) {
			EndMaskUI.SetActive (true); 
			if (Score > PlayerPrefs.GetInt ("Score")) {
				//將當前的積分存在 score 儲存欄中
				PlayerPrefs.SetInt ("Score",Score);
			}
		}
	}
	//UI 顯示
	void UGUIShow(){
		//顯示當前獲得金幣數量
		CoinNumberText.GetComponent<Text> ().text = "" + CoinNumber;
		//顯示牛仔跑步的距離
		RunDistanceText.GetComponent<Text> ().text = "" 
			                    + Mathf.FloorToInt (Cowboy.transform.position.z);
		//闖關失敗介面顯示獲得金幣數量
		EndCoinNumberText.GetComponent<Text>().text = "" + CoinNumber;
		//闖關失敗介面顯示牛仔跑步的距離
		EndRunDistanceText.GetComponent<Text> ().text = "" 
			+ Mathf.FloorToInt (Cowboy.transform.position.z);
		//得分計算方式為, 獲得金幣數量 x10 + 無條件捨去牛仔移動 z 軸的距離
		Score = CoinNumber*10 + Mathf.FloorToInt (Cowboy.transform.position.z);
		EndScoreText.GetComponent<Text> ().text = ""
		+ Score;
	}
	//連續地圖判斷
	void ProduceMap(){
	//宣告尋找標籤為 Map 的遊戲物件
		GameObject FindMap = GameObject.FindGameObjectWithTag("Map");
		if (FindMap == null) {
			MapDistance += 16;
			int RandomMap = Random.Range (0, Maps.Length);
			Instantiate (Maps[RandomMap],new Vector3(0,0,MapDistance),Quaternion.identity);
		}
	}
	public void PlayStop(){
		//如果主介面的暫停/開始按鈕名稱為 UI_02 (暫停圖片)
		if (PlayStopButton.GetComponent<Image> ().sprite.name == "UI_02") {
			//圖片換成 UI_03 (開始圖片)
			PlayStopButton.GetComponent<Image> ().sprite = UI_03;
			//顯示訊息介面
			MessageMaskUI.SetActive (true);
			// 遊戲運行速度為 0, 暫停遊戲
			Time.timeScale = 0;
		} else {    //如果主介面的暫停/ 開始按鈕圖片為 UI_03(開始圖片)
			//圖片換成 UI_02 (暫停圖片)
			PlayStopButton.GetComponent<Image> ().sprite = UI_02;
			//隱藏訊息介面
			MessageMaskUI.SetActive (false);
			//遊戲執行速度為 1, 正常運行
			Time.timeScale = 1;
		}
	
	}
	//返回主選單
	public void MenuButton(){
		Application.LoadLevel (0);
		Debug.Log ("MenuButton");
	}
	// 訊息介面的連續遊戲按鈕
	public void MessagePlayButton(){
		//隱藏訊息介面
		MessageMaskUI.SetActive (false);
		//主介面的暫停/ 開始按鈕換成 UI_02(暫停圖片)
		PlayStopButton.GetComponent<Image> ().sprite = UI_02;
		Time.timeScale = 1;
		Debug.Log ("MessagePlayButton");

	}
	//在玩一次按鈕
	public void EndAgainButton(){
		Debug.Log ("EndAgainButton");
		Application.LoadLevel (1);
	}
}
