using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour {

	public int damage;

	void Awake(){
		Destroy(this.gameObject,2f);
		GetComponent<AudioSource>().Play();
	}

	void OnCollisionEnter (Collision other){

		if (other.gameObject.tag == "Player") {
			FindObjectOfType<Player> ().DamagePlayer (damage);
			Destroy (this.gameObject);
		}
	}
}
