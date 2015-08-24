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

	void Awake () {
		gameObject.GetComponent<SphereCollider> ().radius = aggroRange;
		origin = transform.position;
	}

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay (Collider other) {
		if (other.gameObject.tag == "Player") {
			ray = new Ray(transform.position, (other.transform.position - transform.position));
			Debug.DrawRay(ray.origin, ray.direction*100, Color.red);
			if(Physics.Raycast(ray, out hit, (float)aggroRange)) {
				if(hit.collider.gameObject.tag == "Player"){
					// hit
					Debug.Log("SHIT");
					transform.LookAt(other.transform);
					isFollowingPlayer = true;
					lastPlayerPosition = other.transform.position;
					// Move towards player
					if(ranged && Vector3.Distance(transform.position, other.transform.position) > 5) {
						transform.position = Vector3.MoveTowards(transform.position, other.transform.position, Time.deltaTime * moveSpeed);
					}
				} else if(isFollowingPlayer && transform.position != lastPlayerPosition){
					transform.position = Vector3.MoveTowards(transform.position, lastPlayerPosition, Time.deltaTime * moveSpeed);
				} else {
					isFollowingPlayer = false;
				}
			}
		}// if
	}// on trigger stay
}
