using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour {

	public float distanceFromCamera = 5.0f;

	void Update () {
		Vector3 mousePosition = Input.mousePosition;
		mousePosition.z = distanceFromCamera;

		Vector3 mouseScreenToWorld = Camera.main.ScreenToWorldPoint(mousePosition);

		transform.position = mouseScreenToWorld;
	}
}
