using UnityEngine;
using System.Collections;

public static class CorridorDirections {

	public const int Count = 4;

	private static IntVector2[] vectors = {
		new IntVector2 (0, 1),
		new IntVector2 (1, 0),
		new IntVector2 (0, -1),
		new IntVector2 (-1, 0)
	};

	// Gets a random direction
	public static CorridorDirection RandomValue {
		get {
			return (CorridorDirection)Random.Range (0, Count);
		}
	}// RandomValue

	// Converts into int vectors
	public static IntVector2 ToIntVector2 (this CorridorDirection direction) {
		return vectors [(int)direction];
	}

	// Gets the inverse 
	public static CorridorDirection OppositeOf(CorridorDirection direction) {
		switch (direction) {
		case CorridorDirection.North:
			return CorridorDirection.South;
		case CorridorDirection.South:
			return CorridorDirection.North;
		case CorridorDirection.East:
			return CorridorDirection.West;
		case CorridorDirection.West:
			return CorridorDirection.East;
		default:
			return CorridorDirection.North;
		}// switch
 	}// inverse
}
