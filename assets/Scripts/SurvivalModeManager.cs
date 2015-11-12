using UnityEngine;
using System.Collections;

public class SurvivalModeManager : MonoBehaviour {

	[SerializeField] bool win = false;
	public int keyGoal;
	public int keysCollected;
	public KeyItemManager keyItemManager;
	public GameObject tempVictoryText;

	// Use this for initialization
	void Start () {
		keysCollected = 0;
	
	}
	
	// Update is called once per frame
	void Update () {

		keysCollected = keyItemManager.keysCollected;

		if (keysCollected >= keyGoal){
			win = true;
		}

		//THIS IS TEMP
		if (win == true){
			tempVictoryText.SetActive(true);
		}


	
	}
}
