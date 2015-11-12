using UnityEngine;
using System.Collections;

public class KeyItemManager : MonoBehaviour {

	public int keysCollected;

	// Use this for initialization
	void Start () {
	
		keysCollected = 0;

	}
	
	[PunRPC]
	public void KeyCollectedRPC () {
		keysCollected ++;
	}
}
