using UnityEngine;
using System.Collections;

public class ALexCamMovement : MonoBehaviour {

	public float sensitivity;
	public float speed;

	public GameObject camera;

	void Awake (){

		camera = FindObjectOfType<Camera>().gameObject;
	}

	void FixedUpdate (){

		transform.Rotate(0f,Input.GetAxis("Mouse X")*sensitivity,0f);

		camera.transform.Rotate(-Input.GetAxis("Mouse Y")*sensitivity,0f,0f);

		transform.Translate(-Input.GetAxis("Horizontal")*speed,0f,-Input.GetAxis("Vertical")*speed);

		camera.transform.rotation = Quaternion.Euler(new Vector3(Mathf.Clamp(camera.transform.rotation.eulerAngles.x,25f,70f),camera.transform.rotation.eulerAngles.y,0f));
	}
}
