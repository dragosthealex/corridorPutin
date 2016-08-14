using UnityEngine;
using System.Collections;

public class WallControl : MonoBehaviour {
	public bool door = false;
	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionStay (Collision col)
	{
		if(door && col.gameObject.name == "putin" && Input.GetKey(KeyCode.E))
		{
			GameManager.instance.player.GetComponent<Player> ().gameObject.transform.position = 
				new Vector3(1000f, 1000f, 1000f);
			foreach(GameObject room in GameObject.FindGameObjectsWithTag("room")) {
				Destroy(room);
			}
			GameManager.roomScript.newRoom();
		}
	}
}
