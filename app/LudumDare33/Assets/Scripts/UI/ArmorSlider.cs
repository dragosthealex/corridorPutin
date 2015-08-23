using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ArmorSlider : MonoBehaviour {
	public Player player;
	public float val;
	private CanvasGroup canvasGroup;
	// Use this for initialization
	void Awake() {
		canvasGroup = FindObjectOfType<CanvasGroup> ();
	}
	// Update is called once per frame
	void Update () {

		if (player == null)
			player = FindObjectOfType<Player> ();
		if (player.armor != 0){
			canvasGroup.alpha = 1f;
			GetComponent<Slider> ().value = player.armor;
		}
		else {
			canvasGroup.alpha = 0f;
		}
	}
}
