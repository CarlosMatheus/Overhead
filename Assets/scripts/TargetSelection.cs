using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSelection : MonoBehaviour {

	[Header("To assign")]
	public GameObject enemyHealthBar;
	public GameObject deathEffect;

	[Header("Auto assign (No need to assign)")]
	public float HP;
	public bool sideAffected = false;

	private float maximumHealth;
	private Enemy enemy;
	private SoulsCounter soulsCounter;

	public float getMaximumHealth(){
		return maximumHealth;
	}

	public void TakeDamageBy (GameObject other)
	{
		HP -= other.GetComponent<SkillsProperties> ().damage;
		if (HP <= 0)
			DeathBy (other.GetComponent<SkillsProperties> ().invoker);
	}

	void Start ()
	{
		enemy = gameObject.GetComponent<Enemy> ();

		// Setting current HP to maximumHP
		HP = enemy.getHP();
		maximumHealth = HP;

		// Rotating enemy UI (health bar) at beginning and desactivating it
		enemyHealthBar.transform.rotation = GameObject.FindGameObjectWithTag ("MainCamera").gameObject.transform.rotation;
		enemyHealthBar.SetActive (false);

		soulsCounter = SoulsCounter.instance;
	}

	void Update () {

		// Rotating enemy UI (health bar) at beginning and desactivating it
		enemyHealthBar.transform.rotation = GameObject.FindGameObjectWithTag ("MainCamera").gameObject.transform.rotation;
	}

	void OnMouseDown ()  // If this.gameObject had been clicked
	{
		// Target it! (logic for untargetting happens on GameController script)
		GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ().SetTarget (this.gameObject);
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

		// Get souls for your kill
		soulsCounter.KillEnemy (go.tag);

		// Anyway, instantiate this target's deathEffect and destroy it
		GameObject effectInstantiated = (GameObject) Instantiate (deathEffect, transform.position, transform.rotation);
		Destroy (effectInstantiated, 2f);
		Destroy (gameObject);
	}
}
