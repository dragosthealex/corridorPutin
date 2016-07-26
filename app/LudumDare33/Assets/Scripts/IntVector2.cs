using UnityEngine;
using System.Collections;

[System.Serializable]
public struct IntVector2 {

	public int x, z;

	// Constructor
	public IntVector2 (int x, int z) {
		this.x = x;
		this.z = z;
	}// constructor

	// Add two vectors
	public static IntVector2 operator + (IntVector2 a, IntVector2 b) {
		a.x += b.x;
		a.z += b.z;
		return a;
	}// operator +
}