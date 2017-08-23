using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideEffect : MonoBehaviour {

	[Header("Side Effects Atributes")]
	public float duration = 1.0f;

	[Header("Freeze Effect")]
	public bool freeze = false;
	public float slowFactor = 0.9f;

	[Header("Burn Effect")]
	public bool burn = false;
	public float burnRate = 1.0f;

	private GameObject target;

	public void SetTarget (GameObject _target) {
		target = _target;
	}

	public void StartEffect () {
		
		StartCoroutine (AutoDestroy ());

		if (!target.GetComponent<TargetSelection> ().IsSideAffected ()) {

			target.GetComponent<TargetSelection> ().SetSideEffect (true);

			if (freeze)
				Freeze ();
			else if (burn)
				StartCoroutine (Burn ());
		} else
			Destroy (this.gameObject);
	}

	void Update () {

		if (target != null) {
			// Follow target
			transform.position = target.transform.position;
		}
	}

	void Freeze () {
		target.GetComponent<Enemy> ().SetSpeed( target.GetComponent<Enemy> ().GetSpeed() * slowFactor );
	}

	IEnumerator Burn () {
		while (true) {
			
			if (target != null) {
				target.GetComponent<TargetSelection> ().TakeDamageBy (this.gameObject);
			}

			yield return new WaitForSeconds (0.1f / burnRate);
		}
	}

	IEnumerator AutoDestroy () {
		yield return new WaitForSeconds (duration);

		if (target != null) {  // If target have not died yet
			target.GetComponent<Enemy> ().ReturnToOriginalSpeed ();
			target.GetComponent<TargetSelection> ().SetSideEffect(false);
		}

		Destroy (gameObject);
	}
}
