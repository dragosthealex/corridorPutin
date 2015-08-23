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
		if(col.gameObject.name == "putin")
		{
			Destroy(col.gameObject);
			Destroy(FindObjectOfType<Corridor>());
			FindObjectOfType<GameManager>().newRoom();
		}
	}
}
