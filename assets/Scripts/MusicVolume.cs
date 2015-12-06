using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class MusicVolume : MonoBehaviour {

	private AudioSource bgMusic;

	// Update is called once per frame
	public void ChangeVolume (float volume) {
		if(bgMusic == null) {
			GameObject playerGO = (GameObject) GameObject.FindGameObjectWithTag("Player");
			if(playerGO) {
				bgMusic = playerGO.GetComponentInChildren<AudioSource>();
			}
		}
		if(bgMusic != null) {
			setVolume( volume );
		}
	}

	void setVolume(float volume){
		bgMusic.volume = volume;
	}
}
