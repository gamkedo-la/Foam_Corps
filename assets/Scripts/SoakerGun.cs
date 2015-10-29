using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoakerGun : MonoBehaviour {


	// DL - This script is responsible for: 1) Graphical Effects of Shooting 2) Physics of shooting 3) Impacts, etc.

	Animator anim;
	// DL - Collection (Matrix? Array?) of impacts that are moved when shooting. Since they already exist and are not repeatedly called, this is more resource friendly.
	[SerializeField] bool shooting = false;
	[SerializeField] bool loaded = true; // when unloaded, gun kills itself
	[SerializeField] float shotTime;
	public int ammo = 1200;
	public int ammoDecay = 1; // how fast we burn ammo
	public float range = 10; 
	public float knockbackForce = 35; // how much force hit pushes with
	public GameObject ammoCount;  // referenced for UI readout



	// Use this for initialization
	void Start () 
	{
		ammoCount = this.transform.parent.parent.transform.Find("VitalsCanvas/VitalsBar/AmmoCount").gameObject;
		
	}
	
	// Update is called once per frame
	void Update (){
		if(Input.GetButton ("Fire1") && !Input.GetKey(KeyCode.LeftShift)){ // While pressing fire, we aren't running, and we have ammo, we are shooting.
			if(loaded == true && ammo > 0){
						shooting = true;
						Debug.Log ("It's shooting");
			}		
					
		}
		if(Input.GetButtonUp ("Fire1")){
					shooting = false;
			}

		
		if (shooting){ // how we shoot
			RaycastHit hit;
			Vector3 fwd = transform.TransformDirection(Vector3.forward);
			if(Physics.Raycast(transform.position, fwd, out hit, range)){
				Debug.Log ("It's soaking!");
				Debug.DrawLine(transform.position, hit.point, Color.blue, 2);  // TEMP display of raycast
				if (hit.rigidbody != null){ // if we hit a rigidbody, apply force
					hit.rigidbody.AddForce(fwd * knockbackForce);
					Debug.Log ("Somebody gettin' soaked!");
				}
			}
			
		}

		if(ammo <= 0){ // if we run out of ammo, we aren't loaded
			loaded = false;
		}

		if(loaded == false){ // if we arent loaded...
			// TODO switch to primary weapon
			Destroy (this); // and destroy the special weapon
		}

		
	}

	void FixedUpdate ()
	{
		if (shooting){ 
			ammo -= (Mathf.RoundToInt(Time.deltaTime * ammoDecay));} // lose ammo gradually as we shoot
		ammoCount.GetComponent<Text>().text = (ammo/6).ToString();  // update UI
	}



}