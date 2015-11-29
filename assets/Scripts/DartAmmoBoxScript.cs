using UnityEngine;
using System.Collections;

public class DartAmmoBoxScript : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter (Collider other)
	{
		other.GetComponentInChildren<DartGun>().GiveAmmo(3);
		SoundCenter.instance.PlayClipOn(
			SoundCenter.instance.getPowerup,transform.position);
		Destroy(gameObject);
	}
}
