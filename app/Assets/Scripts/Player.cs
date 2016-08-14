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
	public AudioClip painSound;

	public CharPanel panel;
	public bool isPanelactive = false;
	public Camera cam;

	public GameObject vodkaPrefab;
	public GameObject ammoPrefab;
	public GameObject armorPrefab;

	public GenericRoom room;

	public GameObject canvas_menu = null;

	public bool[] weaponsEnabled;
	[SerializeField]
	public GameObject[] weapons;
	public bool hasKnife = false;
	private GameObject currentWeapon = null;
	private GameObject canvas;
	private int delay = 0;
	private float cameraSensitivity;


	// Use this for initialization
	void Start () {

	}
	void Awake() {
		panel = FindObjectOfType<CharPanel> ();
		level = 1;
		currentXP = 0;
		XPtoLVL = 100;
		maxHP = 100;
		currentHP = maxHP;
		ExtraDMG = 1;
		cameraSensitivity = transform.FindChild("putin").GetComponent<ALexCamMovement>().sensitivity;

		for (int i=0; i<weaponsEnabled.Length; i++) {
			weaponsEnabled [i] = false;
		}
		weaponsEnabled [0] = true;
		weaponsEnabled [1] = true;

		EquipWeapon (1);
		hasKnife = false;

		canvas = GameObject.FindWithTag ("canvas");
	}
	
	// Update is called once per frame
	void Update () {
		if (delay % 20 == 0) {
			delay = 0;
		}
//		if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey (KeyCode.A) || Input.GetKey(KeyCode.D)) && !GetComponent<AudioSource>().isPlaying)
//			GetComponent<AudioSource>().Play();
		// XP slider test
		if (Input.GetKeyDown (KeyCode.X))
			GiveXP (10);
		// dmg/slider test
		if (Input.GetKeyDown (KeyCode.G))
			armor = 100;
		if (Input.GetKeyDown (KeyCode.F))
			DamagePlayer (10);
		//show character panel on C press
		if (Input.GetKeyDown (KeyCode.C)) {
			isPanelactive = !isPanelactive;
		}
		if (currentHP > 0 && !this.isPaused ()) {
			this.doGameStuff ();
		}
	}

	public bool isPaused() {
		if (canvas.active) {
			if (isPanelactive) {
				panel.canvasGroup.alpha = 1f;
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
				transform.FindChild ("putin").GetComponent<ALexCamMovement> ().actualSensitivity = 0;
				// Paused, Don't shoot or do anything else
				return true;
			} else {
				panel.canvasGroup.alpha = 0f;
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
				transform.FindChild ("putin").GetComponent<ALexCamMovement> ().actualSensitivity = cameraSensitivity;
				// Not paused, Continue updating
				return false;
			}
		}
		return false;
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

				bulIns = Instantiate (bul, currentWeapon.transform.FindChild ("bar").transform.position, Quaternion.identity) as GameObject;
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
			currentWeapon.transform.SetParent(transform.FindChild("putin").transform.FindChild("weapon").transform);
			currentWeapon.transform.localPosition = Vector3.zero;
			currentWeapon.transform.localRotation = Quaternion.Euler(Vector3.zero);
			if(currentWeapon.GetComponent<Weapon>().name != "knife") {
				bar = currentWeapon.transform.FindChild("bar").transform;
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
		GetComponent<AudioSource> ().Play ();
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

		AudioSource.PlayClipAtPoint (painSound, transform.position);
	}//use when damaging player

	public void setPanelActive(bool value) {
		this.isPanelactive = value;
	}
}
