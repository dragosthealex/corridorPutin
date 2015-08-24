using UnityEngine;
using System.Collections;

public class UI_functions : MonoBehaviour {

	public void Quit(){
		Application.Quit();
	}
	public void Play_game(){
		GameObject.FindGameObjectWithTag ("startroom").SetActive (false);
		Camera.main.gameObject.SetActive (true);
		GameObject.FindGameObjectWithTag ("canvas").SetActive (true);
	}
}
