using UnityEngine;
using System.Collections;

public class RoomManager : MonoBehaviour {

	public GameObject corridorPrefab;
	private Corridor corridorInstance;

	public GameObject conferenceRoomPrefab;
	private ConferenceRoom conferenceRoomInstance;

	public GameObject barPrefab;
	private Bar barInstance;

	public GameObject bedroomPrefab;
	private Bedroom bedroomInstance;

	public GameObject poolPrefab;
	private Pool poolInstance;

	public Camera initialCamera;

	private void putPutinInRoom(GenericRoom instance) {
		Transform putinTransform = FindObjectOfType<TurnPutin> ().gameObject.transform;
		Transform playerTransform = GameManager.instance.player.gameObject.transform;
		// Assign putin position
		playerTransform.position = new Vector3 (instance.GetSpawn ().x, 0f, instance.GetSpawn ().z);
		putinTransform.localPosition = new Vector3 (0f, 0f, 0f);
		GameManager.instance.player.GetComponent<Player> ().room = instance;
	}

	public void newRoom() {
		int randomRoom = Random.Range (0, 5);

		switch (randomRoom) {
		case 0:
			// Another corridor
			// Instantiate and generate the corridor
			corridorInstance = Instantiate (corridorPrefab).GetComponent<Corridor> ();
			corridorInstance.Generate ();
			// Put putin in it
			putPutinInRoom (corridorInstance);
			break;
		case 1:
			// Instantiate
			conferenceRoomInstance = Instantiate (conferenceRoomPrefab).GetComponent<ConferenceRoom>();
			// Put putin in it
			putPutinInRoom (conferenceRoomInstance);
			break;
		case 2:
			// Instantiate
			barInstance = Instantiate (barPrefab).GetComponent<Bar> ();
			// Put putin in it
			putPutinInRoom (barInstance);
			break;
		case 3:
			// Instantiate
			bedroomInstance = Instantiate (bedroomPrefab).GetComponent<Bedroom> ();
			// Put putin in it
			putPutinInRoom (bedroomInstance);
			break;
		case 4:
			// Instantiate
			poolInstance = Instantiate (poolPrefab).GetComponent<Pool> ();
			// Put putin in it
			putPutinInRoom (poolInstance);
			break;
		}
	}
}
