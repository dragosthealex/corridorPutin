using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Corridor corridorPrefab;
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
}
