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

		transform.Translate (new Vector3 (Input.GetAxis ("Horizontal") * speed * Time.deltaTime, 0f, 
		                                  Input.GetAxis ("Vertical") * speed * Time.deltaTime));

		transform.Rotate(new Vector3(0f,Input.GetAxis("Mouse X"),0f));

		if (Input.GetKeyDown(KeyCode.R) && ammo > 0 && mag < 30){

			GetComponent<AudioSource>().Play();

			ammo = ammo - (30-mag);

			if (ammo > 30)
				mag = 30;
			else {
				mag = ammo;
				ammo = 0;
			}

		}

		cam.fieldOfView =Mathf.Clamp(cam.fieldOfView - Input.GetAxis("Mouse ScrollWheel")*20,35,80);


		if (Input.GetKey (KeyCode.Mouse0) && nowTime < Time.time && mag > 0){

			bulIns = Instantiate(bul,bar.transform.position,Quaternion.identity) as GameObject;
			bulIns.GetComponent<Rigidbody>().AddForceAtPosition(bar.transform.right*100,transform.position,ForceMode.Impulse);

			mag--;

			nowTime = Time.time + delayTime;
		}
	}
}
