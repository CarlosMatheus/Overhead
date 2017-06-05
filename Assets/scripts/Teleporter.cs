using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {

	private Animator anim;

	void Start () {
		anim = GetComponent<Animator> ();
	}

	public void Teleport () {
		transform.LookAt (GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ().currentTarget.transform.position);

		anim.SetBool ("start", true);
	}
}
