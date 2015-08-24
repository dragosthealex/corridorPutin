using UnityEngine;
using System.Collections;

public class ALexCamMovement : MonoBehaviour {

	public float sensitivity;
	public float speed;

	public GameObject camera;

public float fixer = 1f;

	void Awake (){
		camera = FindObjectOfType<Camera>().gameObject;
	}

	void FixedUpdate (){
	
		transform.Rotate(0f,Input.GetAxis("Mouse X")*sensitivity,0f);

//		if (camera.transform.rotation.eulerAngles.x < 30 || camera.transform.rotation.eulerAngles.x > 65)
//			fixer = 0.01f;
//		else if (camera.transform.rotation.eulerAngles.x >= 30 && camera.transform.rotation.eulerAngles.x <= 65)
//			fixer = 1f;

		camera.transform.Rotate(-Input.GetAxis("Mouse Y")*sensitivity*fixer,0f,0f);





		transform.Translate(-Input.GetAxis("Horizontal")*speed,0f,-Input.GetAxis("Vertical")*speed);

//		camera.transform.rotation = Quaternion.Euler(new Vector3(Mathf.Clamp(camera.transform.rotation.eulerAngles.x,25f,70f),camera.transform.rotation.eulerAngles.y,0f));
//		camera.transform.rotation = Quaternion.Euler(new Vector3(Mathf.Clamp(camera.transform.rotation.eulerAngles.x,25f,70f),175f,0f));

		camera.transform.rotation = Quaternion.Euler(new Vector3(Mathf.Clamp(camera.transform.rotation.eulerAngles.x,25f,70f),camera.transform.rotation.eulerAngles.y,0f));

//		camera.transform.localRotation = Quaternion.Euler(camera.transform.localRotation.x,175f,camera.transform.localRotation.z);

	}
}
