using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TargetSelection : MonoBehaviour {

	[Header("To assign")]
	public GameObject enemyHealthBar;
	public GameObject hpText;
	public GameObject deathEffect;

    public GameObject soulCanvas;

	private float HP;
	private bool sideAffected = false;
    private bool isDead = false;

    private ActionManager actionManager;
	private float maximumHealth;
	private Enemy enemy;
	private SoulsCounter soulsCounter;
    private float score;
	private Text text;

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
        if (isDead == true) return;
		HP -= other.GetComponent<SkillsProperties> ().GetDamage();

		if (!enemyHealthBar.activeInHierarchy)
		{
			enemyHealthBar.SetActive(true);
		}

		if (HP <= 0)
		{
			enemyHealthBar.SetActive(false);
			isDead = true;
            DeathBy(other.GetComponent<SkillsProperties>().GetInvoker());
			return;
        }

		Text text = AnimateInfo();
		text.text = "-" + other.GetComponent<SkillsProperties>().GetDamage();
		text.color = Color.red;
		text.fontSize -= 35;
	}

	void Start ()
	{
        isDead = false;

        actionManager = GameObject.FindWithTag("GameMaster").GetComponent<ActionManager>();

		enemy = gameObject.GetComponent<Enemy> ();

		// Setting current HP to maximumHP
		HP = enemy.getHP();
		maximumHealth = HP;

		SetHPTextOnHealthBar();

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
		Text text = AnimateInfo();
		text.text = "+" + score + " Soul";
		if (score > 1)
			text.text = text.text + "s";

		// Anyway, instantiate this target's deathEffect and destroy it
		GameObject effectInstantiated = Instantiate (deathEffect, transform.position, transform.rotation);
		Destroy (effectInstantiated, 2f);

        //tell actionManager that the enemy has been killed
        actionManager.KillEnemy();

		Destroy (gameObject);
	}

    private bool IsInCorrectScene()
    {
        return (SceneManager.GetActiveScene().buildIndex != 0 && string.Equals(SceneManager.GetActiveScene().name, "MainMenu") == false);
    }

	private void SetHPTextOnHealthBar()
	{
		//enemyHealthBar.GetComponentInParent<CanvasScaler>().dynamicPixelsPerUnit = 3;
		Transform fillArea = enemyHealthBar.transform.Find("Fill Area").transform;
		GameObject instText = Instantiate(hpText, fillArea.position, fillArea.rotation, fillArea);
		Rect textRect = instText.GetComponent<RectTransform>().rect;
		textRect.xMax = 0;
		textRect.xMin = 0;
		textRect.yMax = 0;
		textRect.yMin = 0;

		text = instText.GetComponent<Text>();
		text.fontSize = 7;
		text.fontStyle = FontStyle.Bold;
		text.alignment = TextAnchor.MiddleCenter;

		InvokeRepeating("AttHP", 0f, 0.5f);
	}

	private void AttHP ()
	{
		text.text = Mathf.Round(HP).ToString() + " / " + Mathf.Round(maximumHealth).ToString();
		enemyHealthBar.GetComponent<Slider>().value = HP / maximumHealth;
	}

	private Text AnimateInfo()
	{
		GameObject damageCanvasObject = Instantiate(soulCanvas, transform.position + new Vector3(0f, 0.75f, 0f), transform.rotation);
		return damageCanvasObject.GetComponentInChildren<Text>();
	}

	private Text AnimateInfo(float _factor)
	{
		GameObject canvasObject = Instantiate(soulCanvas, transform.position, transform.rotation);
		canvasObject.GetComponent<SoulCounterScript>().speed *= _factor;
		canvasObject.GetComponent<SoulCounterScript>().destroyTime /= _factor;
		return canvasObject.GetComponentInChildren<Text>();
	}
}
