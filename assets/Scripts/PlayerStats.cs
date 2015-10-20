using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using PhotonHashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerStats : MonoBehaviour {
	
public string playerMe;
public float playerFrag;
public float playerDeath;
public float playerAmmo;
public GameObject vitalsPanel;
public ScoreManager scoreManager;

PhotonView photonView;

void Start ()
{
	//get the photonView
photonView = GetComponent<PhotonView> ();


}

void OnJoinedRoom ()
	{
		playerFrag = 0;
		playerDeath = 0;
		playerAmmo = 3;
		//Turn my username into a string
		playerMe = PhotonNetwork.player.name;

	}


[PunRPC]
void AddScore_RPC(string fragger, string fragged)
{


	//If I'm the killer, add to my frags, call AddMessage and say how many opponents I've defeated
	if(fragger == playerMe)
	{
		PhotonHashtable PlayerCustomProps = new PhotonHashtable();
		PlayerCustomProps["kills"] = ((playerFrag + 1).ToString());
		PhotonNetwork.player.SetCustomProperties(PlayerCustomProps);
		playerFrag = (float.Parse(PlayerCustomProps["kills"].ToString()));
		AddMessage (fragger + " has defeated " + playerFrag + " opponents.");
		
		

	}

	if(fragged == playerMe)
	{
		PhotonHashtable PlayerCustomProps = new PhotonHashtable();
		PlayerCustomProps["Deaths"] = ((playerDeath + 1).ToString());
		PhotonNetwork.player.SetCustomProperties(PlayerCustomProps);
		playerDeath = (float.Parse(PlayerCustomProps["Deaths"].ToString()));
		AddMessage (fragged + " has perished " + playerDeath + " times.");
		
		
	}

	scoreManager.ChangeScore(fragger, "kills", 1);
	scoreManager.ChangeScore(fragged, "deaths", 1);

}

//Call AddMessage to report to the chat, pass through death or frag message
void AddMessage(string message)
{
	//               what we call       who sent to       parameter (what passes)
	photonView.RPC ("AddMessage_RPC", PhotonTargets.All, message);
}

}
