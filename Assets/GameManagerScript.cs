using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {
	//配列の宣言
	int[] map;

	//文字列の宣言と初期化
	string debugText = "";

	int playerIndex;

	// Start is called before the first frame update
	void Start() {
		//配列の実態の作成と初期化
		map = new int[] { 0, 0, 1, 0, 0 };
		for (int i = 0; i < map.Length; i++) {

			debugText += map[i] + ",";
		}

		playerIndex = -1;


		Debug.Log(debugText);
	}

	// Update is called once per frame
	void Update() {

		//右移動
		if (Input.GetKeyDown(KeyCode.RightArrow)) {
			for (int i = 0; i < map.Length; i++) {
				if (map[i] == 1) {
					playerIndex = i;
					break;
				}
			}
			/*
			playerIndex+1のインデックスの物と交換するので
			playerIndex-1よりさらに小さいインデックスの時
			のみ交換処理を行う
			*/
			if(playerIndex < map.Length - 1) {
				map[playerIndex + 1] = 1;
				map[playerIndex] = 0;
			}

			string debugText = ",";
			for (int i = 0;i < map.Length; i++) {
				debugText += map[i] + ",";
			}
			Debug.Log(debugText);
		}

		//左移動
		if (Input.GetKeyDown(KeyCode.LeftArrow)) {

			for (int i = 0; i < map.Length; i++) {
				if (map[i] == 1) {
					playerIndex = i;
					break;
				}
			}

			if (playerIndex >= 0) {
				map[playerIndex - 1] = 1;
				map[playerIndex] = 0;
			}

			string debugText = ",";
			for (int i = 0; i < map.Length; i++) {
				debugText += map[i] + ",";
			}
			Debug.Log(debugText);
		}
	}
}
