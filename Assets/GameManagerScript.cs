using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {
	//�z��̐錾
	int[] map;

	//������̐錾�Ə�����
	string debugText = "";

	int playerIndex = -1;

	//���\�b�h��
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

	//�ړ�����
	bool MoveNumber(int number, int moveFrom, int moveTo) {
		//�ړ��悪�͈͊O�Ȃ�ړ��s�\
		if (moveTo < 0 || moveTo >= map.Length) {return false;}
		//�ړ����2(��)��������
		if (map[moveTo] == 2) {
			int velocity = moveTo - moveFrom;
			bool success = MoveNumber(2,moveTo,moveTo + velocity);
			//���������ړ����s������v���C���[���ړ����s
			if(!success) { return false;}
		}
		map[moveTo] = number;
		map[moveFrom] = 0;
		return true;
	}


	// Start is called before the first frame update
	void Start() {
		//�z��̎��Ԃ̍쐬�Ə�����
		map = new int[] { 0, 0, 2, 1, 0, 2, 2, 0, 0 };
		PrintArray();
	}

	// Update is called once per frame
	void Update() {
		
		//�E�ړ�
		if (Input.GetKeyDown(KeyCode.RightArrow)) {
	
			int playerIndex = GetPlayerIndex();

			/*
			playerIndex+1�̃C���f�b�N�X�̕��ƌ�������̂�
			playerIndex-1��肳��ɏ������C���f�b�N�X�̎�
			�̂݌����������s��
			*/
			MoveNumber(1,playerIndex,playerIndex + 1);
			PrintArray();
		}

		//���ړ�
		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			playerIndex = GetPlayerIndex();

			MoveNumber(1, playerIndex,playerIndex - 1);
			PrintArray();
		}
	}
}
