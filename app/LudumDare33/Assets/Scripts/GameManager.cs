using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Corridor corridorPrefab;
	private Corridor corridorInstance;

	private void Start () {
		BeginGame();
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
}
