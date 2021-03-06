﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Corridor : MonoBehaviour {

	// Size of the grid
	public IntVector2 size;
	// A single cell
	public CorridorCell cell;
	
	// Delay for generation (debug)
	public float generationStepDelay;
	// Probability for a corner
	public float cornerProbability;

	// A random coordinate for this corridor
	public IntVector2 RandomCoordinates {
		get {
			return new IntVector2(Random.Range(10, size.x-10), Random.Range(10, size.z-10));
		}
	}

	// The grid
	private CorridorCell[,] cells;
	// Dictionary with the generated cells
	//private  ArrayList cellList = new ArrayList();
	public List<CorridorCell> cellList = new List<CorridorCell>();

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
		// Current coordinates, initialised with -1, -1
		IntVector2 currentCoordinates;
		// Coordinates to generate the next cell on
		IntVector2 nextCoordinates = RandomCoordinates;
		// The direction to generate the next cell to
		CorridorDirection nextDirection = CorridorDirections.RandomValue;
		// Direction of the current cell
		CorridorDirection currentDirection = nextDirection;

		CreateFirstCell (nextCoordinates, nextDirection);
		currentCoordinates = nextCoordinates;
		currentDirection = nextDirection;

		// Generate the cells
		while(true) {

			turned = false;
			// Check if change direction or not
			if(Random.Range(0, 100) > cornerProbability && !turnedRecently) {
				// Try random direction. If wrong, try until right
				nextDirection = CorridorDirections.RandomValue;
				while (nextDirection == CorridorDirections.OppositeOf(currentDirection)) {
					nextDirection = CorridorDirections.RandomValue;
				}// while
				turned = true;
			}// if
			else {
				nextDirection = currentDirection;
			}
			turnedRecently = turned;
			nextCoordinates += currentDirection.ToIntVector2();

			if(!(ContainsCoordinates(nextCoordinates) && GetCell(nextCoordinates) == null)) {
				break;
			}
			CreateCell(nextCoordinates, currentDirection, nextDirection);
			cellList.Add(GetCell (currentCoordinates));
			currentDirection = nextDirection;
			currentCoordinates = nextCoordinates;

//			// Create the cell
//			CreateCell(nextCoordinates, currentDirection, nextDirection);
//			// Set the current coordinates
//			currentCoordinates = nextCoordinates;
//			currentDirection = nextDirection;
//			// Get the current cell and add it to arrayList
//			cellList.Add(GetCell (currentCoordinates));
//
//			turned = false;
//			// Check if change direction or not
//			if(Random.Range(0, 100) > cornerProbability && !turnedRecently) {
//				// Try random direction. If wrong, try until right
//				nextDirection = CorridorDirections.RandomValue;
//				while (nextDirection == CorridorDirections.OppositeOf(currentDirection)) {
//					nextDirection = CorridorDirections.RandomValue;
//				}// while
//				turned = true;
//			}// if
//			else {
//				nextDirection = currentDirection;
//			}
//
//			turnedRecently = turned;
//			nextCoordinates = currentCoordinates + (nextDirection.ToIntVector2());
		}// while
	}// generate

	// Create the first cell (speshal)
	private void CreateFirstCell(IntVector2 nextCoordinates, CorridorDirection nextDirection) {
		CorridorCell newCell;
		newCell = Instantiate (cell) as CorridorCell;
		newCell.PointTo (nextDirection);
		cells [nextCoordinates.x, nextCoordinates.z] = newCell;
		newCell.material.color = new Color (Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f));
		newCell.name = "First Cell " + nextCoordinates.x + ", " + nextCoordinates.z;
		newCell.transform.parent = transform;
		newCell.transform.localPosition = new Vector3 (nextCoordinates.x - size.x * 0.5f + 0.5f, 
		                                               0f, nextCoordinates.z - size.z * 0.5f + 0.5f);

		cellList.Add(newCell);
	}// createFirstCell

	// Create a single corridor cell
	private void CreateCell (IntVector2 nextCoordinates, CorridorDirection currentDirection, CorridorDirection nextDirection) {
		// Cell's neighbors
		CorridorCell north = GetCell (new IntVector2(nextCoordinates.x, nextCoordinates.z + 1));
		CorridorCell south = GetCell (new IntVector2(nextCoordinates.x, nextCoordinates.z - 1));
		CorridorCell east = GetCell (new IntVector2(nextCoordinates.x + 1, nextCoordinates.z));
		CorridorCell west = GetCell (new IntVector2(nextCoordinates.x - 1, nextCoordinates.z));

		// The new cell
		CorridorCell newCell;
		Debug.Log ("###Corridor Cell " + nextCoordinates.x + ", " + nextCoordinates.z);
		newCell = Instantiate (cell) as CorridorCell;
		newCell.PointFromTo (currentDirection, nextDirection);
		Debug.Log ("--------------------------");

		cells [nextCoordinates.x, nextCoordinates.z] = newCell;
		newCell.material.color = new Color (Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f));
		newCell.name = "Corridor Cell " + nextCoordinates.x + ", " + nextCoordinates.z;
		newCell.transform.parent = transform;
		newCell.transform.localPosition = new Vector3 (nextCoordinates.x - size.x * 0.5f + 0.5f, 
		                                               0f, nextCoordinates.z - size.z * 0.5f + 0.5f);
	}// createCell

	// Checks whether the given coordinates is whithin the corridor
	public bool ContainsCoordinates(IntVector2 coordinate) {
		return coordinate.x >= 0 && coordinate.x < size.x 
			&& coordinate.z >= 0 && coordinate.z < size.z;
	}// containsCoordinates


}
