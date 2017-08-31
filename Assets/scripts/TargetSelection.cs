using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TargetSelection : MonoBehaviour {

	[Header("To assign")]
	public GameObject enemyHealthBar;
	public GameObject deathEffect;

    public GameObject soulCanvas;

	private float HP;
	private bool sideAffected = false;

	private float maximumHealth;
	private Enemy enemy;
	private SoulsCounter soulsCounter;
    private float score;

	public float GetHP () {
		return HP;
	}

	public bool IsSideAffected () {
		return sideAffected;
	}

	public void SetSideEffect (bool boolean) {
		sideAffected = boolean;
	}

	public float getMaximumHealth(){
		return maximumHealth;
	}

	public void TakeDamageBy (GameObject other)
	{
		HP -= other.GetComponent<SkillsProperties> ().GetDamage();
		if (HP <= 0)
			DeathBy (other.GetComponent<SkillsProperties> ().GetInvoker());
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
        if (IsInCorrectScene())
        {
            // Target it! (logic for untargetting happens on GameController script)
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().SetTarget(this.gameObject);
        }
	}

	// This.gameObject had been killed by GameObject go
	void DeathBy (GameObject go)
	{
		// If player was killing this.gameObject
		if (go.tag == "Player") {
			if (go.GetComponent<PlayerController> ().GetTarget() != null) {
				if (go.GetComponent<PlayerController> ().GetTarget().transform == this.gameObject.transform) {
					go.GetComponent<PlayerController> ().SetTarget (null);
					go.GetComponent<PlayerController> ().FindNewTarget ();   // Finds a new target
				}
			}
		}

		// Get souls for your kill
        score = soulsCounter.KillerPrice(go.tag);
        soulsCounter.KillEnemy(score);

        //Instantiate soul number
        GameObject soulCanvasObject = Instantiate(soulCanvas, transform.position, transform.rotation);
        soulCanvasObject.GetComponentInChildren<Text>().text = "+" + score.ToString();
        Debug.Log(score.ToString());

		// Anyway, instantiate this target's deathEffect and destroy it
		GameObject effectInstantiated = (GameObject) Instantiate (deathEffect, transform.position, transform.rotation);
		Destroy (effectInstantiated, 2f);
		Destroy (gameObject);
	}

    private bool IsInCorrectScene()
    {
        return (SceneManager.GetActiveScene().buildIndex != 0 && string.Equals(SceneManager.GetActiveScene().name, "MainMenu") == false);
    }

}
