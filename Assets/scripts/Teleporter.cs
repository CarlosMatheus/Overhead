using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {

	private Animator anim;

	public GameObject lightAnimationObject;

	void Start () {
		anim = GetComponent<Animator> ();
	}

	public void TeleportFor (GameObject _target) {

		GetComponent<PlayerController> ().teleporting = true;

		transform.LookAt (_target.transform.position);

		EffectScaler.Scaler (lightAnimationObject, _target, GameObject.FindGameObjectWithTag("Player"), 6.0f);

		anim.SetBool ("start", true);
	}
}
