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
	private bool isDead = false;

	public GameObject bar1;
	public GameObject bul;
	public GameObject bulIns;

	private GameObject vodkaInstance;
	private GameObject ammoInstance;
	private GameObject armorInstance;

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

		if (hp <= 0 && !isDead) {
			player.GiveXP (givenXp);
			AudioSource.PlayClipAtPoint (deathSound, transform.position);
			dropLoot ();
			isDead = true;
		} else if (isDead) {
			Destroy (this.gameObject);
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

	public void dropLoot() {
		switch (Random.Range (0, 1)) {
		case 0:
			vodkaInstance = Instantiate (player.vodkaPrefab) as GameObject;
			vodkaInstance.transform.position = this.transform.position;
			vodkaInstance.GetComponent<DroppableLoot>().SetQuantity(Random.Range(10, player.maxHP/2));
			vodkaInstance.GetComponent<DroppableLoot>().SetType("vodka");
			vodkaInstance.transform.SetParent(player.room.transform);
			break;
		case 1:
			ammoInstance = Instantiate (player.ammoPrefab) as GameObject;
			ammoInstance.transform.position = this.transform.position;
			ammoInstance.GetComponent<DroppableLoot>().SetQuantity(Random.Range(10, 200));
			ammoInstance.GetComponent<DroppableLoot>().SetType("ammo");
			ammoInstance.transform.SetParent(player.room.transform);
			break;
		case 2:
			armorInstance = Instantiate (player.armorPrefab) as GameObject;
			armorInstance.transform.position = this.transform.position;
			armorInstance.GetComponent<DroppableLoot>().SetQuantity(Random.Range(10, 200));
			armorInstance.GetComponent<DroppableLoot>().SetType("armor");
			armorInstance.transform.SetParent(player.room.transform);
			break;
		}
	}// droploot
}
