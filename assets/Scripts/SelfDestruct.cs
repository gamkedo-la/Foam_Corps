using UnityEngine;
using System.Collections;

public class SelfDestruct : MonoBehaviour {
	public float lifeTime = 5.0f;

	void Start () {
		Destroy(gameObject,lifeTime);
	}
}
