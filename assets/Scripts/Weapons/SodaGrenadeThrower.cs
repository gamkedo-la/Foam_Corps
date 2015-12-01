using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using PhotonHashtable = ExitGames.Client.Photon.Hashtable;

public class SodaGrenadeThrower : WeaponBase {


	// DL - This script is responsible for: 1) Graphical Effects of Shooting 2) Physics of shooting 3) Impacts, etc.

	// Animator anim;
	[SerializeField] string dartPrefab;
	[SerializeField] bool shooting = false;
	[SerializeField] bool loaded = true;
	[SerializeField] float shotTime;
	public float loadTime = 2;	
	public float ammo = 3;
	public GameObject ammoCount;

	public GameObject canInHand;

	Vector3 origPos;


	// Use this for initialization
	void Start () 
	{
		//anim = GetComponentInChildren<Animator> ();
		ammoCount = this.transform.parent.parent.transform.Find("VitalsCanvas/VitalsBar/AmmoCount").gameObject;
		origPos = canInHand.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update (){

		// weapon bob during run
		canInHand.transform.localPosition = origPos
			+ Mathf.Cos(transform.position.x) * Vector3.up * 0.05f
				+ Mathf.Cos(transform.position.z) * Vector3.right * 0.05f;
		// raise weapon when reloading
		if(loaded == false) {
			canInHand.transform.localPosition -=
				0.15f*Vector3.up * ((shotTime + loadTime) - Time.time);
		}

		//Shoot if we hit fire, aren't running, have ammo, and round is loaded
		if(Input.GetButtonDown ("Fire1") && !Input.GetKey(KeyCode.LeftShift)){
			if(loaded == true && ammo > 0){
				shooting = true;
				ammo--;
				shotTime = Time.time;
				Debug.Log ("It's firing");
				SoundCenter.instance.PlayClipOn(
					SoundCenter.instance.sodaThrow,transform.position);
			} else {
				SoundCenter.instance.PlayClipOn(
					SoundCenter.instance.playerNoAmmoTriedToFire,transform.position);
			}
		}

		//Reload if the cooldown has happened
		if(loaded == false && Time.time >= (shotTime + loadTime)){
			loaded = true;
		}

		if(ammo <= 0 && canInHand.activeSelf) {
			canInHand.SetActive(false);
		}

		if (shooting){
			//stop shooting and unload
			shooting = false;
			loaded = false;
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