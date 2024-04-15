using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {
	//�z��̐錾
	int[] map;

	//������̐錾�Ə�����
	string debugText = "";

	int playerIndex;

	// Start is called before the first frame update
	void Start() {
		//�z��̎��Ԃ̍쐬�Ə�����
		map = new int[] { 0, 0, 1, 0, 0 };
		for (int i = 0; i < map.Length; i++) {

			debugText += map[i] + ",";
		}

		playerIndex = -1;


		Debug.Log(debugText);
	}

	// Update is called once per frame
	void Update() {

		//�E�ړ�
		if (Input.GetKeyDown(KeyCode.RightArrow)) {
			for (int i = 0; i < map.Length; i++) {
				if (map[i] == 1) {
					playerIndex = i;
					break;
				}
			}
			/*
			playerIndex+1�̃C���f�b�N�X�̕��ƌ�������̂�
			playerIndex-1��肳��ɏ������C���f�b�N�X�̎�
			�̂݌����������s��
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

		//���ړ�
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
