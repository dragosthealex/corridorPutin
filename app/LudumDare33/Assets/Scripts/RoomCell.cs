using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomCell : MonoBehaviour {
	
	// The coordinates of this cell
	public IntVector2 coordinates;
	// The material
	public Material material;
	// The floor
	private GameObject floor;
	// If empty
	private bool empty = true;
	
	// Use this for initialization
	void Awake () {
		material = GetComponentInChildren<Renderer> ().material;
		floor = GameObject.FindGameObjectWithTag("floor");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setEmpty() {
		empty = true;
	}

	public void setFull() {
		empty = false;
	}

	public bool isEmpty() {
		return empty;
	}
}
