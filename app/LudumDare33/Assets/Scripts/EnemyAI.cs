using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	public Player player;

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

	public Transform bar1;
	public GameObject bul;
	public GameObject bulIns;

	public float timeNow,timeDelay;

	void Awake () {

		player = FindObjectOfType<Player>();

		gameObject.GetComponent<SphereCollider> ().radius = aggroRange;
		origin = transform.position;
	}

	void Update (){

		if (hp <= 0){
			player.GiveXP(givenXp);
			player.ammo+=Random.Range(0,100);
			Destroy(this.gameObject);
		}
	}

	void OnCollisionEnter (Collision other){
		if (other.gameObject.tag  == "Bullet"){
			Debug.Log(player.damage*player.ExtraDMG);
			hp -= player.damage*player.ExtraDMG;
		}
	}


	public void Shooooooooot (){
		bulIns = Instantiate(bul,bar1.position,Quaternion.identity) as GameObject;
		bulIns.GetComponent<EnemyBullet>().damage = dmg;
		bulIns.GetComponent<Rigidbody>().AddForceAtPosition(bar1.transform.right*50,transform.position,ForceMode.Impulse);
	}

	void OnTriggerStay(Collider other) {
		if (other.gameObject.tag == "Player") {
			ray = new Ray(transform.position, (other.transform.position - transform.position));
			Debug.DrawRay(ray.origin, ray.direction*100, Color.red);
			if(Physics.Raycast(ray, out hit, (float)aggroRange)) {
				if(hit.collider.gameObject.tag == "Player"){
					// hit

					//shoot here somewhererere
					if (timeNow < Time.time){
					Shooooooooot();
						timeNow = Time.time + timeDelay;
					}

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
