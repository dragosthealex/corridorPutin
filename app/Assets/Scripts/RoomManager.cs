using UnityEngine;
using System.Collections;

public class RoomManager : MonoBehaviour {

	public Corridor corridorPrefab;
	public ConferenceRoom conferenceRoomPrefab;
	public Bar barPrefab;
	public Bedroom bedroomPrefab;
	public Pool poolPrefab;
	public GameObject startRoomInstance;
	public Camera initialCamera;

	private Corridor corridorInstance;
	private Bar barInstance;
	private Pool poolInstance;

	private Bedroom bedroomInstance;
	private ConferenceRoom conferenceRoomInstance;


	public void newRoom() {
		int randomRoom = Random.Range (0, 5);
		Transform putinTransform = FindObjectOfType<TurnPutin> ().gameObject.transform;
		switch (randomRoom) {
		case 0:
			// Another corridor
			corridorInstance = Instantiate (corridorPrefab) as Corridor;
			corridorInstance.Generate();
			FindObjectOfType<Player> ().gameObject.transform.position = new Vector3(
				corridorInstance.GetSpawn ().x, 0f, corridorInstance.GetSpawn ().z);
			putinTransform.localPosition = new Vector3(0f, 0f, 0f);
			FindObjectOfType<Player> ().room = corridorInstance;
			break;
		case 1:
			conferenceRoomInstance = Instantiate (conferenceRoomPrefab) as ConferenceRoom;
			FindObjectOfType<Player> ().gameObject.transform.position = new Vector3( 
				conferenceRoomInstance.GetSpawn ().x, 0f, conferenceRoomInstance.GetSpawn ().z);
			putinTransform.localPosition = new Vector3(0f, -14f, 0f);
			FindObjectOfType<Player> ().room = conferenceRoomInstance;
			break;
		case 2:
			barInstance = Instantiate (barPrefab) as Bar;
			FindObjectOfType<Player> ().gameObject.transform.position = new Vector3( 
				barInstance.GetSpawn ().x, 0f, barInstance.GetSpawn ().z);
			putinTransform.localPosition = new Vector3(0f, -14f, 0f);
			FindObjectOfType<Player> ().room = barInstance;
			break;
		case 3:
			bedroomInstance = Instantiate (bedroomPrefab) as Bedroom;
			FindObjectOfType<Player> ().gameObject.transform.position = new Vector3( 
				bedroomInstance.GetSpawn ().x, 0f, bedroomInstance.GetSpawn ().z);
			putinTransform.localPosition = new Vector3(0f, 0f, 0f);
			FindObjectOfType<Player> ().room = bedroomInstance;
			break;
		case 4:
			poolInstance = Instantiate (poolPrefab) as Pool;
			FindObjectOfType<Player> ().gameObject.transform.position = new Vector3( 
				poolInstance.GetSpawn ().x, 0f, poolInstance.GetSpawn ().z);
			putinTransform.localPosition = new Vector3(0f, 0f, 0f);
			FindObjectOfType<Player> ().room = poolInstance;
			break;
		}
	}
}
