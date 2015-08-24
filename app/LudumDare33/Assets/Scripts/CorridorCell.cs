using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CorridorCell : MonoBehaviour {

	// The probability of door per a given wall
	public int wallDoorProbability;
	// The coordinates of this cell
	public IntVector2 coordinates;
	// The material
	public Material material;
	// The floor
	private GameObject floor;
	// The walls
	public GameObject[] walls = new GameObject[4];
	// empty
	private bool empty = true;

	// Use this for initialization
	void Awake () {
		material = GetComponentInChildren<Renderer> ().material;
		floor = GameObject.FindGameObjectWithTag("floor");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PointTo(CorridorDirection to) {
		if (to == CorridorDirection.North) {
			walls [0].SetActive (false);
		} else if (to == CorridorDirection.South) {
			walls [1].SetActive (false);
		} else if (to == CorridorDirection.East) {
			walls [2].SetActive (false);
		} else if (to == CorridorDirection.West) {
			walls [3].SetActive (false);
		}
	}// PointTo

	// Rotate the cell
	public void PointFromTo(CorridorDirection from, CorridorDirection to) {
		if (from == CorridorDirection.North) {
			walls [1].SetActive (false);
		} else if (from == CorridorDirection.South) {
			walls [0].SetActive (false);
		} else if (from == CorridorDirection.East) {
			walls [3].SetActive (false);
		} else if (from == CorridorDirection.West) {
			walls [2].SetActive (false);
		}
		if (to == CorridorDirection.North) {
			walls [0].SetActive (false);
		} else if (to == CorridorDirection.South) {
			walls [1].SetActive (false);
		} else if (to == CorridorDirection.East) {
			walls [2].SetActive (false);
		} else if (to == CorridorDirection.West) {
			walls [3].SetActive (false);
		}

		// Generate the door
		foreach (GameObject wall in walls) {
			if(wall.activeSelf){
				if(Random.Range(0, 100) < wallDoorProbability) {
					// Generate new door
					wall.GetComponent<WallControl>().door = true;
					wall.GetComponent<MeshRenderer>().material.color = Color.blue;
				}
			}// if
		}// foreach
	}// point from to

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
