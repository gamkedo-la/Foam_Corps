using UnityEngine;
using System.Collections;

public class ZombieNetworkMover : Photon.MonoBehaviour {

	
	public delegate void SendMessage(string MessageOverlay);
	public event SendMessage SendNetworkMessage;
	public delegate void SendScore(string fragger, string fragged);
	public event SendScore SendNetworkScore;
	public ZombieStats zombieStats; // keeper of health, damage, etc.
	Vector3 position;
	Quaternion rotation;
	float smoothing = 10f; // Lerp update smoothing
	float health;
	bool initialLoad = true;

	// Use this for initialization
	void Start () {
	
	StartCoroutine("UpdateData");

	}


	void Update () {
		health = zombieStats.health;
	}
	
	IEnumerator UpdateData() // Syncing across Photon
	{

		//Set transform for each clone if this is the first time we're loading. Should fix jitter
		if(initialLoad)
		{
			initialLoad = false;
			transform.position = position;
			transform.rotation = rotation; 

		}
		while(true)
		{
			transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * smoothing);
			transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * smoothing);
			yield return null;
		}
	}

	void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info)
	{
		if(stream.isWriting){ // Stream Input
		
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
			stream.SendNext(health);
		}
		
		else{ // Stream Output. Read/Write order must be the same
		
			position = (Vector3) stream.ReceiveNext();
			rotation = (Quaternion) stream.ReceiveNext();
			health = (float)stream.ReceiveNext();
		}
	}

	[PunRPC]
	public void GetShot(float damage, string enemyName)
	{
		health -= damage;
		if (health <= 0)
		{
			string myName = ("Zombie");
			if(SendNetworkScore != null)
			{
				 
				SendNetworkScore(enemyName, myName);
			}


			//send messaging the frag event
			if(SendNetworkMessage != null)
				SendNetworkMessage(myName + " was killed by " + enemyName);
					
			PhotonNetwork.Destroy (gameObject);
		}
	}

	
}
