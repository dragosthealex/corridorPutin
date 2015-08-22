using UnityEngine;
using System.Collections;

public class CorridorCell : MonoBehaviour {

	// The coordinates of this cell
	public IntVector2 coordinates;
	// The material
	public Material material;

	// Use this for initialization
	void Awake () {
		material = GetComponentInChildren<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
