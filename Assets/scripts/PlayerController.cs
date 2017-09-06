using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	[Header("To assign")]
	public Transform shotSpawn;

	[Header("Auto assign")]
	public GameObject currentSkill;
	public GameObject currentTower;
	public bool teleporting = false;

	// Private stuff
	private GameObject currentTarget;
	private GameObject enemyHealthBar;
	private float attackCouldown;
	private float time;
	private PropertiesManager pm;

	const int FIRST = 0, SECOND = 1;

    #region Set and get methods

    // Function to be called by the new target when clicked
    public void SetTarget (GameObject _newTarget) {
		
		if (_newTarget != null) {
			
			if (currentTarget != null) {
				
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
			enemyHealthBar = currentTarget.GetComponent<TargetSelection> ().enemyHealthBar;

			// Active HP on top screen
			enemyHealthBar.SetActive (true);

			return;
		}

		currentTarget = _newTarget;
	}

	// Search for a new target (this is only called when a enemy dies killed by a player)
	public void FindNewTarget () {
		if (currentTower.GetComponent<TowerScript> ().GetTarget () != null) {   // If tower has a target, target it!
			SetTarget (currentTower.GetComponent<TowerScript> ().GetTarget ());
		}  // If not, keep null
	}

	public GameObject GetTarget () {
		return currentTarget;
	}

	public float GetDamage () {
		return pm.GetDamage();
	}

	public float GetCooldown () {
		return pm.GetCooldown ();
	}

	public float GetRange () {
		return pm.GetRange ();
	}

	public float GetBurnValue () {
		return pm.GetBurnRate ();
	}

	public float GetSlowFactor () {
		return pm.GetSlowFactor ();
	}

	public float GetRangeRadius () {
		return pm.GetRangeRadius ();
	}

	public float GetEffectDuration () {
		return pm.GetEffectDuration ();
	}
#endregion

    void Start () {
		// Get component to properties data manager
		pm = GetComponent<PropertiesManager> ();

		// Set skill values from prefab
		pm.SetValues(currentSkill.GetComponent<SkillsProperties>());

		// Setting time reference
		time = Time.time;
	}

	void Update () {
		
		if (enemyHealthBar != null && currentTarget != null) {
			
			if (enemyHealthBar.activeInHierarchy) {   // If had target someone
			
				// Shows enemy (target) HP
				enemyHealthBar.GetComponent<Slider> ().value = SetHealth (currentTarget.GetComponent<TargetSelection> ().GetHP());

			}
		}

		if (currentTarget != null) {

			if (!teleporting) {
				
				// Look at target
				transform.LookAt 
				(
					new Vector3 (
						currentTarget.transform.position.x,
						transform.position.y,
						currentTarget.transform.position.z
					)
				);
			}

			if (IsInRange (transform, currentTarget.transform)) {

				if (Time.time - time > GetCooldown() ) {
					// Attack!
					Attack ();
					time = Time.time;
				}
			}
		}
	}

	// Function to set health valor into "canvas language"
	float SetHealth (float currentHealth) {
		
		return currentHealth / currentTarget.GetComponent<TargetSelection>().getMaximumHealth();
	}

	// Function to instantiate a skill depending on what tower he is
	void Attack () {
		currentSkill.GetComponent<TowerSpell> ().Seek (currentTarget.transform);

		GameObject currentSpell = (GameObject) Instantiate (currentSkill, shotSpawn.position, shotSpawn.rotation);
		SkillsProperties skillPro = currentSpell.GetComponent<SkillsProperties> ();

		// Pass PropertiesManager's information to prefab's SkillsProperties
		skillPro.SetDamage (GetDamage());
		skillPro.SetCooldown (GetCooldown());
		skillPro.SetRange (GetRange());
		skillPro.SetSideEffectValues (GetBurnValue (), GetSlowFactor (), GetRangeRadius (), GetEffectDuration ());

		// Spell need to know who instantiated him
		skillPro.SetInvoker (this.gameObject);
	}

	// Verify if A and B are in range
	public bool IsInRange (Transform trA, Transform trB) {

		Vector3 a = trA.position;
		Vector3 b = trB.position;

		a.y = 0;
		b.y = 0;

		if (Vector3.Magnitude (a - b) <= GetRange())
			return true;
		else
			return false;

	}
}
