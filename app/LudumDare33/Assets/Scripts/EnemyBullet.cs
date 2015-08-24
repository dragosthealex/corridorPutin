using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour {

	public int damage;

	void Awake(){
		Destroy(this.gameObject,2f);
		GetComponent<AudioSource>().Play();
	}

	void OnCollisionEnter (Collision other){

		Debug.Log(other.gameObject.name);

		if (other.gameObject.tag == "Player")
			other.gameObject.GetComponent<Player>().DamagePlayer(damage);
	}
}
