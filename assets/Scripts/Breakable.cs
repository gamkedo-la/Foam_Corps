using UnityEngine;
using System.Collections;

public class Breakable : MonoBehaviour {

public MeshCollider[] piece;
public Rigidbody[] rigidbody;
public int numberOfPieces;

	// Use this for initialization
	void Start () {
	
		for(int i = 0; i < numberOfPieces; i++){
			Transform c = this.transform.GetChild(i);
			piece[i] = c.GetComponent<MeshCollider>();
			rigidbody[i] = c.GetComponent<Rigidbody>();

			
		   
		   //foreach (MeshCollider piece in pieces){
           //piece.Value = GameObject.GetComponentInChildren<MeshCollider>();
		}

	}

	void OnTriggerEnter(Collider other){
		for(int i = 0; i < numberOfPieces; i++){
			rigidbody[i].isKinematic = false;
		}
	}
	
	//FOR TESTING
		// void Update () {
		//	if(Input.GetKeyDown("space")) {
			//	for(int i = 0; i < numberOfPieces; i++){
			//	rigidbody[i].isKinematic = false;}}

	

}
