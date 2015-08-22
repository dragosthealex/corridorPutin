using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	void Awake(){
		Destroy(this,2f);
		GetComponent<AudioSource>().Play();
	}

	void OnCollisionEnter  (Collision other){

		Destroy(this);

	}
}
