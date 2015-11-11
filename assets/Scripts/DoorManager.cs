using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoorManager : MonoBehaviour {

	// This script will collect all of the breakable doors into an array and tell them at the start of the game what they will be spawning

	public Transform doorHolder; // parent empty that holds all the doors
	public List<DoorSpawner> doorList = new List<DoorSpawner>(); // all of the doors that will be managed
	public int numberOfDoors; // how many doors there are

	// "Percentage" of total that are each item, actually added and checked so doesn't have to be %
	public float percentKey;
	public float percentHealthPowerUp;
	public float percentSpeedPowerUp;
	public float percentHoard;
	public float percentDartAmmo;
	public float percentSoakerAmmo;
	int numberKey;
	int numberHealthPowerUp;
	int numberSpeedPowerUp;
	int numberHoard;
	int numberDartAmmo;
	int numberSoakerAmmo;


	// Use this for initialization
	void Start () {

		numberOfDoors = doorHolder.transform.childCount; // set number of doors based on children of doorHolder

	
		foreach (Transform child in doorHolder){ // populate our list with the doors children of doorHolder
			doorList.Add(child.GetComponent<DoorSpawner>());
		}

		// take spawn type percentage inputs and calculate how much of each we need with how many doors we have
		PercentageBreakdown();

		// Assign all of the doors a spawn type
		AssignDoors(numberKey, 0);
		AssignDoors(numberHealthPowerUp, 1);
		AssignDoors(numberSpeedPowerUp, 2);
		AssignDoors(numberHoard, 3);
		AssignDoors(numberDartAmmo, 4);
		AssignDoors(numberSoakerAmmo, 5);
		AssignDoorsRemainder(1,5);
	}


	void PercentageBreakdown(){
		// determine from percentage inputs how many of each spawn type is assigned to door arrays
		float total = (percentKey + percentHealthPowerUp + percentSpeedPowerUp + percentHoard + percentDartAmmo + percentSoakerAmmo);
		numberKey = (Mathf.RoundToInt(numberOfDoors * (percentKey/total)));
		numberHealthPowerUp = (Mathf.RoundToInt(numberOfDoors * (percentHealthPowerUp/total)));
		numberSpeedPowerUp = (Mathf.RoundToInt(numberOfDoors * (percentSpeedPowerUp/total)));
		numberHoard = (Mathf.RoundToInt(numberOfDoors * (percentHoard/total)));
		numberDartAmmo = (Mathf.RoundToInt(numberOfDoors * (percentDartAmmo/total)));
		numberSoakerAmmo = (Mathf.RoundToInt(numberOfDoors * (percentSoakerAmmo/total)));
	}
	
	// if we make this a generic with inputs, int numberLeft, int numberDoors, int spawnChoice
	void AssignDoors(int numberItem, int spawnChoiceNum){

		// set all the Key doors
		while(numberItem > 0 && numberOfDoors > 0){ // While we have keys to assign and there are still doors
			int randomDoor = (Random.Range(0,(numberOfDoors - 1)));  // get a random array position
			doorList[randomDoor].spawnChoice = spawnChoiceNum; // assign that list position
			doorList.RemoveAt(randomDoor); // remove that list item
			numberItem --; // decrease items left
			numberOfDoors--; // decrease doors left
		}		
	}

	void AssignDoorsRemainder(int rangeLow, int rangeHigh){
		while(numberOfDoors > 0){ // While we have doors left
			int randomDoor = (Random.Range(0,(numberOfDoors - 1)));  // get a random array position
			doorList[randomDoor].spawnChoice = (Random.Range(rangeLow,rangeHigh)); // assign that array position
			doorList.RemoveAt(randomDoor); // remove that array item
			numberOfDoors--; // decrease doors left
		}	
	}
}
