using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (new Vector3 (Input.GetAxis ("Horizontal") * speed * Time.deltaTime, 0f, 
		                                  Input.GetAxis ("Vertical") * speed * Time.deltaTime));

	}
}
