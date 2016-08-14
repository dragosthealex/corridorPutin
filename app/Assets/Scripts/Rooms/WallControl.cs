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
			FindObjectOfType<Player>().gameObject.transform.position = new Vector3(1000f, 1000f, 1000f);
			if(GameObject.Find("Corridor(Clone)")) {
				Destroy(FindObjectOfType<Corridor>().gameObject);
			}
			if(GameObject.Find("ConferenceRoom(Clone)")) {
				Destroy(FindObjectOfType<ConferenceRoom>().gameObject);
			} 
			if(GameObject.Find("StartRoom")) {
				Destroy(GameObject.Find("StartRoom").gameObject);
			} 
			if(GameObject.Find("Bar(Clone)")) {
				Destroy(GameObject.Find("Bar(Clone)").gameObject);
			}
			if(GameObject.Find("Bedroom(Clone)")) {
				Destroy(GameObject.Find("Bedroom(Clone)").gameObject);
			}
			if(GameObject.Find("Pool(Clone)")) {
				Destroy(GameObject.Find("Pool(Clone)").gameObject);
			}
			GameManager.roomScript.newRoom();
		}
	}
}
