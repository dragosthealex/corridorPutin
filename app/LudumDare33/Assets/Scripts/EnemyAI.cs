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
	public AudioClip meleeDmg;
	public AudioClip deathSound;

	private bool isFollowingPlayer = false;
	private Ray ray;
	private RaycastHit hit;
	private Vector3 lastPlayerPosition;
	private Vector3 origin;

	public GameObject bar1;
	public GameObject bul;
	public GameObject bulIns;

	public float timeNow,timeDelay;

	void Awake () {

		player = FindObjectOfType<Player>();

		gameObject.GetComponent<SphereCollider> ().radius = aggroRange;
		origin = transform.position;

		// Update stats depending on plaer level
		hp += (int)(hp * 3 * (player.level / 100));
		dmg += (int)(dmg * 4 * (player.level / 100));
	}

	void Update (){

		if (hp <= 0){
			player.GiveXP(givenXp);
			player.ammo+=Random.Range(0,100);
			AudioSource.PlayClipAtPoint(deathSound, transform.position);
			Destroy(this.gameObject);
		}
	}

	void OnCollisionEnter (Collision other){
		if (other.gameObject.tag  == "Bullet"){
			hp -= player.damage*player.ExtraDMG;
		}
	}

	// shoots from range
	public void Shooooooooot (){
		bulIns = Instantiate(bul,bar1.transform.position,Quaternion.identity) as GameObject;
		bulIns.GetComponent<EnemyBullet>().damage = dmg ;
		bulIns.GetComponent<Rigidbody>().AddForceAtPosition(bar1.transform.right*50,transform.position,ForceMode.Impulse);
	}

	public void HitMelee() {
		player.DamagePlayer (dmg);

		AudioSource.PlayClipAtPoint (meleeDmg, transform.position);
	}

	void OnTriggerStay(Collider other) {
		if (other.gameObject.tag == "Player") {
			ray = new Ray(transform.position, (other.transform.position - transform.position));
			if(Physics.Raycast(ray, out hit, (float)aggroRange)) {
				if(hit.collider.gameObject.tag == "Player"){
					// hit

					//shoot here somewhererere
					if (ranged && timeNow < Time.time){
						Shooooooooot();
						timeNow = Time.time + timeDelay;
					} else if(timeNow < Time.time && Vector3.Distance(transform.position, other.transform.position) < 3) {
						HitMelee();
						timeNow = Time.time + timeDelay;
					}

					transform.LookAt(other.transform);
					isFollowingPlayer = true;
					lastPlayerPosition = other.transform.position;
					// Move towards player
					if((ranged && Vector3.Distance(transform.position, other.transform.position) > 5) || 
					   (!ranged && Vector3.Distance(transform.position, other.transform.position) > 1)) {
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
