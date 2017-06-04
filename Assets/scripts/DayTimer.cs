using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayTimer : MonoBehaviour {

	public Light aux_light;

	private Light sun;

	void Start () {
		sun = GetComponent<Light> ();
	}

	void Update () {
		sun.transform.RotateAround (new Vector3 (0, 0, 0), new Vector3 (1, 1, 0), Time.deltaTime * 0.5f);
	}
}
