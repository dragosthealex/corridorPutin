using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DmgDisp : MonoBehaviour {
	
	public Player player;
	public Text tex;
	
	
	void Awake(){
		
		tex = GetComponent<Text>();
	}
	
	void Update(){
		
		if (player == null)
			player = FindObjectOfType<Player>();
		
		tex.text = "Extra DMG: " + player.ExtraDMG;
	}
}
