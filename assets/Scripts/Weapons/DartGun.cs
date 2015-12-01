using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using PhotonHashtable = ExitGames.Client.Photon.Hashtable;

public class DartGun : WeaponBase {


	// DL - This script is responsible for: 1) Graphical Effects of Shooting 2) Physics of shooting 3) Impacts, etc.

	// Animator anim;
	[SerializeField] string dartPrefab;
	[SerializeField] bool shooting = false;
	[SerializeField] bool loaded = true;
	[SerializeField] float shotTime;
	public float loadTime = 2;	
	private int ammo = 3;
	public GameObject ammoCount;

	public int ammoMax = 3;
	public GameObject gunObj;
	public GameObject dartAmmoGOFirst;
	public GameObject dartAmmoGOSecond;
	public GameObject dartAmmoGOLast;

	void UpdateAmmoModelVis() {
		dartAmmoGOLast.SetActive(ammo>0);
		dartAmmoGOSecond.SetActive(ammo>1);
		dartAmmoGOFirst.SetActive(ammo>2);
	}

	public void GiveAmmo(int amt) {
		ammo += amt;
		/*if(ammo > ammoMax) { // allow overload?
			ammo = ammoMax;
		}*/

		// prompt gun to display its new ammo
		loaded = false;
		shotTime = Time.time;

		UpdateAmmoModelVis();
	}

	// Use this for initialization
	void Start () 
	{
		//anim = GetComponentInChildren<Animator> ();
		ammoCount = this.transform.parent.parent.transform.Find("VitalsCanvas/VitalsBar/AmmoCount").gameObject;

		UpdateAmmoModelVis();
	}
	
	// Update is called once per frame
	void Update (){
		//Shoot if we hit fire, aren't running, have ammo, and round is loaded
		if(Input.GetButtonDown ("Fire1") && !Input.GetKey(KeyCode.LeftShift)){
			if(loaded == true && ammo > 0){
				SoundCenter.instance.PlayClipOn(
					SoundCenter.instance.dartShoot,transform.position);
				shooting = true;
				shotTime = Time.time;
				Debug.Log ("It's firing");
			} else {
				SoundCenter.instance.PlayClipOn(
					SoundCenter.instance.playerNoAmmoTriedToFire,transform.position);
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
			UpdateAmmoModelVis();
			//instantiate the dart
			GameObject dartInstance;
			dartInstance = PhotonNetwork.Instantiate(dartPrefab, transform.position, transform.rotation, 0) as GameObject;
		}

		
	}

	void FixedUpdate ()
	{
		Quaternion gunRotGoal = transform.rotation * Quaternion.AngleAxis(180.0f,Vector3.up);
		if(loaded == false || ammo == 0) {
			gunRotGoal *= Quaternion.AngleAxis(-68.0f,Vector3.up);
			gunRotGoal *= Quaternion.AngleAxis(38.0f,Vector3.right);
		}
		
		gunObj.transform.rotation = Quaternion.Slerp(
			gunObj.transform.rotation,
			gunRotGoal, 0.3f);


		ammoCount.GetComponent<Text>().text = ammo.ToString();
	}



}