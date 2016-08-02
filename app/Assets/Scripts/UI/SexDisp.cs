using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SexDisp : MonoBehaviour {
	
	public Player player;
	public Text tex;
	
	
	void Awake(){
		
		tex = GetComponent<Text>();
	}
	
	void Update(){
		
		if (player == null)
			player = FindObjectOfType<Player>();
		
		tex.text = "Sex Appeal: " + player.SexAppeal;
	}
}
