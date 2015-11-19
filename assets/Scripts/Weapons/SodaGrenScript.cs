using UnityEngine;
using System.Collections;

public class SodaGrenScript : WeaponBase {
	
	[SerializeField] string splashPrefab;

	public string owner;
	public float moveSpeed;
	private Rigidbody rb;
	private Collider col;
	public bool shot = false;
	private bool airborne = false;
	public float damage = 100f;
	
	[SerializeField] float selfdestructTime;
	[SerializeField] float hitTime = 0;
	
	void Start() 
	{		
		rb = GetComponent<Rigidbody>();
		col = GetComponent<Collider>();
		shot = true;
		hitTime = (Time.time + selfdestructTime);
		rb.velocity = (transform.forward * moveSpeed + transform.up * moveSpeed * 0.3f);
		rb.angularVelocity = Random.onUnitSphere * 100.0f;
		airborne = true;
		shot = false;
		StartCoroutine(AutoBurstTimer());
	}

	IEnumerator AutoBurstTimer() {
		yield return new WaitForSeconds(7.0f);
		SodaBurst();
	}
	
	void SodaBurst() {
		PhotonNetwork.Instantiate(splashPrefab, transform.position, Quaternion.identity, 0);
		Destroy(gameObject);
	}

	void OnCollisionEnter() {
		SodaBurst();
	}
	
	[PunRPC]
	public void NameDartRPC (string playerName)
	{
		owner = playerName;
	}
	
}