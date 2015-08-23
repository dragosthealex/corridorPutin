using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Hpslider : MonoBehaviour {
	public Player player;
	public float val;

	// Use this for initialization
	void Start () {
		
	}
	void Awake() {
		val = GetComponent<Slider> ().value;
	}
	// Update is called once per frame
	void Update () {
		if (player == null)
			player = FindObjectOfType<Player> ();
		GetComponent<Slider> ().value = player.currentHP;
	}
}
