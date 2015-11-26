using UnityEngine;
using System.Collections;

public class Breakable : MonoBehaviour {

public MeshCollider[] piece;
public Rigidbody[] rigidbody;
public int numberOfPieces;
public bool broken = false;
public float selfDestructTime;
public float colliderDelay; // this aids the dynamics of the door shatter
public Collider collider;

	// Use this for initialization
	void Start () {
		collider = this.transform.GetComponent<Collider>();
	
		for(int i = 0; i < numberOfPieces; i++){
			Transform c = this.transform.GetChild(i);
			piece[i] = c.GetComponent<MeshCollider>();
			rigidbody[i] = c.GetComponent<Rigidbody>();		   
		   //foreach (MeshCollider piece in pieces){
           //piece.Value = GameObject.GetComponentInChildren<MeshCollider>();
		}

	}

	//void OnTriggerEnter(Collider other){
	//	broken = true;
	//}

	void Update (){
		if (broken == true){
			for(int i = 0; i < numberOfPieces; i++){
			rigidbody[i].isKinematic = false;
			}
			StartCoroutine(SelfDestruct());
		}
	}

	IEnumerator SelfDestruct() {
			yield return new WaitForSeconds(colliderDelay);
			collider.enabled = false;	
	  		yield return new WaitForSeconds(selfDestructTime); //Cooldown
	  		Destroy(gameObject);
  		}
	
	//FOR TESTING
		// void Update () {
		//	if(Input.GetKeyDown("space")) {yeahy
			//	for(int i = 0; i < numberOfPieces; i++){
			//	rigidbody[i].isKinematic = false;}}

	

}
