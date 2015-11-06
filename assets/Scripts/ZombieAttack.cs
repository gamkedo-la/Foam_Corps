using UnityEngine;
using System.Collections;

public class ZombieAttack : MonoBehaviour {

	public bool cooledDown = true;
	public float cooldownTime = 2.0F;
	public string owner = ("Zombie");
	public ZombieStats zombieStats; // keeper of health, damage, etc.
	public float damage;

	void Start (){
		damage = zombieStats.damage;
	}
	
	void OnCollisionEnter (Collision collision){
		if(collision.gameObject.tag == "Player" && cooledDown == true){
			collision.transform.GetComponent<PhotonView>().RPC ("GetShot", PhotonTargets.All, damage, owner);
			StartCoroutine(coolDown());
		}
	}

	IEnumerator coolDown() {
		cooledDown = false;
		yield return new WaitForSeconds(cooldownTime);
		cooledDown = true;
	}	
}