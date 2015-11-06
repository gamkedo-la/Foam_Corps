using UnityEngine;
using System.Collections;
using PhotonHashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerNetworkMover : Photon.MonoBehaviour {


	public delegate void Respawn(float time);
	public event Respawn RespawnMe;
	public delegate void SendMessage(string MessageOverlay);
	public event SendMessage SendNetworkMessage;
	public delegate void SendScore(string fragger, string fragged);
	public event SendScore SendNetworkScore;
	
	Vector3 position;
	Quaternion rotation;
	float smoothing = 10f;
	public float health = 100f;
	float myPlayerFrag;
	float myPlayerDeath;

	//syncing animation
	bool aim = false;
	bool sprint = false;
	bool initialLoad = true;

	Animator anim;

	// Use this for initialization
	void Start () 
	{
		//Get animator for syncing
		anim = GetComponentInChildren<Animator> ();

		if(photonView.isMine)  // Activate player scripts if my character
		{
			GetComponent<Rigidbody>().useGravity = true;
			GetComponent<CharacterController>().enabled = true;
			(GetComponent("FirstPersonController") as MonoBehaviour).enabled = true;
			GetComponentInChildren<DartGun>().enabled = true;
			GetComponentInChildren<AudioListener>().enabled = true;
			transform.tag = "Player";
			gameObject.layer = 14;
			foreach(Camera cam in GetComponentsInChildren<Camera>())
			cam.enabled = true;
			// Put weapon back on Gun layer for camera masks
			transform.Find("FirstPersonCharacter/GunCamera/Gun").gameObject.layer = 10;
			
		}
		else
		{
			StartCoroutine("UpdateData");
		}
	}


	IEnumerator UpdateData()
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
			anim.SetBool ("Aim", aim);
			anim.SetBool ("Sprint", sprint);
			yield return null;
		}
	}

	void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info)
	{
		// DL - Stream Input
		if(stream.isWriting)
		{
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
			stream.SendNext(health);
			stream.SendNext(anim.GetBool ("Aim"));
            stream.SendNext(anim.GetBool ("Sprint"));
		}
		// DL - Stream Output. Read/Write order must be the same
		else
		{
			position = (Vector3) stream.ReceiveNext();
			rotation = (Quaternion) stream.ReceiveNext();
			health = (float)stream.ReceiveNext();
			aim = (bool)stream.ReceiveNext();
			sprint = (bool)stream.ReceiveNext();
		}
	}



	[PunRPC]
	public void GetShot(float damage, string enemyName)
	{
		health -= damage;
		if (health <= 0 && photonView.isMine)
		{
			string myName = PhotonNetwork.player.name;
			if(SendNetworkScore != null) // Update the scoreboard data
			{
				SendNetworkScore(enemyName, myName);	}

			if(SendNetworkMessage != null) // send messaging of the frag event
				SendNetworkMessage(myName + " was killed by " + enemyName);

			if(RespawnMe != null)
				RespawnMe(3f);
		
			PhotonNetwork.Destroy (gameObject);
		}
	}


}
