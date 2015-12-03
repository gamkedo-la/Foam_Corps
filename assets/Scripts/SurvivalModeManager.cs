using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SurvivalModeManager : MonoBehaviour {

	[SerializeField] bool win = false;
	public int keyGoal;
	public int keysCollected;
	public KeyItemManager keyItemManager;
	public GameObject tempVictoryText;
	public GameObject keyCount;
	string keyText;


	// Use this for initialization
	void Start () {
		keysCollected = 0;
		keyText = keyCount.GetComponent<Text>().text;
	
	}
	
	// Update is called once per frame
	void Update () {

		keysCollected = keyItemManager.keysCollected;

		//keyText = "keysCollected  keyGoal).ToString()";

		if (keysCollected >= keyGoal){
			win = true;
		}

		//THIS IS TEMP
		if (win == true){
			Application.LoadLevel("VictoryScreen");
		}	
	}

	void FixedUpdate() {
		keyCount.GetComponent<Text>().text = (keysCollected + " / " + keyGoal).ToString();
	}
}
