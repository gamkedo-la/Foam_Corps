using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class HealthPowerUp : MonoBehaviour {

	public float rotateSpeed = 2.0f;

	void OnTriggerEnter(Collider other){
		if (other.gameObject.GetComponent<FirstPersonController> () != null) {
			other.gameObject.GetComponent<FirstPersonController> ().HealPowerUp();
			Destroy(gameObject);
		}
	}

	void Update(){
		transform.Rotate (Vector3.up, rotateSpeed);
	}
}
