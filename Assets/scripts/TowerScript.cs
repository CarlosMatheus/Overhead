using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TowerScript : MonoBehaviour {

	[Header("Unity Setup Fields")]

	public Transform partToRotate;
	public Transform firePoint;
	public Transform playerSpawnOnTower;
	public GameObject bulletPrefab;
    [SerializeField] private GameObject rangeObject = null;

	private float fireCountdown = 1f;
	private float turnSpeed = 10f;
	private Transform target;
	private GameObject player;
	private PropertiesManager pm;
    private GameObject gameMaster;
    private MouseCursorManager mouseCursorManage;

	public GameObject GetTarget() {
		if (target != null)
			return target.gameObject;
		else
			return null;
	}

    public void AppearRange() {
		if (rangeObject == null)
			return;
        rangeObject.SetActive(true);
    }

	public void DisappearRange() {
		if (rangeObject == null)
			return;
        rangeObject.SetActive(false);
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

	void Start () {

        gameMaster = GameObject.FindWithTag("GameMaster");
        mouseCursorManage = gameMaster.GetComponent<MouseCursorManager>();

		// Finding the player gameObject
		player = GameObject.FindGameObjectWithTag ("Player");

		if (bulletPrefab == null)
			return;

		pm = GetComponent<PropertiesManager> ();

		// Set skill values from prefab
		pm.SetValues (bulletPrefab.GetComponent<SkillsProperties> ());

		//This will reapeat every 0.5 sec
		InvokeRepeating ("UpdateTarget", 0f, 0.5f);

		SetRangeObject ();

	}

	void Update () {
		if (bulletPrefab == null)
			return;
		
		//do nothing in case there is no target
		if (target == null)
			return;

		FollowRotation ();
		Fire ();

	}

	//Check the array of enemies, find the closest, see if it is on range and target it
	private void UpdateTarget () {
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;
		foreach (GameObject enemy in enemies) {
			float distanceToEnemy = Vector3.Distance (transform.position, enemy.transform.position);
			if (distanceToEnemy < shortestDistance) {
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}
		if (nearestEnemy != null && shortestDistance <= GetRange()) 
        {
            if (IsInCorrectScene())
            {
                target = nearestEnemy.transform;
                if (IsAround(playerSpawnOnTower, player.transform))
                {
                    if
                    (
							player.GetComponent<PlayerController>().GetTarget() == null ||
							!player.GetComponent<PlayerController>().IsInRange
                        (
								player.GetComponent<PlayerController>().GetTarget().transform,
                            player.transform
                        )
                    )
                    {

                        player.GetComponent<PlayerController>().SetTarget(nearestEnemy);   // Redefine player target

                    }
                }
            }
		} 
        else 
        {
			target = null;
		}
	}

	// Rotation to follow the enemy direction
	private void FollowRotation(){
		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
		partToRotate.rotation = Quaternion.Euler (0f, rotation.y, 0f);
	}

	// Will make it fire with the right rate
	private void Fire(){
		if( fireCountdown <= 0f ){
			Shoot ();
			fireCountdown = bulletPrefab.GetComponent<SkillsProperties> ().GetCooldown ();
		}
		fireCountdown -= Time.deltaTime;
	}

	// Will instantiete the shot and make it fallow the target
	private void Shoot(){
		GameObject spellGO = (GameObject)Instantiate (bulletPrefab, firePoint.position, firePoint.rotation);
		TowerSpell towerSpell = spellGO.GetComponent<TowerSpell>();
		SkillsProperties skillPro = spellGO.GetComponent<SkillsProperties> ();

		// Set tower values on instantiated skill prefab
		skillPro.SetDamage (GetDamage ());
		skillPro.SetCooldown (GetCooldown ());
		skillPro.SetRange (GetRange ());
		skillPro.SetSideEffectValues (GetBurnValue (), GetSlowFactor ());

		// Speel need to know who instantiated him
		skillPro.SetInvoker (gameObject);

		if (towerSpell != null)
			towerSpell.Seek (target);
	}

    private bool IsAround (Transform trA, Transform trB) {

		if (Vector3.Magnitude (trA.position - trB.position) < 1.0f)
			return true;
		else
			return false;
	}

    private bool IsInCorrectScene()
    {
        return (SceneManager.GetActiveScene().buildIndex != 0 && string.Equals(SceneManager.GetActiveScene().name, "MainMenu") == false);
    }

    private void SetRangeObject()
    {
        if (IsInCorrectScene())
        {
			rangeObject.transform.localScale = new Vector3(GetRange() * 2, 0.01f, GetRange() * 2);
            rangeObject.SetActive(false);
        }
	}

    private void OnMouseEnter()
    {
        if( IsAround(player.transform,playerSpawnOnTower) == false )
        {
            mouseCursorManage.SetTeleportCursor();
        }
    }

    private void OnMouseExit()
    {
        mouseCursorManage.SetIdleCursor();
    }
}