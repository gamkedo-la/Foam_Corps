using UnityEngine;
using System.Collections;

public class DoorSpawner : Photon.MonoBehaviour {
	
	// This script 
		// holds the list of spawnable prefabs
		// receives the choice from the DoorManager at the start of the game
		// spawns the prefab when activated

	public int spawnChoice; // what this will spawn when activated, set by Manager
	public string[] spawnOption; // options for what can be spawned, string for Photon.Instantiate
	public bool spawnIt = false; // trigger for if this should spawn
	public float offsetZ = 0; // Offset in Z from this object where we spawn
	public float offsetX = -1;
	public float offsetY = 0;
	public bool broken = false; // trigger from breakable script to initiate spawn
	bool firstTime = true; // prevent double spawns in network?
	Breakable breakable;
	PhotonView photonView;


	// Use this for initialization
	void Start () {

		breakable = this.GetComponent<Breakable>();
		//photonView = transform.GetComponent<PhotonView>();
	}
	
	// Update is called once per frame
	void Update () {
		broken = breakable.broken;

		if (broken == true && firstTime == true){
			spawnIt = true;
		}

		if (spawnIt == true){
			firstTime = false;
			spawnIt = false;
			DoorSpawn();
			//photonView.RPC ("DoorSpawn", PhotonTargets.All);
			
		}
	
	}

	//[PunRPC]
	public void DoorSpawn (){

			firstTime = false;
			spawnIt = false;
			Vector3 doorSpawnPoint = new Vector3( ((this.transform.position.x) + offsetX), ((this.transform.position.y) + offsetY), ((this.transform.position.z) + offsetZ) );
			//PhotonNetwork.Instantiate (spawnOption[spawnChoice], doorSpawnPoint, this.transform.rotation, 0);
			PhotonNetwork.InstantiateSceneObject (spawnOption[spawnChoice], doorSpawnPoint, this.transform.rotation, 0, null);
			this.enabled = false;
			
	}
}