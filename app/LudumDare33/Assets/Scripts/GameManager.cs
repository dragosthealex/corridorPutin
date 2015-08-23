﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Corridor corridorPrefab;
	public ConferenceRoom conferenceRoomPrefab;
	private Corridor corridorInstance;
	private ConferenceRoom conferenceRoomInstance;

	// Player prefab
	public GameObject playerPF;

	//Player
	public GameObject player;

	private void Start () {
		BeginGame();
		player = Instantiate(playerPF,corridorInstance.cellList[0].transform.position + Vector3.up,Quaternion.identity) as GameObject;

	}
	
	private void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			RestartGame();
		}
	}
	
	private void BeginGame () {
		corridorInstance = Instantiate (corridorPrefab) as Corridor;
		corridorInstance.Generate ();
	}
	
	private void RestartGame () {
		Destroy (corridorInstance.gameObject);
		BeginGame();
	}

	public void newRoom() {
		int randomRoom = Random.Range (0, 2);

		switch (randomRoom) {
		case 0:
			// Another corridor
			corridorInstance = Instantiate (corridorPrefab) as Corridor;
			corridorInstance.Generate();
			FindObjectOfType<Player> ().gameObject.transform.position = new Vector3(
				corridorInstance.GetSpawn ().x, 1f, corridorInstance.GetSpawn ().z);
			break;
		case 1:
			conferenceRoomInstance = Instantiate (conferenceRoomPrefab) as ConferenceRoom;
			FindObjectOfType<Player> ().gameObject.transform.position = new Vector3( 
			    conferenceRoomInstance.GetSpawn ().x, 1f, conferenceRoomInstance.GetSpawn ().z);
			break;
		}
		FindObjectOfType<TurnPutin>().gameObject.transform.localPosition = new Vector3(0f, 1f, 0f);
	}
}