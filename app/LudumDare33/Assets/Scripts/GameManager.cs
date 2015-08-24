using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Corridor corridorPrefab;
	public ConferenceRoom conferenceRoomPrefab;
	public Bar barPrefab;
	public GameObject startRoomInstance;
	public Camera initialCamera;
	private Corridor corridorInstance;
	private Bar barInstance;
	private ConferenceRoom conferenceRoomInstance;
	// Player prefab
	public GameObject playerPF;

	//Player
	public GameObject player;

	public Camera mainCam;
	public GameObject OLcanvas;
	public GameObject StartRoom;
	private void Start () {
		OLcanvas = GameObject.FindGameObjectWithTag ("canvas");
		OLcanvas.gameObject.SetActive (false);
		StartRoom = GameObject.FindGameObjectWithTag ("startroom");
	}
	
	private void Update () {

	}
	private void BeginGame () {
		Vector3 initialPutinPosition = startRoomInstance.transform.FindChild ("putin").transform.position;
		Destroy (startRoomInstance.transform.FindChild ("putin").gameObject);
		Destroy (startRoomInstance.transform.FindChild ("ak47").gameObject);
		Destroy (startRoomInstance.transform.FindChild ("Canvas-MM").gameObject);
		OLcanvas.gameObject.SetActive (true);
		player = Instantiate(playerPF,initialPutinPosition, Quaternion.identity) as GameObject;

		Destroy (initialCamera.gameObject);

	}
	
	private void RestartGame () {
		Destroy (corridorInstance.gameObject);
		BeginGame();
	}

	public void newRoom() {
		int randomRoom = Random.Range (0, 3);
		Transform putinTransform = FindObjectOfType<TurnPutin> ().gameObject.transform;
		switch (randomRoom) {
		case 0:
			// Another corridor
			corridorInstance = Instantiate (corridorPrefab) as Corridor;
			corridorInstance.Generate();
			FindObjectOfType<Player> ().gameObject.transform.position = new Vector3(
				corridorInstance.GetSpawn ().x, 0f, corridorInstance.GetSpawn ().z);
			putinTransform.localPosition = new Vector3(0f, 0f, 0f);
			break;
		case 1:
			conferenceRoomInstance = Instantiate (conferenceRoomPrefab) as ConferenceRoom;
			FindObjectOfType<Player> ().gameObject.transform.position = new Vector3( 
			    conferenceRoomInstance.GetSpawn ().x, 0f, conferenceRoomInstance.GetSpawn ().z);
			putinTransform.localPosition = new Vector3(0f, -14f, 0f);
			break;
		case 2:
			barInstance = Instantiate (barPrefab) as Bar;
			FindObjectOfType<Player> ().gameObject.transform.position = new Vector3( 
			     barInstance.GetSpawn ().x, 0f, barInstance.GetSpawn ().z);
			putinTransform.localPosition = new Vector3(0f, -14f, 0f);
			break;
		}
	}
	public void Play_game(){
		BeginGame();
	}
}