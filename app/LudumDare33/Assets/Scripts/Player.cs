using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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
		//ExtraDMG = 0;
		SexAppeal = 0;
	}
	
	// Update is called once per frame
	void Update () {

//		if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey (KeyCode.A) || Input.GetKey(KeyCode.D)) && !GetComponent<AudioSource>().isPlaying)
//			GetComponent<AudioSource>().Play();
		// XP slider test
		if (Input.GetKeyDown (KeyCode.X))
			GiveXP (10);
		// dmg/slider test
		if (Input.GetKeyDown (KeyCode.G))
			armor = 100;
		if(Input.GetKeyDown (KeyCode.F))
		   DamagePlayer(10);
		//show character panel on C press
		if (Input.GetKeyDown (KeyCode.C)) {
			isPanelactive = !isPanelactive;
		}
		if (GameObject.FindWithTag ("canvas")) {
			if (isPanelactive) {
				panel.canvasGroup.alpha = 1f;
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}
			else {
				panel.canvasGroup.alpha = 0f;
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}
		}
		// Reload
		if (Input.GetKeyDown (KeyCode.R) && ammo > 0 && mag < 30) {
			Reload ();
		}

		// Zoom
		cam.fieldOfView =Mathf.Clamp(cam.fieldOfView - Input.GetAxis("Mouse ScrollWheel")*20,35,80);

		// Shoot i guess
		if (Input.GetKey (KeyCode.Mouse0) && nowTime < Time.time && mag > 0){

			bulIns = Instantiate(bul,bar.transform.position,Quaternion.identity) as GameObject;
			bulIns.GetComponent<Rigidbody>().AddForceAtPosition(bar.transform.right*100,transform.position,ForceMode.Impulse);
			mag--;
			nowTime = Time.time + delayTime;
		}// if

	
	}// update

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
		maxHP += 5;
		XPtoLVL =(int)(XPtoLVL*1.5);
	}
	public void DamagePlayer(int amount) {
		if (armor >= amount)
			armor -= amount;
		else 
			currentHP -= (amount-armor);
		AudioSource.PlayClipAtPoint (painSound, transform.position);
	}//use when damaging player
}
