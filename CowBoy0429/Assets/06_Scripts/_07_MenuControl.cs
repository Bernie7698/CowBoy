using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _07_MenuControl : MonoBehaviour {

	//宣告最高分顯示文字物件
	public GameObject MenuScoreText;

	// Use this for initialization
	void Start () {
		//存檔初始化判斷, 如果名稱為 Score 的存檔內容不存在的話
		if (PlayerPrefs.HasKey ("Score") == false) {
			//在 Score 存檔空間裡存入 0 的數字
			PlayerPrefs.SetInt ("Score", 0);
		}
	}
	// Update is called once per frame
	void Update () {
		MenuScoreText.GetComponent<Text> ().text = "" + PlayerPrefs.GetInt ("Score");
	}
	//開始遊戲按鈕事件
	public void MenuStartButton(){
		Application.LoadLevel (1);
	}
	//離開遊戲按鈕事件
	public void MenuExitButton(){
		Application.Quit ();
	}
}
