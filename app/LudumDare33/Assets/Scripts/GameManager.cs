using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

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

	private GameObject musicPlayer;

	private Bedroom bedroomInstance;
	private ConferenceRoom conferenceRoomInstance;
	// Player prefab
	public GameObject playerPF;

	//Player
	public GameObject player = null;

	public Camera mainCam;
	public GameObject OLcanvas;
	public GameObject StartRoom;

	private void Awake () {
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	private void Start () {

		OLcanvas = GameObject.FindGameObjectWithTag ("canvas");
		OLcanvas.gameObject.SetActive (false);
		StartRoom = GameObject.FindGameObjectWithTag ("startroom");
		musicPlayer = GameObject.FindGameObjectWithTag ("musicPlayer");

		musicPlayer.GetComponent<MusicPlayer> ().transitionTo (1);
	}

	void Update() {
		// If deaded
		if (player != null && player.GetComponent<Player> ().currentHP == 0) {
			// Restart game
			this.GameOver ();
		}

	}
	private void BeginGame () {
		Vector3 initialPutinPosition = startRoomInstance.transform.FindChild ("putin").transform.position;
		Destroy(startRoomInstance.transform.FindChild ("putin").gameObject);
		Destroy(startRoomInstance.transform.FindChild ("Canvas-MM").gameObject);
		Destroy(startRoomInstance.transform.FindChild ("ak47").gameObject);
		OLcanvas.gameObject.SetActive (true);
		player = Instantiate(playerPF,initialPutinPosition, Quaternion.identity) as GameObject;

		Destroy (initialCamera.gameObject);

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	public void pauseGame() {
		player.GetComponent<Player> ().setPanelActive (true);
	}
	public void unPauseGame() {
		player.GetComponent<Player>().setPanelActive(false);
	}

	public void RestartGame () {
		Application.LoadLevel (0);
	}

	public void GameOver() {
		musicPlayer.GetComponent<MusicPlayer> ().transitionTo (4);
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		Application.LoadLevel (1);
	}

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
	public void Play_game(){
		BeginGame();
		musicPlayer.GetComponent<MusicPlayer> ().transitionTo (2);
	}
}