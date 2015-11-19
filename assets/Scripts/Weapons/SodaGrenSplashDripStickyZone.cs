using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class SodaGrenSplashDripStickyZone : WeaponBase {
	
	public string owner;

	void OnTriggerEnter(Collider other) {
		StickySlowsMe ssmScript = other.GetComponent<StickySlowsMe>();
		if(ssmScript) {
			ssmScript.SpeedStickyZone();
		}
	}

	void OnTriggerExit(Collider other) {
		StickySlowsMe ssmScript = other.GetComponent<StickySlowsMe>();
		if(ssmScript) {
			ssmScript.UnstickySpeed();
		}
	}

	[PunRPC]
	public void NameSplashDripZoneRPC (string playerName)
	{
		owner = playerName;
	}
	
}