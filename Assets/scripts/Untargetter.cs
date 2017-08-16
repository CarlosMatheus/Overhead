using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Untargetter : MonoBehaviour {

	void OnMouseDown ()
	{
		if (GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ().GetTarget() != null) {
			GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ().SetTarget
			(
				GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ().GetTarget()
			);
		}
	}
}
