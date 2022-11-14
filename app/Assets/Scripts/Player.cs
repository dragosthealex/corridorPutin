using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	public Transform putinTrans;

	//  ENJOY THE LEGACY CODE

	public float speed;
	public GameObject bul;
	private GameObject bulIns;
	public Transform bar;

	public int mag,ammo;
	public float nowTime, delayTime;

	public int maxHP;
	public int currentHP;

	public int maxArmor;
	public int armor;

	public int level;
	public int currentXP;
	public int XPtoLVL;
	public int ExtraDMG;
	public int SexAppeal;
	public int damage;

	// Sound stuffs
	public AudioClip painSound;
	public AudioClip reloadSound;
	private AudioSource source;

	public Camera cam;

	public GameObject vodkaPrefab;
	public GameObject ammoPrefab;
	public GameObject armorPrefab;

	public GenericRoom room;
	public GameObject currentRoom;

	// The menu stuffs
	public GameObject charPanel = null;
	public bool charPanelActive = false;
	public bool inventoryPanelActive = false; // for future stuffs

	public bool[] weaponsEnabled;
	[SerializeField]
	public GameObject[] weapons;
	public bool hasKnife = false;
	private GameObject currentWeapon = null;
	private int delay = 0;

	// Use this for initialization
	void Start () {

	}
	void Awake() {
		level = 1;
		currentXP = 0;
		XPtoLVL = 100;
		maxHP = 100;
		currentHP = maxHP;
		ExtraDMG = 1;

		for (int i=0; i<weaponsEnabled.Length; i++) {
			weaponsEnabled [i] = false;
		}
		weaponsEnabled [0] = true;
		weaponsEnabled [1] = true;

		EquipWeapon (1);
		hasKnife = false;

		// Assign audio source
		source = GetComponent<AudioSource>();
		source.clip = null;
	}
	
	// Update is called once per frame
	void Update () {
		if (delay % 20 == 0) {
			delay = 0;
		}

		// XP slider test
		if (Input.GetKeyDown (KeyCode.X))
			GiveXP (10);
		// dmg/slider test
		if (Input.GetKeyDown (KeyCode.G))
			armor = 100;
		if (Input.GetKeyDown (KeyCode.F))
			DamagePlayer (10);
		
		// If we still have health and the game is not paused, do stuffs
		if (currentHP > 0 && !GameManager.instance.paused) {
			this.doGameStuff ();
			if (Input.GetKeyDown (KeyCode.C)) {
				//show character panel on C press and pause
				showCharPanel ();
				GameManager.instance.pauseGame ();
			} else if (Input.GetKeyDown (KeyCode.P)) {
				// Just pause the game
				GameManager.instance.pauseGame ();
			}
		} else if (currentHP > 0) {
			// Else we can unpause the game
			// or switch different pannels
			// We are either in pause, char panel or other panels
			if (Input.GetKeyDown (KeyCode.C) && charPanelActive) {
				//hide character panel on C press and unpause
				hideCharPanel ();
			} else if (Input.GetKeyDown (KeyCode.C)) {
				showCharPanel ();
			} else if (Input.GetKeyDown (KeyCode.P)) {
				hideCharPanel ();
			}
		} else {
			// Deaded
			GameManager.instance.GameOver();
		}
	}

	private void showCharPanel() {
		charPanelActive = true;
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		charPanel.SetActive (true);
		GameManager.instance.pauseGame ();
	}
	public void hideCharPanel() {
		charPanelActive = false;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		charPanel.SetActive (false);
		GameManager.instance.unPauseGame ();
	}

	private void doGameStuff() {
		// Reload
		if (Input.GetKeyDown (KeyCode.R) && ammo > 0 && mag < 30) {
			Reload ();
		}
		if (Input.GetKeyDown (KeyCode.Alpha1))
			EquipWeapon (0);
		if (Input.GetKeyDown (KeyCode.Alpha2))
			EquipWeapon (1);
		if (Input.GetKeyDown (KeyCode.Alpha3))
			EquipWeapon (2);
		if (Input.GetKeyDown (KeyCode.Alpha4))
			EquipWeapon (3);
		if (Input.GetKeyDown (KeyCode.Alpha5))
			EquipWeapon (4);

		// Zoom
		cam.fieldOfView =Mathf.Clamp(cam.fieldOfView - Input.GetAxis("Mouse ScrollWheel")*20,35,80);

		// Shoot i guess
		if (currentWeapon.GetComponent<Weapon> ().name != "knife") {

			if (Input.GetKey (KeyCode.Mouse0) && nowTime < Time.time && mag > 0) {

				bulIns = Instantiate (bul, currentWeapon.transform.Find ("bar").transform.position, Quaternion.identity) as GameObject;
				bulIns.GetComponent<Rigidbody> ().AddForceAtPosition (bar.transform.right * 100, transform.position, ForceMode.Impulse);
				mag--;
				nowTime = Time.time + delayTime;
				delay = 0;
			}// if
		} else {
			GameObject knife = currentWeapon;
			if (Input.GetKeyDown (KeyCode.Mouse0) && nowTime < Time.time) {
				knife.transform.localRotation = Quaternion.Euler (90f, 90f, 0f);
				delay = 0;
				hasKnife = true;
				nowTime = Time.time + delayTime;
			}
			if(delay == 5){
				knife.transform.localRotation = Quaternion.Euler (0f, 90f, 0f);
				delay = 0;
				hasKnife = false;
			}
			delay ++;
		}
	}// update
	
	public void EquipWeapon (int value) {
		if (value >= 0 && value < weaponsEnabled.Length && weaponsEnabled [value]) {
			if(currentWeapon != null)
				Destroy(currentWeapon.gameObject);
			currentWeapon = weapons [value];
			currentWeapon = Instantiate(currentWeapon) as GameObject;
			currentWeapon.transform.SetParent(transform.Find("putin").transform.Find("weapon").transform);
			currentWeapon.transform.localPosition = Vector3.zero;
			currentWeapon.transform.localRotation = Quaternion.Euler(Vector3.zero);
			if(currentWeapon.GetComponent<Weapon>().name != "knife") {
				bar = currentWeapon.transform.Find("bar").transform;
			}
			else {
				currentWeapon.transform.localRotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
			}
			this.damage = currentWeapon.GetComponent<Weapon>().damage;
		}
		else {
			EquipWeapon(1);
		}
	}
	private void Reload() {
		source.clip = reloadSound;
		source.Play ();

		int ammoToSubstract = 30 - mag;
		mag = ammo >= ammoToSubstract ? 30 : mag + ammo;
		ammo = ammo >= ammoToSubstract ? ammo - ammoToSubstract : 0;
	}// Reload

	public void GiveXP(int amount) {
		currentXP += amount;
		if (currentXP >= XPtoLVL) {
			currentXP -= XPtoLVL;
			LevelUp();
		}
	}
	public void LevelUp(){
		level++;
		ExtraDMG += 2;
		maxHP += (int)(maxHP * 0.1 * level);
		maxArmor += (int)(maxArmor * 0.1 * level);
		XPtoLVL =(int)(XPtoLVL*1.5);
	}
	public void DamagePlayer(int amount) {
		if (armor >= amount) {
			armor -= amount;
		} else {
			amount -= armor;
			armor = 0;
		}
		if (currentHP >= amount) {
			currentHP -= amount;
		} else {
			currentHP = 0;
		}
		source.clip = painSound;
		source.Play ();

	}//use when damaging player
}
