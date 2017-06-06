using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {

	private Animator anim;

	public GameObject lightAnimationObject;

	void Start () {
		anim = GetComponent<Animator> ();
	}

	public void TeleportFor (Vector3 _target) {

		GetComponent<PlayerController> ().teleporting = true;

		transform.LookAt (_target);

		Vector3 distance = _target - transform.position;

		lightAnimationObject.transform.localScale = new Vector3 (
			lightAnimationObject.transform.localScale.x,
			lightAnimationObject.transform.localScale.y,
			CalculateScale (1.0f, 6.0f, Vector3.Magnitude (distance))
		);

		anim.SetBool ("start", true);
	}

	private float CalculateScale (float a, float b, float c) {
		return a * c / b * 1.05f;
	}
}
