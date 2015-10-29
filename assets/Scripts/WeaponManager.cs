using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour {

public DartGun dartGun;
public SoakerGun soakerGun;

void Update () {

	if(Input.GetButtonDown ("WeaponSlot1")){
		dartGun.enabled = true;
		Debug.Log("Weapon Switch 1");
		if(soakerGun != null){
			soakerGun.enabled = false;
		}

	}
	if(Input.GetButtonDown ("WeaponSlot2")){
		Debug.Log("Weapon Switch 1");
		soakerGun.enabled = true;
		if(dartGun != null){
			dartGun.enabled = false;
		}
		
	}
}





}