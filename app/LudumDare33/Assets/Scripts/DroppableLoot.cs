using UnityEngine;
using System.Collections;

public class DroppableLoot : MonoBehaviour {

	private int quantity;
	private string type;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetQuantity(int quantity) {
		this.quantity = quantity;
	}

	public void SetType(string type) {
		this.type = type;
	}

	void OnTriggerEnter (Collider col) {
		if (col.gameObject.tag == "Player") {
			switch (type) {
			case "ammo":
				col.gameObject.transform.parent.gameObject.GetComponent<Player>().ammo += quantity;
				break;
			case "armor":
				col.gameObject.transform.parent.gameObject.GetComponent<Player>().armor += quantity;
				break;
			case "vodka":
				col.gameObject.transform.parent.gameObject.GetComponent<Player>().currentHP += quantity;
				break;
			}
			Destroy(this.gameObject);
		}
	}
}