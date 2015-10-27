using UnityEngine;
using System.Collections;

public class FogTurnOn : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log("fog turned on by script (off in editor for visibility)");
		RenderSettings.fog = true;
	}
}
