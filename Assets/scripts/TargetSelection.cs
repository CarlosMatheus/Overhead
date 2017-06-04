using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSelection : MonoBehaviour {

	[Header("To assign")]
	public float maximumHealth = 100.0f;
	public GameObject enemyHealthBar;

	[Header("Auto assign (No need to assign)")]
	public Material originalMaterial;
	public float HP;

	void Start () {
		HP = maximumHealth;
		originalMaterial = GetComponent<MeshRenderer> ().material;
		enemyHealthBar.transform.rotation = GameObject.FindGameObjectWithTag ("MainCamera").gameObject.transform.rotation;
	}

	void OnMouseDown ()
	{
		GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().SetTarget (this.gameObject);
	}

	void OnTriggerEnter (Collider other)
	{
		HP -= 10.0f;//other.gameObject.GetComponent<SkillAttributes>.hitAmount;
	}
}
