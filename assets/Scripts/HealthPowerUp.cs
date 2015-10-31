using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class HealthPowerUp : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		if (other.gameObject.GetComponent<FirstPersonController> () != null) {
			other.gameObject.GetComponent<FirstPersonController> ().HealPowerUp();
			Destroy(gameObject);
		}
	}
}
