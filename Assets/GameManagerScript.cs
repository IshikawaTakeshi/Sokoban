using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

	public GameObject playerPrefab;

	//���x���f�U�C���p�z��
	int[,] map;

	//�Q�[���Ǘ��p�̔z��
	GameObject[,] field;

	//������̐錾�Ə�����
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

	//�ړ�����
	bool MoveNumber(string tag, Vector2Int moveFrom, Vector2Int moveTo) {

		//�ړ��悪�͈͊O�Ȃ�ړ��s�\
		if (moveTo.y < 0 || moveTo.y >= field.GetLength(0)) { return false; }
		if (moveTo.x < 0 || moveTo.x >= field.GetLength(1)) { return false; }
		//�ړ����2(��)��������
		if (field[moveTo.y, moveTo.x] != null && field[moveTo.y, moveTo.x].tag == "Box") {
			Vector2Int velocity = moveTo - moveFrom;
			bool success = MoveNumber(tag, moveTo, moveTo + velocity);
			//���������ړ����s������v���C���[���ړ����s
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

		//��dfor����2�����z��̏������
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

		//�E�ړ�
		if (Input.GetKeyDown(KeyCode.RightArrow)) {

			playerIndex = GetPlayerIndex();

			/*
			playerIndex+1�̃C���f�b�N�X�̕��ƌ�������̂�
			playerIndex-1��肳��ɏ������C���f�b�N�X�̎�
			�̂݌����������s��
			*/
			MoveNumber("Player", playerIndex, playerIndex + new Vector2Int(1,0));

		}

		//���ړ�
		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			playerIndex = GetPlayerIndex();

			MoveNumber("Player", playerIndex, playerIndex - new Vector2Int(1, 0));

		}
	}
}
