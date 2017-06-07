using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectScaler : MonoBehaviour {
	
	public static void Scaler (GameObject _toScale, GameObject _target, GameObject _father, float range) {

		Vector3 distance = _target.transform.position - _father.transform.position;

		_toScale.transform.localScale = new Vector3 (
			_toScale.transform.localScale.x,
			_toScale.transform.localScale.y,
			CalculateScale (1.0f, range, Vector3.Magnitude (distance))
		);
	}

	private static float CalculateScale (float a, float b, float c) {
		return a * c / b * 1.05f;
	}
}
