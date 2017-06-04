using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	[Header("To assign")]
	public Slider healthBar;
	public Material targetMaterial;
	public Transform shotSpawn;

	[Header("Auto assign")]
	public GameObject currentTarget;
	public GameObject currentSkill;

	// Private stuff
	private GameObject enemyHealthBar;
	private float maximumHealth = 100.0f;
	private float currentHealth;
	private float attackCouldown;
	private float time;

	const int FIRST = 0, SECOND = 1;

	void Start () {
		
		// Setting player health
		currentHealth = maximumHealth;
		healthBar.value = SetHealth(currentHealth);

		// Turnning healthBar to camera
		healthBar.gameObject.transform.rotation = GameObject.FindGameObjectWithTag ("MainCamera").gameObject.transform.rotation;

		// Setting time reference
		time = Time.time;
	}

	void Update () {
		
		if (enemyHealthBar != null) {
			
			if (enemyHealthBar.activeInHierarchy) {   // If had target someone
			
				// Shows enemy (target) HP
				enemyHealthBar.GetComponent<Slider> ().value = SetHealth (currentTarget.GetComponent<TargetSelection> ().HP);

			}
		}

		if (currentTarget != null) {
			// Look at target
			transform.LookAt (currentTarget.transform.position);

			if (Time.time - time > currentSkill.GetComponent<SkillsProperties> ().cooldown
				&& IsInRange(transform, currentTarget.transform)) {
				// Attack!
				Attack ();
			}

			// Holds healthBar turned to camera at every frame rotation (healthBar is atached with player object in hierarchy
			healthBar.gameObject.transform.rotation = GameObject.FindGameObjectWithTag ("MainCamera").gameObject.transform.rotation;
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

	// Function to instantiate a skill depending on what tower he is
	void Attack () {
		Instantiate (currentSkill, shotSpawn.position, shotSpawn.rotation);
	}
		
	bool IsInRange (Transform trA, Transform trB) {

		if (Vector3.Magnitude (trA.position - trB.position) <= currentSkill.GetComponent<SkillsProperties> ().range)
			return true;
		else
			return false;

	}
}
