using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MaxHPDisplay : MonoBehaviour {
	
	public Player player;
	public Text tex;
	
	
	void Awake(){
		
		tex = GetComponent<Text>();
	}
	
	void Update(){
		
		if (player == null)
			player = FindObjectOfType<Player>();
		
		tex.text = "Max HP: " + player.maxHP;
	}
}
