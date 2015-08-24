using UnityEngine;
using System.Collections;

public class ALexCamMovement : MonoBehaviour {

	public float sensitivity;
	public float actualSensitivity;
	public float speed;

	public GameObject camera;

public float fixer = 1f;

	void Awake (){
		camera = FindObjectOfType<Camera>().gameObject;
		actualSensitivity = sensitivity;
	}

	void FixedUpdate (){


		transform.Rotate(0f,Input.GetAxis("Mouse X")*actualSensitivity,0f);
		camera.transform.Rotate (-Input.GetAxis ("Mouse Y") * actualSensitivity, 0f, 0f);

		transform.Translate(-Input.GetAxis("Horizontal")*speed,0f,-Input.GetAxis("Vertical")*speed);

		camera.transform.rotation = Quaternion.Euler(new Vector3(Mathf.Clamp(camera.transform.rotation.eulerAngles.x,25f,70f),
		                                camera.transform.rotation.eulerAngles.y,0f));

		// MEGA MASTER AWUSUM FIXs
		if (camera.transform.localRotation.eulerAngles.y > 180f) {
			camera.transform.localRotation = Quaternion.Euler (camera.transform.rotation.eulerAngles.x, 175.5f, 0f); 
		}
	}
}
