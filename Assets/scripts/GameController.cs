using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	const int FIRST = 0, SECOND = 1;

	[Header("To assign")]

	public Slider healthBar;
	public Material targetMaterial;

	private GameObject enemyHealthBar;
	private GameObject currentTarget;
	private float maximumHealth = 100.0f;
	private float currentHealth;

	// Use this for initialization
	void Start () {
		currentHealth = maximumHealth;
		healthBar.value = SetHealth(currentHealth);
		healthBar.gameObject.transform.rotation = GameObject.FindGameObjectWithTag ("MainCamera").gameObject.transform.rotation;
	}

	void Update () {
		if (enemyHealthBar != null) {
			if (enemyHealthBar.activeInHierarchy) {   // If had target someone
			
				// Shows enemy HP
				enemyHealthBar.GetComponent<Slider> ().value = SetHealth (currentTarget.GetComponent<TargetSelection> ().HP);

			}
		}
	}

	public void SetTarget (GameObject _newTarget) {
		
		if (currentTarget != null) {
			// Sets target material (color, specifically) int original one
			currentTarget.GetComponent<MeshRenderer> ().material = currentTarget.GetComponent<TargetSelection> ().originalMaterial;

			// If had double clicked the same target, just destarget it
			if (_newTarget == currentTarget) {
				enemyHealthBar.SetActive (false);
				currentTarget = null;
				return;
			} else {
				enemyHealthBar.SetActive (true);
			}
		}

		// Turns new target into currentTarget
		currentTarget = _newTarget;
		enemyHealthBar = _newTarget.GetComponent<TargetSelection>().enemyHealthBar;

		// Turns targetMaterial color into originalMaterial color and atribute it to targetObject
		targetMaterial.color = currentTarget.GetComponent<TargetSelection> ().originalMaterial.color;
		currentTarget.GetComponent<MeshRenderer> ().material = targetMaterial;

		// Active HP on top screen
		enemyHealthBar.SetActive (true);
	}

	float SetHealth (float currentHealth) {
		return currentHealth / maximumHealth;
	}
}
