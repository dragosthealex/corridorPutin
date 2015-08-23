using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {


	//  ENJOY THE LEGACY CODE

	public float speed;
	public GameObject bul;
	private GameObject bulIns;
	public Transform bar;

	public int mag,ammo;

	public float nowTime, delayTime;

	public Camera cam;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

//		if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey (KeyCode.A) || Input.GetKey(KeyCode.D)) && !GetComponent<AudioSource>().isPlaying)
//			GetComponent<AudioSource>().Play();



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

		if (Input.GetKey (KeyCode.Q)) {
			FindObjectOfType<Camera> ().gameObject.transform.RotateAround (FindObjectOfType<TurnPutin>().gameObject.transform.position, Vector3.up, -1f);
		}
		if (Input.GetKey (KeyCode.E)) {
			FindObjectOfType<Camera> ().gameObject.transform.RotateAround (FindObjectOfType<TurnPutin>().gameObject.transform.position, Vector3.up, 1f);
		}
	}// update

	private void Reload() {
		GetComponent<AudioSource> ().Play ();
		int ammoToSubstract = 30 - mag;
		mag = ammo >= ammoToSubstract ? 30 : mag + ammo;
		ammo = ammo >= ammoToSubstract ? ammo - ammoToSubstract : 0;
	}// Reload
}
