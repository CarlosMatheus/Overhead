using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	[Header("To assign")]
	public Material targetMaterial;
	public Material inRangeMaterial;
	public Transform shotSpawn;

	[Header("Auto assign")]
	public GameObject currentTarget;
	public GameObject currentSkill;
	public GameObject currentTower;

	// Private stuff
	private GameObject enemyHealthBar;
	private Material materialToAssign;
	private float attackCouldown;
	private float time;

	const int FIRST = 0, SECOND = 1;

	// Function to be called by the new target when clicked
	public void SetTarget (GameObject _newTarget) {

		if (currentTarget != null) {

			// Sets target material (color in actual one and emission on tempMaterial) into original one
			SetMaterial (currentTarget.GetComponent<TargetSelection> ().originalMaterial);
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

		// Active HP on top screen
		enemyHealthBar.SetActive (true);

		// Turns targetMaterial color into originalMaterial color and atribute it to targetObject
		SetMaterial (materialToAssign);
	}

	// Search for a new target (this is only called when a enemy dies killed by a player)
	public void FindNewTarget () {
		if (currentTower.GetComponent<TowerScript> ().GetTarget () != null) {   // If tower has a target, target it!
			SetTarget (currentTower.GetComponent<TowerScript> ().GetTarget ());
		}  // If not, keep null
	}

	void Start () {

		// Setting time reference
		time = Time.time;

		// Setting up material stuff
		materialToAssign = targetMaterial;
	}

	void Update () {
		
		if (enemyHealthBar != null && currentTarget != null) {
			
			if (enemyHealthBar.activeInHierarchy) {   // If had target someone
			
				// Shows enemy (target) HP
				enemyHealthBar.GetComponent<Slider> ().value = SetHealth (currentTarget.GetComponent<TargetSelection> ().HP);

			}
		}

		if (currentTarget != null) {
			// Look at target
			transform.LookAt 
			(
				new Vector3 (
					currentTarget.transform.position.x,
					transform.position.y,
					currentTarget.transform.position.z
				)
			);

			if (IsInRange (transform, currentTarget.transform)) {

				materialToAssign = inRangeMaterial;

				SetMaterial (materialToAssign);

				if (Time.time - time > currentSkill.GetComponent<SkillsProperties> ().cooldown) {
					// Attack!
					Attack ();
					time = Time.time;
				}
			} else {
				materialToAssign = targetMaterial;
				SetMaterial (materialToAssign);
			}
		}
	}

	// Function to set health valor into "canvas language"
	float SetHealth (float currentHealth) {
		
		return currentHealth / currentTarget.GetComponent<TargetSelection>().maximumHealth;
	}

	// Function to instantiate a skill depending on what tower he is
	void Attack () {
		currentSkill.GetComponent<TowerSpell> ().Seek (currentTarget.transform);
		GameObject currentSpell = (GameObject) Instantiate (currentSkill, shotSpawn.position, shotSpawn.rotation);
		currentSpell.GetComponent<SkillsProperties> ().invoker = this.gameObject;
	}

	// Verify if A and B are in range
	public bool IsInRange (Transform trA, Transform trB) {

		if (Vector3.Magnitude (trA.position - trB.position) <= currentSkill.GetComponent<SkillsProperties> ().range)
			return true;
		else
			return false;

	}

	void SetMaterial (Material mat) {
		mat.color = currentTarget.GetComponent<TargetSelection> ().originalMaterial.color;
		currentTarget.GetComponent<MeshRenderer> ().material = mat;
		currentTarget.GetComponent<TargetSelection> ().tempMaterial = mat;
	}
}
