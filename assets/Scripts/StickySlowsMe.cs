using UnityEngine;
using System.Collections;

public class StickySlowsMe : MonoBehaviour {
	protected float m_StickyEffectMult = 1.0f;

	public void SpeedStickyZone() {
		m_StickyEffectMult = 0.12f;
		StartCoroutine (UnstickySpeed());
	}
	
	public IEnumerator UnstickySpeed() {
		yield return new WaitForSeconds (6.0f);
		m_StickyEffectMult = 1.0f;
	}
}
