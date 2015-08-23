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

	public int maxHP = 100;
	public int currentHP;
	public Slider HPSlider;

	public Camera cam;

	// Use this for initialization
	void Start () {

	}
	void Awake() {
		maxHP = 100;
		currentHP = maxHP;
	}
	
	// Update is called once per frame
	void Update () {

//		if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey (KeyCode.A) || Input.GetKey(KeyCode.D)) && !GetComponent<AudioSource>().isPlaying)
//			GetComponent<AudioSource>().Play();
		// dmg/slider test
		if(Input.GetKeyDown (KeyCode.F))
		   DamagePlayer(10);


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

		if (Input.GetKeyDown(KeyCode.Z))
			FindObjectOfType<Camera>().gameObject.transform.LookAt(putinTrans.position);


		if (Input.GetKey (KeyCode.Q)) {
			FindObjectOfType<Camera> ().gameObject.transform.RotateAround (putinTrans.position, Vector3.up, -1f);
		}
		if (Input.GetKey (KeyCode.E)) {
			FindObjectOfType<Camera> ().gameObject.transform.RotateAround (putinTrans.position, Vector3.up, 1f);
		}
	}// update

	private void Reload() {
		GetComponent<AudioSource> ().Play ();
		int ammoToSubstract = 30 - mag;
		mag = ammo >= ammoToSubstract ? 30 : mag + ammo;
		ammo = ammo >= ammoToSubstract ? ammo - ammoToSubstract : 0;
	}// Reload

	private void DamagePlayer(int amount) {
		currentHP -= amount;
	}//use when damaging player
}
