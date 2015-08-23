using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class XPSlider : MonoBehaviour {
	public Player player;
	
	// Use this for initialization
	void Awake () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (player == null)
			player = FindObjectOfType<Player> ();
		GetComponent<Slider> ().value = player.currentXP;
		GetComponent<Slider> ().maxValue = player.XPtoLVL;
	}
}
