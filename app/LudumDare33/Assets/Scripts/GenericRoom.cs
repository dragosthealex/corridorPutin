using UnityEngine;
using System.Collections;

public abstract class GenericRoom : MonoBehaviour {
	public string name;
	public string flavor;
	//public RoomEvent[] roomEvents;
	public IntVector2 size;
	public RoomCell[,] tileGrid;
	public GameObject middle;
	public IntVector2 minSize;
	public IntVector2 maxSize;
	public GameObject middlePrefab;
	public IntVector2 middlePrefabGridSize;
	public GameObject[] furniture;
	public RoomCell cell;
	public RoomCell[] doors;
	public IntVector2 spawn;

	// Generates the grid
	protected void GenerateGrid() {
		for (int i = 0; i< size.x; i++) {
			for (int j = 0; j< size.z; j++) {
				RoomCell newCell = Instantiate (cell) as RoomCell;
				newCell.name = "Room Cell at " + i + ", " + j;
				tileGrid[i, j] = newCell;
				newCell.transform.parent = transform;
				newCell.transform.localPosition = new Vector3 (i - size.x * 0.5f + 0.5f, 
				                                               0f, j - size.z * 0.5f + 0.5f);
			}
		}
	}// Generate
	
	public RoomCell GetCellAt(IntVector2 coords) {
		return tileGrid[coords.x, coords.z];
	}// getcellat
	
	// Randomise the doors, and also the enter door (spawn point in this room)
	protected RoomCell[] GetDoors() {
		// Number of doors
		int numberOfDoors = Random.Range (1, 4);
		int spawnDoor = Random.Range(0, numberOfDoors-1);
		RoomCell[] doors = new RoomCell[numberOfDoors];
		IntVector2 coords;

		if (numberOfDoors > 0) {
			coords = new IntVector2 (size.x-1, Random.Range (2, size.z - 2));
			Debug.Log ("++++++++++++++");
			Debug.Log ("x: " + size.x + ", z" + size.z);
			Debug.Log ("noDoors: " + numberOfDoors);
			Debug.Log("coords x: " + coords.x + ", coords z: " + coords.z);
			doors [--numberOfDoors] = GetCellAt (coords);
			// If this is the spawn door
			if (spawnDoor == numberOfDoors) {
				spawn = new IntVector2 ((int)GetCellAt (coords).gameObject.transform.position.x, 
				                        (int)GetCellAt (coords).gameObject.transform.position.z);
			}// if
		}
		if (numberOfDoors > 0) {
			coords = new IntVector2 (Random.Range (2, size.x - 2), size.z-1);
			Debug.Log ("++++++++++++++");
			Debug.Log ("x: " + size.x + ", z" + size.z);
			Debug.Log ("noDoors: " + numberOfDoors);
			Debug.Log("coords x: " + coords.x + ", coords z: " + coords.z);
			doors [--numberOfDoors] = GetCellAt (coords);
			// If this is the spawn door
			if (spawnDoor == numberOfDoors) {
				spawn = new IntVector2 ((int)GetCellAt (coords).gameObject.transform.position.x, 
				                        (int)GetCellAt (coords).gameObject.transform.position.z);
			}// if
		}
		if (numberOfDoors > 0) {
			coords = new IntVector2 (0, Random.Range (2, size.z - 2));
			Debug.Log ("++++++++++++++");
			Debug.Log ("x: " + size.x + ", z" + size.z);
			Debug.Log ("noDoors: " + numberOfDoors);
			Debug.Log("coords x: " + coords.x + ", coords z: " + coords.z);
			doors [--numberOfDoors] = GetCellAt (coords);
			// If this is the spawn door
			if (spawnDoor == numberOfDoors) {
				spawn = new IntVector2 ((int)GetCellAt (coords).gameObject.transform.position.x, 
				                        (int)GetCellAt (coords).gameObject.transform.position.z);
			}// if
		}
		if (numberOfDoors > 0) {
			coords = new IntVector2 (Random.Range (2, size.x - 2), 0);
			Debug.Log ("++++++++++++++");
			Debug.Log ("x: " + size.x + ", z" + size.z);
			Debug.Log ("noDoors: " + numberOfDoors);
			Debug.Log("coords x: " + coords.x + ", coords z: " + coords.z);
			doors [--numberOfDoors] = GetCellAt (coords);
			// If this is the spawn door
			if (spawnDoor == numberOfDoors) {
				spawn = new IntVector2 ((int)GetCellAt (coords).gameObject.transform.position.x, 
				                        (int)GetCellAt (coords).gameObject.transform.position.z);
			}// if
		}


		// TODO : PUT DOORS ON WALLS
		foreach (RoomCell door in doors) {
			door.gameObject.transform.GetChild(0).GetComponent<MeshRenderer> ().material.color = Color.blue;
		}
	return doors;
	}// getDoors

	public IntVector2 GetSpawn() {
		return spawn;
	}
}// class
