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

	// Generates the grid
	protected void GenerateGrid() {
		for (int i = 0; i<= size.x; i++) {
			for (int j = 0; j<= size.z; j++) {
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
		
		while (numberOfDoors > 0) {
			IntVector2 coords = new IntVector2(size.x, Random.Range (2, size.z-2));
			doors[--numberOfDoors] = GetCellAt(coords);
			Debug.Log("++++++++++++++");
			Debug.Log("x: " + size.x + ", z" + size.z);
	        Debug.Log("noDoors: " + numberOfDoors);
		}

		// If this is the spawn door
		if(spawnDoor == numberOfDoors) {
			FindObjectOfType<GameManager>().player = Instantiate(FindObjectOfType<GameManager>().playerPF,doors[spawnDoor].transform.position 
			                                                     + Vector3.up,Quaternion.identity) as GameObject;
		}// if
	return doors;
	}// getDoors
}// class
