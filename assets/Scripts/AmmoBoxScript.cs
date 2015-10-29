using UnityEngine;
using System.Collections;

public class AmmoBoxScript : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter (Collider other)
	{
		other.GetComponentInChildren<DartGun>().ammo = 3;
	}
}
