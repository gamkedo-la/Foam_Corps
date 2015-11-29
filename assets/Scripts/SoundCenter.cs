using UnityEngine;
using System.Collections;

public class SoundCenter : MonoBehaviour {
	public static SoundCenter instance;

	public AudioClip sodaSplash;
	public AudioClip sodaThrow;
	public AudioClip watergunSquirt;
	public AudioClip zombieMoan;
	public AudioClip zombieMoan2;
	public AudioClip zombieMoan3;
	public AudioClip doorBreak;
	public AudioClip melee;
	public AudioClip dartShoot;
	public AudioClip dartStick;
	public AudioClip getPowerup;
	public AudioClip playerHurt;
	public AudioClip playerDie;
	public AudioClip playerJump;
	public AudioClip playerWepSwitch;
	public AudioClip playerNoAmmoTriedToFire;

	void Awake() {
		if(instance) {
			Destroy(instance.gameObject);
		}
		instance = this;
	}

	public AudioClip randomZombieSound() {
		switch( Random.Range(0,3) ) {
		case 0:
			return zombieMoan;
			break;
		case 1:
			return zombieMoan2;
			break;
		case 2:
		default:
			return zombieMoan3;
			break;
		}
	}

	public void PlayClipOn(AudioClip clip, Vector3 pos, float atVol = 1.0f,
	                       Transform attachToParent = null) {
		GameObject tempGO = new GameObject("TempAudio"); // create the temp object
		tempGO.transform.position = pos; // set its position
		if(attachToParent != null) {
			tempGO.transform.parent = attachToParent.transform;
		}
		AudioSource aSource = tempGO.AddComponent<AudioSource>() as AudioSource; // add an audio source
		aSource.clip = clip; // define the clip
		aSource.volume = atVol;
		aSource.pitch = Random.Range(0.85f,1.15f);
		// set other aSource properties here, if desired
		aSource.Play(); // start the sound
		Destroy(tempGO, clip.length/aSource.pitch);  // destroy object after clip duration
	}
}
