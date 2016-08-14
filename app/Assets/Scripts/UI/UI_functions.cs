using UnityEngine;
using System.Collections;

public class UI_functions : MonoBehaviour {

	public GameObject gameManager;

	public void Quit(){
		Application.Quit();
	}
	public void Restart() {
		Application.LoadLevel (0);
	}
	public void Resume() {
		gameManager.GetComponent<GameManager> ().unPauseGame ();
	}
	public void TestRoom() {
		Application.LoadLevel (2);
	}
}
