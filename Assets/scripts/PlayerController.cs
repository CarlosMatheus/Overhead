using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	[Header("To assign")]

	public Slider healthBar;
	public Material targetMaterial;

	[Header("Auto assign")]
	public GameObject currentTarget;

	// Private stuff
	private GameObject enemyHealthBar;
	private float maximumHealth = 100.0f;
	private float currentHealth;

	const int FIRST = 0, SECOND = 1;

	void Start () {
		
		// Sets player health
		currentHealth = maximumHealth;
		healthBar.value = SetHealth(currentHealth);

		// Turns healthBar to camera
		healthBar.gameObject.transform.rotation = GameObject.FindGameObjectWithTag ("MainCamera").gameObject.transform.rotation;
	}

	void Update () {
		
		if (enemyHealthBar != null) {
			
			if (enemyHealthBar.activeInHierarchy) {   // If had target someone
			
				// Shows enemy (target) HP
				enemyHealthBar.GetComponent<Slider> ().value = SetHealth (currentTarget.GetComponent<TargetSelection> ().HP);

			}
		}
	}

	// Function to be called by the new target when clicked
	public void SetTarget (GameObject _newTarget) {
		
		if (currentTarget != null) {
			
			// Sets target material (color in actual one and emission on tempMaterial) into original one
			currentTarget.GetComponent<MeshRenderer> ().material = currentTarget.GetComponent<TargetSelection> ().originalMaterial;
			currentTarget.GetComponent<TargetSelection> ().tempMaterial = currentTarget.GetComponent<TargetSelection> ().originalMaterial;
			currentTarget.GetComponent<TargetSelection> ().enemyHealthBar.SetActive (false);

			// If had double clicked the same target, just destarget it
			if (_newTarget == currentTarget) {
				
				currentTarget = null;
				return;
			}
		}

		// Turns new target into currentTarget
		currentTarget = _newTarget;

		// Gets newTarget HP bar reference
		enemyHealthBar = currentTarget.GetComponent<TargetSelection>().enemyHealthBar;

		// Turns targetMaterial color into originalMaterial color and atribute it to targetObject
		targetMaterial.color = currentTarget.GetComponent<TargetSelection> ().originalMaterial.color;
		currentTarget.GetComponent<MeshRenderer> ().material = targetMaterial;
		currentTarget.GetComponent<TargetSelection> ().tempMaterial = targetMaterial;

		// Active HP on top screen
		enemyHealthBar.SetActive (true);
	}

	// Function to set health valor into "canvas language"
	float SetHealth (float currentHealth) {
		
		return currentHealth / maximumHealth;
	}
}
