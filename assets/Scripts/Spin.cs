using UnityEngine;
using System.Collections;

public class Spin : MonoBehaviour {
	public float spinAmt = 40.0f;

	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.up,Time.deltaTime * spinAmt);
	}
}
