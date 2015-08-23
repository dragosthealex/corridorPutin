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
		// Get the doors
		doors = GetDoors();
		// Generate the grid
		GenerateGrid ();
		// Instantiate the middle
		//middle = Instantiate(middlePrefab) as GameObject;
		// Instantiate the furnitures
//		foreach (GameObject furniturePrefab in furniture) {
//			furniturePrefab = Instantiate(furniturePrefab) as GameObject;
//		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}