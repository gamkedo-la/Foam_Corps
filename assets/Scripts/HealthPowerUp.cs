using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class HealthPowerUp : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		if (other.gameObject.GetComponent<PlayerNetworkMover> () != null) {
			other.gameObject.GetComponent<PlayerNetworkMover> ().HealPowerUp();
			SoundCenter.instance.PlayClipOn(
				SoundCenter.instance.getPowerup,transform.position);
			Destroy(gameObject);
		}
	}
}
