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

	void OnCollisionEnter (Collision col)
	{
		if(door && col.gameObject.name == "putin")
		{
			Destroy(FindObjectOfType<Player>().gameObject);
			Destroy(FindObjectOfType<Corridor>().gameObject);
			FindObjectOfType<GameManager>().newRoom();
		}
	}
}
