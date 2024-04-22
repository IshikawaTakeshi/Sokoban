using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

	public GameObject playerPrefab;

	//レベルデザイン用配列
	int[,] map;

	//ゲーム管理用の配列
	GameObject[,] field;

	//文字列の宣言と初期化
	//string debugText = "";

	Vector2Int playerIndex = new Vector2Int(-1, -1);

	private Vector2Int GetPlayerIndex() {
		for (int y = 0; y < field.GetLength(0); y++) {
			for (int x = 0; x < field.GetLength(1); x++) {
				if (field[y, x] == null) { continue; }
				if (field[y, x].tag == "Player") { return new Vector2Int(x, y); }
			}
		}
		return new Vector2Int(-1, -1);
	}

	//移動処理
	bool MoveNumber(string tag, Vector2Int moveFrom, Vector2Int moveTo) {

		//移動先が範囲外なら移動不可能
		if (moveTo.y < 0 || moveTo.y >= field.GetLength(0)) { return false; }
		if (moveTo.x < 0 || moveTo.x >= field.GetLength(1)) { return false; }
		//移動先に2(箱)がいたら
		if (field[moveTo.y, moveTo.x] != null && field[moveTo.y, moveTo.x].tag == "Box") {
			Vector2Int velocity = moveTo - moveFrom;
			bool success = MoveNumber(tag, moveTo, moveTo + velocity);
			//もし箱が移動失敗したらプレイヤーも移動失敗
			if (!success) { return false; }
		}
		field[moveFrom.x, moveFrom.y].transform.position = new Vector3(moveTo.x, field.GetLength(0) - moveTo.y, 0);
		field[moveTo.x, moveTo.y] = field[moveFrom.y, moveFrom.x];
		field[moveFrom.y, moveFrom.x] = null;

		return true;
	}


	// Start is called before the first frame update
	void Start() {

		map = new int[,] {
		{ 0, 0, 0, 0, 0 },
		{ 0, 0, 1, 0, 0 },
		{ 0, 0, 0, 0, 0 },
		};

		field = new GameObject[
			map.GetLength(0),
			map.GetLength(1)
			];

		//二重for文で2次元配列の情報を入力
		for (int y = 0; y < map.GetLength(0); y++) {
			for (int x = 0; x < map.GetLength(1); x++) {
				if (map[y, x] == 1) {
					field[y, x] = Instantiate(
						playerPrefab,
						new Vector3(x, map.GetLength(0) - y, 0),
						Quaternion.identity
						);
				}
			}
		}
	}

	// Update is called once per frame
	void Update() {

		//右移動
		if (Input.GetKeyDown(KeyCode.RightArrow)) {

			playerIndex = GetPlayerIndex();

			/*
			playerIndex+1のインデックスの物と交換するので
			playerIndex-1よりさらに小さいインデックスの時
			のみ交換処理を行う
			*/
			MoveNumber("Player", playerIndex, playerIndex + new Vector2Int(1,0));

		}

		//左移動
		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			playerIndex = GetPlayerIndex();

			MoveNumber("Player", playerIndex, playerIndex - new Vector2Int(1, 0));

		}
	}
}
