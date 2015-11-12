using UnityEngine;
using System.Collections;

public class KeyItem : MonoBehaviour {

	private GameObject networkManager;

	// Use this for initialization
	void Start () {

		networkManager = GameObject.Find("NetworkManager");
	
	}
	
	void OnTriggerEnter(Collider other){

		if(other.gameObject.tag == "Player"){
			networkManager.GetComponent<PhotonView>().RPC ("KeyCollectedRPC", PhotonTargets.All);
			Destroy(this.gameObject);
		}

	}
}
