using UnityEngine;
using System.Collections;

public class Dart_Force : MonoBehaviour {

	public float moveSpeed;
	public Rigidbody rb;
	public Collider col;
	public float dartRange;
	public bool shot = false;
	private bool airborne = false;
	private bool casting = false;

	void Start() 
	{
		rb = GetComponent<Rigidbody>();
		col = GetComponent<Collider>();
	}

	void FixedUpdate() 
	{
		//Redo this when weapons are figured out
		if (Input.GetButton("Fire1"))
		{
			shot = true;
		}


		if (shot)
		{
			//Initiate velocity forward
			rb.velocity += (transform.forward * moveSpeed);

			//Turn on gravity, make "airborne" for interaction/physics, stop shot so velocity isn't continously applied
			rb.useGravity = true;
			airborne = true;
			shot = false;
			casting = true;
		}

		if (airborne)
		{
			//Rotate towards velocity
			casting = false;
			RaycastHit hit;
			rb.transform.LookAt( rb.transform.position + rb.velocity);
			if(Physics.Raycast(transform.position, transform.forward, out hit, dartRange))
				{
					if(hit.transform.tag == "Player")
					{
						Destroy(hit.transform.gameObject);
					}
					if(hit.transform.tag == "Ground")
					{
						Destroy(gameObject);
					}
					else
					{
						Debug.Log ("No Hit");
					}
				}

		}

	}

	void OnCollisionEnter (Collision other)
	{
		if (airborne)
		{
			//Stop Physics and stick to what we hit
			rb.useGravity = false;
			rb.isKinematic = true;
			col.GetComponent<Collider>().enabled = false;
			rb.transform.parent = other.transform;
		}
	}

}