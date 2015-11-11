using UnityEngine;
using System.Collections;

public class SoakerAmmoBoxScript : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter (Collider other)
	{
		other.GetComponentInChildren<SoakerGun>().ammo = 600;
		Destroy(gameObject);
	}
}
