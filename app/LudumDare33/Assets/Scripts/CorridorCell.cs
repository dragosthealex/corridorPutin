using UnityEngine;
using System.Collections;

public class CorridorCell : MonoBehaviour {

	// The coordinates of this cell
	public IntVector2 coordinates;
	// The material
	public Material material;
	// The mesh
	public Mesh mesh;

	// Use this for initialization
	void Awake () {
		material = GetComponentInChildren<Renderer> ().material;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Rotate the cell
	public void PointFromTo(CorridorDirection from, CorridorDirection to) {
		if (from == to) {
			Debug.Log(from + " " + to);
			if (to == CorridorDirection.North)
				transform.rotation = Quaternion.Euler(0, 0, 0);
			else if (to == CorridorDirection.South)
				transform.rotation = Quaternion.Euler(0, 180, 0);
			else if (to == CorridorDirection.East)
				transform.rotation = Quaternion.Euler(0, 90, 0);
			else if (to == CorridorDirection.West)
				transform.rotation = Quaternion.Euler(0, 270, 0);
		}
	}
}
