using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSelection : MonoBehaviour {

	public float HP;

	public float maximumHealth = 100.0f;
	public Material originalMaterial;

	void Start () {
		HP = maximumHealth;
		originalMaterial = GetComponent<MeshRenderer> ().material;
	}

	void OnMouseDown ()
	{
		GameObject[] target = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().currentTarget;
		GameObject aux = target[0];
		target [0] = this.gameObject;  // Current target
		target [1] = aux;              // Anterior target
	}

	void OnTriggerEnter (Collider other)
	{
		HP -= 10.0f;//other.gameObject.GetComponent<SkillAttributes>.hitAmount;
	}
}
