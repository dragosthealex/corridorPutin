using UnityEngine;
using System.Collections;

public class ConferenceRoom : GenericRoom {



	// Use this for initialization
	void Awake () {
		// Set vars
		name = "Conference Room";
		flavor = "Something awesome happens every day!";
		//roomEvents = [];
		// Get the size and the grid
		size.x = Random.Range(minSize.x, maxSize.x);
		size.z = Random.Range(minSize.z, maxSize.z);
		tileGrid = new RoomCell[size.x, size.z];
		// Generate the grid and make the walls
		GenerateGrid ();
		MakeWalls ();
		// Get the door cells
		doors = GetDoors();
		// Generate the middle
		RoomCell middleCell = GetCellAt (new IntVector2(size.x / 2, size.z / 2));
		float floorY = FindObjectOfType<ConferenceRoom>().gameObject.transform.position.y;
		middle = Instantiate (middlePrefab) as GameObject;
		middle.transform.position = middleCell.transform.position + new Vector3 (2.5f, -5f, 2.5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}