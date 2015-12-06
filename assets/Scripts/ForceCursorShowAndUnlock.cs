using UnityEngine;
using System.Collections;

public class ForceCursorShowAndUnlock : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}
}
