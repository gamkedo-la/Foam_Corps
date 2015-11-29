using UnityEngine;
using System.Collections;

public class SodaGrenScript : WeaponBase {
	
	[SerializeField] string splashPrefab;
	[SerializeField] string dripZonePrefab;

	private float ceilingY = 3.03f;

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
		SoundCenter.instance.PlayClipOn(
			SoundCenter.instance.sodaSplash,transform.position);
		PhotonNetwork.Instantiate(splashPrefab, transform.position, Quaternion.identity, 0);
		Vector3 dripZoneOrigin = transform.position;
		dripZoneOrigin.y = ceilingY;
		PhotonNetwork.Instantiate(dripZonePrefab, dripZoneOrigin,
		                          Quaternion.AngleAxis(90.0f,Vector3.right), 0);
		Destroy(gameObject);
	}

	void OnCollisionEnter() {
		SodaBurst();
	}
	
	[PunRPC]
	public void NameSodaGrenRPC (string playerName)
	{
		owner = playerName;
	}
	
}