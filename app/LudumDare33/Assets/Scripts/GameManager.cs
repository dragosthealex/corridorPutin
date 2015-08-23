using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Corridor corridorPrefab;
	public ConferenceRoom conferenceRoomPrefab;
	private Corridor corridorInstance;

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
			Start ();
			break;
		case 1:
			ConferenceRoom room = Instantiate (conferenceRoomPrefab) as ConferenceRoom;
			break;
		}
	}
}