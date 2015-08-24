using UnityEngine;
using System.Collections;

public abstract class GenericRoom : MonoBehaviour {
	public string name;
	public string flavor;
	//public RoomEvent[] roomEvents;
	public IntVector2 size;
	public RoomCell[,] tileGrid;
	protected GameObject middle;
	protected GameObject doorInstance;
	public IntVector2 minSize;
	public IntVector2 maxSize;
	public GameObject doorPrefab;
	public GameObject middlePrefab;
	public IntVector2 middlePrefabGridSize;
	public GameObject[] furniture;
	public RoomCell cell;
	public RoomCell[] doors;
	public IntVector2 spawn;
	public GameObject wall;
	public int enemyProbability;
	public int npcProbability;
	public GameObject[] enemyPrefabs;
	protected GameObject enemy;
	public GameObject[] neutralPrefabs;
	protected GameObject neutral;


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
		try {
			return tileGrid[coords.x, coords.z];
		} catch (System.Exception e) {
			return null;
		}
	}// getcellat

	protected void MakeWalls() {
		RoomCell theCell;
		Vector3 cellPosition;
		Transform wTransform;
		// Set walls for south
		for (int i = 0; i < size.x; i++) {
			// Create the wall
			wall = Instantiate(wall) as GameObject;
			wTransform = wall.transform;
			// Get cell and set the wall as child
			theCell = GetCellAt(new IntVector2(i, 0));
			cellPosition = theCell.transform.position;
			wTransform.SetParent(theCell.transform);
			// Set wall position
			wTransform.position = new Vector3(cellPosition.x, cellPosition.y + 5f, cellPosition.z-5f);
			wTransform.localScale = 1 * Vector3.one;
			
		}//for
		// Set walls for north
		for (int i = 0; i < size.x; i++) {
			// Create the wall
			wall = Instantiate(wall) as GameObject;
			wTransform = wall.transform;
			// Get cell and set the wall as child
			theCell = GetCellAt(new IntVector2(i, size.z-1));
			cellPosition = theCell.transform.position;
			wTransform.SetParent(theCell.transform);
			// Set wall position
			wTransform.position = new Vector3(cellPosition.x, cellPosition.y + 5f, cellPosition.z+5f);
			wTransform.localScale = 1 * Vector3.one;
			wTransform.rotation = Quaternion.Euler(0f, 0f, 0f);
		}//for
		// Set walls for east
		for (int i = 0; i < size.z; i++) {
			// Create the wall
			wall = Instantiate(wall) as GameObject;
			wTransform = wall.transform;
			// Get cell and set the wall as child
			theCell = GetCellAt(new IntVector2(0, i));
			cellPosition = theCell.transform.position;
			wTransform.SetParent(theCell.transform);
			// Set wall position
			wTransform.position = new Vector3(cellPosition.x - 5f, cellPosition.y + 5f, cellPosition.z);
			wTransform.localScale = 1 * Vector3.one;
			wTransform.rotation = Quaternion.Euler(0f, -90f, 0f);
		}//for
		// Set walls for west
		for (int i = 0; i < size.z; i++) {
			// Create the wall
			wall = Instantiate(wall) as GameObject;
			wTransform = wall.transform;
			// Get cell and set the wall as child
			theCell = GetCellAt(new IntVector2(size.x-1, i));
			cellPosition = theCell.transform.position;
			wTransform.SetParent(theCell.transform);
			// Set wall position
			wTransform.position = new Vector3(cellPosition.x + 5f, cellPosition.y + 5f, cellPosition.z);
			wTransform.localScale = 1 * Vector3.one;
			wTransform.rotation = Quaternion.Euler(0f, 90f, 0f);
		}//for
	}// makeWalls

	// Randomise the doors, and also the enter door (spawn point in this room)
	protected RoomCell[] GetDoors() {
		// Number of doors
		int numberOfDoors = Random.Range (1, 4);
		int spawnDoor = Random.Range(0, numberOfDoors-1);
		RoomCell[] doors = new RoomCell[numberOfDoors];
		IntVector2 coords;

		if (numberOfDoors > 0) {
			coords = new IntVector2 (size.x-1, Random.Range (2, size.z - 2));
			doors [--numberOfDoors] = GetCellAt (coords);
			// If this is the spawn door
			if (spawnDoor == numberOfDoors) {
				spawn = new IntVector2 ((int)GetCellAt (coords).gameObject.transform.position.x, 
				                        (int)GetCellAt (coords).gameObject.transform.position.z);
			}// if
		}
		if (numberOfDoors > 0) {
			coords = new IntVector2 (Random.Range (2, size.x - 2), size.z-1);
			doors [--numberOfDoors] = GetCellAt (coords);
			// If this is the spawn door
			if (spawnDoor == numberOfDoors) {
				spawn = new IntVector2 ((int)GetCellAt (coords).gameObject.transform.position.x, 
				                        (int)GetCellAt (coords).gameObject.transform.position.z);
			}// if
		}
		if (numberOfDoors > 0) {
			coords = new IntVector2 (0, Random.Range (2, size.z - 2));
			doors [--numberOfDoors] = GetCellAt (coords);
			// If this is the spawn door
			if (spawnDoor == numberOfDoors) {
				spawn = new IntVector2 ((int)GetCellAt (coords).gameObject.transform.position.x, 
				                        (int)GetCellAt (coords).gameObject.transform.position.z);
			}// if
		}
		if (numberOfDoors > 0) {
			coords = new IntVector2 (Random.Range (2, size.x - 2), 0);
			doors [--numberOfDoors] = GetCellAt (coords);
			// If this is the spawn door
			if (spawnDoor == numberOfDoors) {
				spawn = new IntVector2 ((int)GetCellAt (coords).gameObject.transform.position.x, 
				                        (int)GetCellAt (coords).gameObject.transform.position.z);
			}// if
		}


		// TODO : PUT DOORS ON WALLS
		foreach (RoomCell door in doors) {
			door.transform.GetChild(1).gameObject.GetComponent<WallControl>().door = true;
			//door.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;

			doorInstance = Instantiate(doorPrefab) as GameObject;
			doorInstance.transform.SetParent(door.transform.GetChild(1).gameObject.GetComponent<WallControl>().gameObject.transform);
			doorInstance.transform.localPosition = new Vector3(0f, 0f, -0.05f);
			doorInstance.transform.localRotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
			doorInstance.transform.localScale = new Vector3(0.1f, 0.1f, 0.3f);
		}
	return doors;
	}// getDoors

	public IntVector2 GetSpawn() {
		return spawn;
	}

	// Spawn the npcs
	public void spawnNPCs () {
		for (int i=0; i<size.x; i++) {
			for (int j=0; j<size.z; j++) {
				// If spawn something
				if (GetCellAt(new IntVector2(i, j)) != null && GetCellAt(new IntVector2(i, j)).isEmpty() && npcProbability > Random.Range (0, 100)) {
					// If spawn enemies
					if(enemyProbability > Random.Range(0, 100)){
						// Spawn enemy
						enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)]) as GameObject;
						enemy.transform.position = GetCellAt(new IntVector2(i, j)).transform.position;
						enemy.transform.SetParent(this.gameObject.transform);
					} else {
						neutral = Instantiate(neutralPrefabs[Random.Range(0, neutralPrefabs.Length)]) as GameObject;
						neutral.transform.position = GetCellAt(new IntVector2(i, j)).transform.position;
						neutral.transform.SetParent(this.gameObject.transform);
					}// else
				}//if
			}// for
		}// for
	}// spawn npcs
}// class
