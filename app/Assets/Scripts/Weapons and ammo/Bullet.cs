using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	void Awake(){
		Destroy(this.gameObject,2f);
		GetComponent<AudioSource>().Play();
	}

	void OnCollisionEnter  (Collision other){

		Destroy(GetComponent<Renderer>());
		Destroy(GetComponent<Collider>());
		Destroy(GetComponent<TrailRenderer>());
	}
}
