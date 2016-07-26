using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CorridorCell : MonoBehaviour {

	// The coordinates of this cell
	public IntVector2 coordinates;
	// The material
	public Material material;
	// The floor
	private GameObject floor;
	// The walls
	public GameObject[] walls = new GameObject[4];

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
		Debug.Log("FROM:" + from + " TO:" + to);
		Debug.Log ("STUFF: " + CorridorDirection.North);
		Debug.Log ("walls[0]: " + walls [0]);
		Debug.Log ("walls[1]: " + walls [1]);
		Debug.Log ("walls[2]: " + walls [2]);
		Debug.Log ("walls[3]: " + walls [3]);

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
	}// point from to
}
