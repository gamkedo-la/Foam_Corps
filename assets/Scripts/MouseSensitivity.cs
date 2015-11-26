using UnityEngine;
using System.Collections;

public class MouseSensitivity : MonoBehaviour {

	public float sensitivity;
	public float mouseSpeedX;
	public float mouseSpeedY;



	// Use this for initialization
	void Start () {
		mouseSpeedX = Input.GetAxis("Mouse X");
		mouseSpeedY = Input.GetAxis("Mouse Y");
	
	}
	
	// Update is called once per frame
	public void ChangeSensitivity (float sensitivity) {
	mouseSpeedX = Input.GetAxis("Mouse X") * sensitivity;
	mouseSpeedY = Input.GetAxis("Mouse Y") * sensitivity;
	}
}
