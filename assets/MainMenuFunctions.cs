using UnityEngine;
using System.Collections;

public class MainMenuFunctions : MonoBehaviour {
	public GameObject creditsCanvas;
	public GameObject mainButtonsCanvas;

	public void StartSingleplayer() {
		PhotonNetwork.PhotonServerSettings.HostType = ServerSettings.HostingOption.OfflineMode;
		Application.LoadLevel("LEVEL_Cubicles");
	}

	public void StartMultiplayer() {
		PhotonNetwork.PhotonServerSettings.HostType = ServerSettings.HostingOption.PhotonCloud;
		Application.LoadLevel("LEVEL_Cubicles");
	}

	public void CreditsToggle() {
		creditsCanvas.SetActive( mainButtonsCanvas.activeSelf );
		mainButtonsCanvas.SetActive( mainButtonsCanvas.activeSelf == false );
	}
}
