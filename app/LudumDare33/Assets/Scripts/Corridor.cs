using UnityEngine;
using System.Collections;

public class Corridor : MonoBehaviour {

	// Size of the grid
	public IntVector2 size;
	// A single cell
	public CorridorCell corridorCellInfundN;
	public CorridorCell corridorCellCornerN_W;
	public CorridorCell corridorCellCorridorN_S;
	public CorridorCell corridorCellWallToE;
	
	// Delay for generation (debug)
	public float generationStepDelay;
	// Probability for a corner
	public float cornerProbability;

	// A random coordinate for this corridor
	public IntVector2 RandomCoordinates {
		get {
			return new IntVector2(Random.Range(0, size.x), Random.Range(0, size.z));
		}
	}

	// The grid
	private CorridorCell[,] cells;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Gets the cell at given coords
	public CorridorCell GetCell(IntVector2 coordinates) {
		if (ContainsCoordinates (coordinates)) {
			return cells [coordinates.x, coordinates.z];
		} else {
			return null;
		}
	}// getcell
	
	// Generate the corridor
	public void Generate() {
		cells = new CorridorCell[size.x, size.z];
		bool turned = false, turnedRecently = false;
		IntVector2 coordinates = RandomCoordinates;
		CorridorDirection nextDirectionName = CorridorDirections.RandomValue;
		CorridorDirection lastDirectionName = nextDirectionName;
		IntVector2 nextDirection = nextDirectionName.ToIntVector2();

		while (ContainsCoordinates(coordinates) && GetCell(coordinates) == null) {
			CreateCell(coordinates, lastDirectionName, nextDirectionName);
			turned = false;
			if(Random.Range(0, 100) < cornerProbability && !turnedRecently) {
				nextDirectionName = CorridorDirections.RandomValue;
				while(nextDirectionName == CorridorDirections.OppositeOf(lastDirectionName)) {
					nextDirectionName = CorridorDirections.RandomValue;
				}
				lastDirectionName = nextDirectionName;
				nextDirection = nextDirectionName.ToIntVector2();
				turned = true;
			}// if
			turnedRecently = turned;
			coordinates += nextDirection;
		}// while
	}// generate

	// Create a single corridor cell
	private void CreateCell (IntVector2 coordinates, CorridorDirection lastDirection, CorridorDirection direction) {
		// Cell's neighbors
		CorridorCell north = GetCell (new IntVector2(coordinates.x, coordinates.z + 1));
		CorridorCell south = GetCell (new IntVector2(coordinates.x, coordinates.z - 1));
		CorridorCell east = GetCell (new IntVector2(coordinates.x + 1, coordinates.z));
		CorridorCell west = GetCell (new IntVector2(coordinates.x - 1, coordinates.z));

		// The new cell
		CorridorCell newCell;
		// If the first cell
		if (north == null && south == null && east == null && west == null) {
			newCell = Instantiate (corridorCellInfundN) as CorridorCell;
			newCell.PointFromTo (lastDirection, direction);
		} else {
			newCell = Instantiate (corridorCellCorridorN_S) as CorridorCell;
			newCell.PointFromTo (lastDirection, direction);
		}
		cells [coordinates.x, coordinates.z] = newCell;
		newCell.material.color = new Color (Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f));
		newCell.name = "Corridor Cell " + coordinates.x + ", " + coordinates.z;
		newCell.transform.parent = transform;
		newCell.transform.localPosition = new Vector3 (coordinates.x - size.x * 0.5f + 0.5f, 
		                                               0f, coordinates.z - size.z * 0.5f + 0.5f);
	}// createCell

	// Checks whether the given coordinates is whithin the corridor
	public bool ContainsCoordinates(IntVector2 coordinate) {
		return coordinate.x >= 0 && coordinate.x < size.x 
			&& coordinate.z >= 0 && coordinate.z < size.z;
	}// containsCoordinates


}
