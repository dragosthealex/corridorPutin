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

		if (Input.GetKeyDown(KeyCode.Z))
			FindObjectOfType<Camera>().gameObject.transform.LookAt(putinTrans.position);


		if (Input.GetKey (KeyCode.Q)) {
<<<<<<< HEAD
			FindObjectOfType<Camera> ().gameObject.transform.RotateAround (FindObjectOfType<TurnPutin>().gameObject.transform.position, Vector3.up, -1f);
		}
		if (Input.GetKey (KeyCode.E)) {
			FindObjectOfType<Camera> ().gameObject.transform.RotateAround (FindObjectOfType<TurnPutin>().gameObject.transform.position, Vector3.up, 1f);
=======
			FindObjectOfType<Camera> ().gameObject.transform.RotateAround (putinTrans.position, Vector3.up, -1f);
		}
		if (Input.GetKey (KeyCode.E)) {
			FindObjectOfType<Camera> ().gameObject.transform.RotateAround (putinTrans.position, Vector3.up, 1f);
>>>>>>> 1e3dd63bd792daef1a7a3a99a23d8f32a8d5c4e0
		}
	}// update

	private void Reload() {
		GetComponent<AudioSource> ().Play ();
		int ammoToSubstract = 30 - mag;
		mag = ammo >= ammoToSubstract ? 30 : mag + ammo;
		ammo = ammo >= ammoToSubstract ? ammo - ammoToSubstract : 0;
	}// Reload
}
