using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {



	private GameObject musicPlayer;



	// Player prefab
	public GameObject playerPF;
	//Player object
	public GameObject player = null;
	// Canvas menu prefab
	public GameObject canvasMenuPF = null;

	// Static instance
	public static GameManager instance = null;
	public static SceneLevelManager sceneScript = null;
	public static RoomManager roomScript = null;

	// "level" = scene
	public int level;
	//

	public Camera mainCam;
	public GameObject OLcanvas;
	public GameObject StartRoom;

	private void Awake () {
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;

		// Enforce singleton
		if (instance == null) {
			// If instance does not exist set this as instance
			instance = this;
		} else if (instance != this) {
			// If instance exists and it is not this one, destroy this
			// to prevent duplicates
			Destroy (gameObject);
		}
		// Prevent this being destroyed
		DontDestroyOnLoad(gameObject);

		// Assign scripts for scene and room mgmt
		sceneScript = GetComponent<SceneLevelManager>();
		roomScript = GetComponent<RoomManager>();

		// Assign delegate stuff
		SceneManager.sceneLoaded += (scene, loadingMode) => {
			level = SceneManager.GetActiveScene().buildIndex;
			InitGame ();
		};
	}

	private void InitGame() {
		sceneScript.setupScene (level);
	}
		
	public void BeginGame () {
		level = 1;
		SceneManager.LoadScene (level);
	}

	public void RestartGame () {
		level = 0;
		SceneManager.LoadScene (level);
	}

	public void QuitGame() {
		Application.Quit ();
	}

	void Update() {
		// If deaded
		if (player != null && player.GetComponent<Player> ().currentHP == 0) {
			// Restart game
			this.GameOver ();
		}
	}

	public void pauseGame() {
		player.GetComponent<Player> ().setPanelActive (true);
	}
	public void unPauseGame() {
		player.GetComponent<Player>().setPanelActive(false);
	}



	public void GameOver() {
		musicPlayer.GetComponent<MusicPlayer> ().transitionTo (4);
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		Application.LoadLevel (1);
	}
}