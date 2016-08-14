using UnityEngine;
using System.Collections;

public class SceneLevelManager : MonoBehaviour {

	public void setupScene(int level) {
		switch (level) {
		case 0:
			// Start game screen
			// Do nothing
			break;
		case 1:
			// Need to init start room and put putin in, etc
			InitStartRoom ();
			break;
		default:
			// Do nothing
			break;
		}

	}

	private void InitStartRoom() {
		print ("sheet");
		// Get spawn point coords
		GameObject initial_putin = GameObject.FindGameObjectWithTag ("initial_spawn");
		Vector3 pos = initial_putin.transform.position;
		Quaternion rot = initial_putin.transform.rotation;
		// Destroy initial putin
		Destroy(initial_putin.gameObject);
		// Instantiate player
		GameManager.instance.player = Instantiate (GameManager.instance.playerPF, pos, rot) as GameObject;
		// Instantiate canvas
		GameObject canvas_menu = Instantiate(GameManager.instance.canvasMenuPF);
		GameObject charPanel = GameObject.Find("CharPanel");
		// Assign to player and make canvas visible
		GameManager.instance.player.GetComponent<Player> ().charPanel = charPanel;
		charPanel.SetActive (false);

		// Disable mouse
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
