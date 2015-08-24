﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;

[System.Serializable]
public class EventRoom  {

	
	public string name;
	public int id;

	public string displayText;


	public Text textHolder;
	public Button[] options;

//	public EventRoom (string newName,int newId,string newDisplayText,Text newText,Button[] newButtonArray){
//
//		name = newName;
//		id = newId;
//		displayText = newDisplayText;
//		textHolder = newText;
//		options = newButtonArray;
//	}
}