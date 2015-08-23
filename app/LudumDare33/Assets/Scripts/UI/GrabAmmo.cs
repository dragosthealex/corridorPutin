using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GrabAmmo : MonoBehaviour {

	public Player player;
	public Text tex;


	void Awake(){

		tex = GetComponent<Text>();
	}

	void Update(){

		if (player == null)
			player = FindObjectOfType<Player>();

		tex.text = "AMMO:" + player.mag + " / " + player.ammo;
	}
}
