using UnityEngine;
using System.Collections;

public class DartScript : WeaponBase {

	public string owner;
	public float moveSpeed;
	public Rigidbody rb;
	public Collider col;
	public float dartRange;
	public bool shot = false;
	private bool airborne = false;
	private bool killMe = false;
	public float damage = 100f;

	[SerializeField] float selfdestructTime;
	[SerializeField] float hitTime = 0;

	void Start() 
	{
		
		rb = GetComponent<Rigidbody>();
		col = GetComponent<Collider>();
		shot = true;
	}

	void FixedUpdate() 
	{
		
		if (shot)
		{
			//Initiate velocity forward
			rb.velocity = (transform.forward * moveSpeed);

			//Turn on gravity, make "airborne" for interaction/physics, stop shot so velocity isn't continously applied
			rb.useGravity = true;
			airborne = true;
			shot = false;
		}

		if (airborne)
		{
			//Rotate towards velocity
			RaycastHit hit;
			rb.transform.LookAt( rb.transform.position + rb.velocity);
			if(Physics.Raycast(transform.position, transform.forward, out hit, dartRange))
				{
					
					if(hit.transform.tag == "Enemy")
					{
						hit.transform.GetComponent<PhotonView>().RPC ("GetShot", PhotonTargets.All, damage, owner);
						Destroy(gameObject);
					}
					if(hit.transform.tag == "Ground")
					{
						Destroy(gameObject);
					}
					if(hit.transform.tag == "Player")
					{
						return;
					}
					
					//COME BACK TO THIS. Currently happens too far from the collision
					if(hit.transform.tag == "Map")
					{
				
						//Stop Physics and stick to what we hit
						if (killMe == false)
						{
							hitTime = (Time.time + selfdestructTime);
							rb.useGravity = false;
							rb.isKinematic = true;
							col.GetComponent<Collider>().enabled = false;
							rb.transform.parent = hit.transform;
							killMe = true;
						}


						
					}
					
				}

		}

		if (killMe == true && Time.time >= hitTime)
		{
			Destroy(gameObject);
		}

	}


	[PunRPC]
	public void NameDartRPC (string playerName)
	{
		owner = playerName;
	}

}