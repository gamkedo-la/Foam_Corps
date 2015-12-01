using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class SpeedPowerUp : MonoBehaviour {

	public float speed = 7;

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.GetComponent<FirstPersonController> () != null) {
			other.gameObject.GetComponent<FirstPersonController> ().SpeedPowerUp(speed);
			SoundCenter.instance.PlayClipOn(
				SoundCenter.instance.getPowerup,transform.position);
			Destroy(gameObject);
		}
	}
}
