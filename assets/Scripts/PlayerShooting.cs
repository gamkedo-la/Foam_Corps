using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using PhotonHashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerShooting : MonoBehaviour {


	// DL - This script is responsible for: 1) Graphical Effects of Shooting 2) Physics of shooting 3) Impacts, etc.

	Animator anim;
	[SerializeField] string dartPrefab;
	// DL - Collection (Matrix? Array?) of impacts that are moved when shooting. Since they already exist and are not repeatedly called, this is more resource friendly.
	[SerializeField] bool shooting = false;
	[SerializeField] bool loaded = true;
	public float loadTime = 2;
	[SerializeField] float shotTime;
	public float ammo = 3;
	public GameObject ammoCount;



	// Use this for initialization
	void Start () 
	{
		anim = GetComponentInChildren<Animator> ();
		ammoCount = this.transform.parent.parent.transform.Find("VitalsCanvas/VitalsBar/AmmoCount").gameObject;
	}
	
	// Update is called once per frame
	void Update (){
		//Shoot if we hit fire, aren't running, have ammo, and round is loaded
		if(Input.GetButtonDown ("Fire1") && !Input.GetKey(KeyCode.LeftShift)){
				if(loaded == true && ammo > 0){
					shooting = true;
					shotTime = Time.time;
					Debug.Log ("It's firing");
				}		
		}

		//Reload if the cooldown has happened
		if(loaded == false && Time.time >= (shotTime + loadTime)){
			loaded = true;
		}

		if (shooting){
			//stop shooting and unload
			shooting = false;
			loaded = false;
			ammo -= 1;
			//instantiate the dart
			GameObject dartInstance;
			dartInstance = PhotonNetwork.Instantiate(dartPrefab, transform.position, transform.rotation, 0) as GameObject;
		}

		
	}

	void FixedUpdate ()
	{
		ammoCount.GetComponent<Text>().text = ammo.ToString();
	}



}