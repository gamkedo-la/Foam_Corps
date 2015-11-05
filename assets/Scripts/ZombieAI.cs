using UnityEngine;
using System.Collections;

public class ZombieAI : MonoBehaviour {

	public GameObject target; // what we're going to lerp to and "attack"
	public float targetTime; // time of last targeting operation
	public float targetCooldown = 5.0F; // time between retargets
	public float moveSpeed = 0.1F;
	


//TODO
	// Update targeting to consider dead players

	// Use this for initialization
	void Start () {
		// Get list of players
		StartCoroutine(targetUpdate());
		
	}
	
	// Update is called once per frame
	void Update () {

		// Look at player
		transform.LookAt(target.transform);

		// Lerp towards the player
		float step = moveSpeed * Time.time;
		transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
				
	}


	IEnumerator targetUpdate() {
		while(true) {
			target = GetTarget(); // Set closest player as the target
			Debug.Log ("Targeting has run");
	  		yield return new WaitForSeconds(targetCooldown); //Cooldown
  		}
	}

	GameObject GetTarget (){ // Figure out the closest player to go after
		GameObject[] targets;
		targets = GameObject.FindGameObjectsWithTag("Player");
		GameObject closest = null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject tar in targets){
			Vector3 diff = tar.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance){
				closest = tar;
				distance = curDistance;
			}
		}
		return closest;
	}
}
