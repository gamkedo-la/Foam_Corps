using UnityEngine;
using System.Collections;
using System.Collections.Generic; // for List<>
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour {
	
	private DartGun dartGun;
	private SoakerGun soakerGun;
	private SodaGrenadeThrower sodaGrenadeThrower;

	private List<WeaponBase> allWep = new List<WeaponBase>();
	
	void Start() {
		dartGun = GetComponent<DartGun>();
		allWep.Add((WeaponBase)dartGun);

		soakerGun = GetComponent<SoakerGun>();
		allWep.Add((WeaponBase)soakerGun);

		sodaGrenadeThrower = GetComponent<SodaGrenadeThrower>();
		allWep.Add((WeaponBase)sodaGrenadeThrower);
	}

	void ChangeWep(WeaponBase toWep) {
		SoundCenter.instance.PlayClipOn(
			SoundCenter.instance.playerWepSwitch,transform.position);

		foreach(WeaponBase eachWep in allWep) {
			eachWep.enabled = (eachWep == toWep);
		}
	}
	
	void Update () {
		if(Input.GetButtonDown ("WeaponSlot1")){
			ChangeWep(dartGun);
		}
		if(Input.GetButtonDown ("WeaponSlot2")){
			ChangeWep(soakerGun);
		}
		if(Input.GetButtonDown ("WeaponSlot3")){
			ChangeWep(sodaGrenadeThrower);
		}
	}
}