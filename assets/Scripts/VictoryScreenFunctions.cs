using UnityEngine;
using System.Collections;

public class VictoryScreenFunctions : MonoBehaviour {
	public GameObject creditsCanvas;
	public GameObject victoryText;

	public void GoToMenu() {
		Application.LoadLevel("TitleMenu");
		Debug.Log("Clicked Main Menu");
	}

	
	public void CreditsToggle() {
		creditsCanvas.SetActive(true);
		victoryText.SetActive(false);
	}
}
