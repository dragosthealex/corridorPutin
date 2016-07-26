using UnityEngine;
using System.Collections;

public class newCamera : MonoBehaviour {

	public Transform target;
	public float smoothTime = 0.3f;
	private Vector3 velocity = Vector3.zero;
	private bool colliding = false;
	private Vector3 putinPosition;
	private Vector3 putinOffset, originOffset, origin;

	void Awake() {
		putinPosition = FindObjectOfType<TurnPutin> ().transform.position;
		putinOffset = putinPosition - transform.position;
		transform.localScale = new Vector3 (1f, 1f, 1f);
		origin = putinPosition;
		originOffset = origin - putinPosition;
	}
	// Use this for initialization
	void Start () {
	}
	public void OnTriggerEnter (){ 
		colliding = true;
	}

	public void OnTriggerExit (){ 
		colliding = false; 
	}
	// Update is called once per frame
	void Update () {
		Vector3 newPos = FindObjectOfType<TurnPutin> ().transform.position;
		if (newPos.x != putinPosition.x || newPos.z != putinPosition.z) {
			putinPosition = FindObjectOfType<TurnPutin> ().transform.position;
			originOffset = origin - putinPosition;
		}

		if (colliding){ 
			transform.localPosition = Vector3.SmoothDamp(transform.localPosition, 
			                          new Vector3(0f,1f,0f) - putinOffset - originOffset, ref velocity, smoothTime);
			print ("shit");

		} else{ 
			RaycastHit hit; 
			if (Physics.Raycast(transform.position, -transform.forward, out hit, 0.01f)){ 
				print ("ray has hit");
			} else{ 
				transform.localPosition = Vector3.SmoothDamp(transform.localPosition, 
				                      new Vector3(0f,0f,0f) - putinOffset - originOffset, ref velocity, smoothTime); 
			} 
		} 
	}
}
