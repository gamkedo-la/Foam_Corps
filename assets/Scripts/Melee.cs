using UnityEngine;
using System.Collections;

public class Melee : MonoBehaviour {

public float cooldownTime = 2.0F;
public float damage = 25F;
public float range = 0.5F;
public float knockbackForce = 15; // how much force hit pushes with
bool meleeCooldown = true;
public string owner;

	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown ("Fire2")){
			if( meleeCooldown == true){
				SoundCenter.instance.PlayClipOn(
					SoundCenter.instance.melee,transform.position);
				MeleeAttack();
				meleeCooldown = false;
				StartCoroutine(MeleeCool());

			}		
		}
	}

	void MeleeAttack (){
		RaycastHit hit;
		Vector3 fwd = transform.TransformDirection(Vector3.forward);
		if(Physics.Raycast(transform.position, fwd, out hit, range)){
			Debug.Log ("Melee Attack!");
			if (hit.rigidbody != null){ // if we hit a rigidbody, apply knockback
					hit.rigidbody.AddForce(fwd * knockbackForce);
			}
			if(hit.transform.tag == "Enemy"){
						hit.transform.GetComponent<PhotonView>().RPC ("GetShot", PhotonTargets.All, damage, owner);
						Destroy(gameObject);
			}
			if(hit.transform.tag == "Door"){
				hit.transform.GetComponent<PhotonView>().RPC ("BreakDoor", PhotonTargets.All);
				if(Physics.Raycast(transform.position, fwd, out hit, range)){
					hit.rigidbody.AddForce(fwd * knockbackForce);
				}
			}	

		}
	}	

	IEnumerator MeleeCool() {
		yield return new WaitForSeconds(cooldownTime);
		meleeCooldown = true;
	}	

}		

