using UnityEngine;
using System.Collections;

public class TurnPutin : MonoBehaviour {
	public float speed = 10f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Turning ();
	}

	void Turning ()
	{
		// Move player
		transform.Translate (new Vector3 (Input.GetAxis ("Horizontal") * -speed * Time.deltaTime, 0f, 
		                                  Input.GetAxis ("Vertical") * -speed * Time.deltaTime));
		// Create a ray from the mouse cursor on screen in the direction of the camera.
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		
		// Create a RaycastHit variable to store information about what was hit by the ray.
		RaycastHit floorHit;
		
		// Perform the raycast and if it hits something on the floor layer...
		if(Physics.Raycast (camRay, out floorHit))
		{
			// Create a vector from the player to the point on the floor the raycast from the mouse hit.
			Vector3 playerToMouse = -(floorHit.point - transform.position);
			//Vector3 playerToMouse = new Vector3(Input.mousePosition.x, 0.0f, Input.mousePosition.y) - transform.position;
			
			// Ensure the vector is entirely along the floor plane.
			playerToMouse.y = 0f;
			transform.LookAt(transform.position + playerToMouse, Vector3.up);
			
		}// if
		Camera.main.gameObject.transform.position = transform.position + new Vector3(0f, 32.1f, 0f);
	}// turning
}
