using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	// Use this for initialization
	public int hp;
	public int dmg;
	public int givenXp;
	public bool ranged = true;
	public int aggroRange;
	public int moveSpeed;
	private bool isFollowingPlayer = false;
	private Ray ray;
	private RaycastHit hit;
	private Vector3 lastPlayerPosition;
	private Vector3 origin;

	public bool justDOIT = false;
	public Collider playerCollider;

	void Awake () {
		gameObject.GetComponent<SphereCollider> ().radius = aggroRange;
		origin = transform.position;
	}

	void FixedUpdate (){

		if (justDOIT){
			ray = new Ray(transform.position, (playerCollider.transform.position - transform.position));
			Debug.DrawRay(ray.origin, ray.direction*100, Color.red);
			if(Physics.Raycast(ray, out hit, (float)aggroRange)) {
				if(hit.collider.gameObject.tag == "Player"){
					// hit
					Debug.Log("SHIT");
					transform.LookAt(playerCollider.transform);
					isFollowingPlayer = true;
					lastPlayerPosition = playerCollider.transform.position;
					// Move towards player
					if(ranged && Vector3.Distance(transform.position, playerCollider.transform.position) > 5) {
						transform.position = Vector3.MoveTowards(transform.position, playerCollider.transform.position, Time.deltaTime * moveSpeed);
					}
				} else if(isFollowingPlayer && transform.position != lastPlayerPosition){
					transform.position = Vector3.MoveTowards(transform.position, lastPlayerPosition, Time.deltaTime * moveSpeed);
				} else {
					isFollowingPlayer = false;
				}
			}

		}
	}

	void OnTriggerExit(Collider other){
		justDOIT = false;
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			playerCollider = other;
			justDOIT = true;
		}// if
	}// on trigger stay
}
