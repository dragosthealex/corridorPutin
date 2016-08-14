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
			Player player = col.gameObject.transform.parent.gameObject.GetComponent<Player> ();
			switch (type) {
			case "ammo":
				player.ammo += quantity;
				break;
			case "armor":
				if (player.armor + quantity >= player.maxArmor) {
					player.armor = player.maxArmor;
				} else {
					player.armor += quantity;
				}
				break;
			case "vodka":
				if (player.currentHP + quantity >= player.maxHP) {
					player.currentHP = player.maxHP;
				} else {
					player.currentHP += quantity;
				}
				break;
			}
			Destroy(this.gameObject);
		}
	}
}