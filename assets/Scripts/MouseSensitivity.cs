using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class MouseSensitivity : MonoBehaviour {

	private FirstPersonController fpsController;

	// Update is called once per frame
	public void ChangeSensitivity (float sensitivity) {
		if(fpsController == null) {
			GameObject playerGO = (GameObject) GameObject.FindGameObjectWithTag("Player");
			if(playerGO) {
				fpsController = playerGO.GetComponent<FirstPersonController>();
			}
		}
		if(fpsController != null) {
			fpsController.setMouseSens( sensitivity );
		}
	}
}
