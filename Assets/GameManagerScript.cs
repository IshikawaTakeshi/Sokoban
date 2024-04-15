using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {
	//配列の宣言
	int[] map;

	//文字列の宣言と初期化
	string debugText = "";

	int playerIndex = -1;

	//メソッド化
	void PrintArray() {
		string debugText = "";
		for (int i = 0; i < map.Length; i++) {
			debugText += map[i] + ",";
		}

		Debug.Log(debugText);
	}


	int GetPlayerIndex() {
		for (int i = 0;i < map.Length;i++) {
			if (map[i] == 1) {
			return i;
			}
		}
		return -1;
	}

	//移動処理
	bool MoveNumber(int number, int moveFrom, int moveTo) {
		//移動先が範囲外なら移動不可能
		if (moveTo < 0 || moveTo >= map.Length) {return false;}
		//移動先に2(箱)がいたら
		if (map[moveTo] == 2) {
			int velocity = moveTo - moveFrom;
			bool success = MoveNumber(2,moveTo,moveTo + velocity);
			//もし箱が移動失敗したらプレイヤーも移動失敗
			if(!success) { return false;}
		}
		map[moveTo] = number;
		map[moveFrom] = 0;
		return true;
	}


	// Start is called before the first frame update
	void Start() {
		//配列の実態の作成と初期化
		map = new int[] { 0, 0, 2, 1, 0, 2, 2, 0, 0 };
		PrintArray();
	}

	// Update is called once per frame
	void Update() {
		
		//右移動
		if (Input.GetKeyDown(KeyCode.RightArrow)) {
	
			int playerIndex = GetPlayerIndex();

			/*
			playerIndex+1のインデックスの物と交換するので
			playerIndex-1よりさらに小さいインデックスの時
			のみ交換処理を行う
			*/
			MoveNumber(1,playerIndex,playerIndex + 1);
			PrintArray();
		}

		//左移動
		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			playerIndex = GetPlayerIndex();

			MoveNumber(1, playerIndex,playerIndex - 1);
			PrintArray();
		}
	}
}
