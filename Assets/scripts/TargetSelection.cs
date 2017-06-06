using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSelection : MonoBehaviour {

	[Header("To assign")]
	public float maximumHealth = 100.0f;
	public GameObject enemyHealthBar;
	public GameObject deathEffect;
	public Material overMaterial;

	[Header("Auto assign (No need to assign)")]
	public Material originalMaterial;
	public Material tempMaterial;
	public float HP;

	public void TakeDamageBy (GameObject other)
	{
		HP -= other.GetComponent<SkillsProperties> ().damage;
		if (HP <= 0)
			DeathBy (other.GetComponent<SkillsProperties> ().invoker);
	}

	void Start ()
	{
		// Setting current HP to maximumHP
		HP = maximumHealth;

		// Getting reference for materials to be used later
		originalMaterial = GetComponent<MeshRenderer> ().material;
		tempMaterial = GetComponent<MeshRenderer> ().material;

		// Rotating enemy UI (health bar) at beginning and desactivating it
		enemyHealthBar.transform.rotation = GameObject.FindGameObjectWithTag ("MainCamera").gameObject.transform.rotation;
		enemyHealthBar.SetActive (false);
	}

	void OnMouseDown ()  // If this.gameObject had been clicked
	{
		// Target it! (logic for untargetting happens on GameController script)
		GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ().SetTarget (this.gameObject);
	}

	void OnMouseEnter ()  // If mouse is over this.gameObject
	{
		// Gets current material
		tempMaterial = gameObject.GetComponent<MeshRenderer> ().material;

		// Sets actual material color to overMaterial material
		overMaterial.color = originalMaterial.color;

		// Sets actual material to overMaterial (with emission, but with originalMaterial color)
		gameObject.GetComponent<MeshRenderer> ().material = overMaterial;
	}

	void OnMouseExit ()  // If mouse is not over this.gameObject
	{
		gameObject.GetComponent<MeshRenderer> ().material = tempMaterial;
	}

	// This.gameObject had been killed by GameObject go
	void DeathBy (GameObject go)
	{
		// If player was killing this.gameObject
		if (go.tag == "Player") {
			if (go.GetComponent<PlayerController> ().currentTarget != null) {
				if (go.GetComponent<PlayerController> ().currentTarget.transform == this.gameObject.transform) {
					go.GetComponent<PlayerController> ().currentTarget = null;
					go.GetComponent<PlayerController> ().FindNewTarget ();   // Finds a new target
				}
			}
		}

		// Anyway, instantiate this target's deathEffect and destroy it
		GameObject effectInstantiated = (GameObject) Instantiate (deathEffect, transform.position, transform.rotation);
		Destroy (effectInstantiated, 2f);
		Destroy (gameObject);
	}
}
