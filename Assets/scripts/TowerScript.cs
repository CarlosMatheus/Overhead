using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TowerScript : MonoBehaviour {

	[Header("Unity Setup Fields")]

	public Transform rangePiece;
	public Transform partToRotate;
	public Transform firePoint;
	public Transform playerSpawnOnTower;
	public GameObject bulletPrefab;
	public GameObject teleportEffect;
	public string enemyTag = "Enemy";
    [SerializeField] private GameObject rangeObject;

	private float fireCountdown = 1f;
	private float turnSpeed = 10f;
	private Transform target;
	private GameObject player;

	// Atributes from spell
	private float damage;
	private float cooldown;
	private float range;

	public GameObject GetTarget() {
		if (target != null)
			return target.gameObject;
		else
			return null;
	}

    public float GetRange()
    {
		return bulletPrefab.GetComponent<SkillsProperties> ().GetRange ();
    }

    public void AppearRange()
    {
        rangeObject.SetActive(true);
    }

    public void DisappearRange()
    {
        rangeObject.SetActive(false);
    }

	public void SetDamage (float multiplicationFactor) {
		damage = multiplicationFactor * bulletPrefab.GetComponent<SkillsProperties> ().GetDamage ();
	}

	public void SetCooldown (float multiplicationFactor) {
		cooldown = multiplicationFactor * bulletPrefab.GetComponent<SkillsProperties> ().GetCooldown ();
	}

	public void SetRange (float multiplicationFactor) {
		range = multiplicationFactor * bulletPrefab.GetComponent<SkillsProperties> ().GetRange ();
	}

	void Start () {
		// Set skill values from prefab
		SetValues (1f);

		// Finding the player gameObject
		player = GameObject.FindGameObjectWithTag ("Player");
        SetRangeObject();

		//This will reapeat every 0.5 sec
		InvokeRepeating ("UpdateTarget", 0f, 0.5f);
	}

	void Update () {
		//do nothing in case there is no target
		if (target == null)
			return;

		FollowRotation ();
		Fire ();
	}

	//Check the array of enemies, find the closest, see if it is on range and target it
	void UpdateTarget () {
		GameObject[] enemies = GameObject.FindGameObjectsWithTag (enemyTag);
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
	void FollowRotation(){
		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
		partToRotate.rotation = Quaternion.Euler (0f, rotation.y, 0f);
	}

	// Will make it fire with the right rate
	void Fire(){
		if( fireCountdown <= 0f ){
			Shoot ();
			fireCountdown = bulletPrefab.GetComponent<SkillsProperties> ().GetCooldown ();
		}
		fireCountdown -= Time.deltaTime;
	}

	// Will instantiete the shot and make it fallow the target
	void Shoot(){
		GameObject spellGO = ( GameObject ) Instantiate (bulletPrefab,firePoint.position,firePoint.rotation);
		TowerSpell towerSpell = spellGO.GetComponent<TowerSpell>();
		SkillsProperties skillPro = spellGO.GetComponent<SkillsProperties> ();

		// Set tower values on instantiated skill prefab
		skillPro.SetDamage (damage);
		skillPro.SetCooldown (cooldown);
		skillPro.SetRange (range);

		// Speel need to know who instantiated him
		skillPro.SetInvoker (gameObject);

		if (towerSpell != null)
			towerSpell.Seek (target);
	}

	void OnDrawGizmosSelected(){
		Gizmos.color = new Color(255,0,0,0.75f);
		Gizmos.DrawWireSphere (rangePiece.position, range);
	}

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (IsAround(playerSpawnOnTower, player.transform))
            {   // If player is at this tower, returns
                return;
            }
            else if (!player.GetComponent<PlayerController>().teleporting)
            {   // If not and player is not teleporting
                player.GetComponent<PlayerController>().teleporting = true;
                StartCoroutine(TeleportEvents());
            }
        }
    }

    bool IsAround (Transform trA, Transform trB) {

		if (Vector3.Magnitude (trA.position - trB.position) < 1.0f)
			return true;
		else
			return false;

	}

	IEnumerator TeleportEvents () {
		
		// Animates
		player.GetComponent<Teleporter>().TeleportFor(playerSpawnOnTower.gameObject);

		yield return new WaitUntil (() => !player.GetComponent<Animator> ().GetBool ("start"));

		// Teleports
		player.transform.position = playerSpawnOnTower.position;
		player.transform.rotation = partToRotate.transform.rotation;

		// Sets new target to player if this isn't already his
		if (target != null && player.GetComponent<PlayerController> ().GetTarget() != null) {
			if (player.GetComponent<PlayerController> ().GetTarget() != target.gameObject) {
				player.GetComponent<PlayerController> ().SetTarget (target.gameObject);
			}
		}

		// Telling player where he is
		player.GetComponent<PlayerController>().currentTower = this.gameObject;

		// Habiliting player's LookAt target position
		player.GetComponent<PlayerController> ().teleporting = false;

		StopCoroutine (TeleportEvents ());
	}

    private bool IsInCorrectScene()
    {
        return (SceneManager.GetActiveScene().buildIndex != 0 && string.Equals(SceneManager.GetActiveScene().name, "MainMenu") == false);
    }

    private void SetRangeObject()
    {
        if (IsInCorrectScene())
        {
			rangeObject.transform.localScale = new Vector3(range * 2, 0.01f, range * 2);
            rangeObject.SetActive(false);
        }
	}

	private void SetValues (float multiplicationFactor) {

		damage = multiplicationFactor * bulletPrefab.GetComponent<SkillsProperties> ().GetDamage ();
		cooldown = multiplicationFactor * bulletPrefab.GetComponent<SkillsProperties> ().GetCooldown ();
		range = multiplicationFactor * bulletPrefab.GetComponent<SkillsProperties> ().GetRange ();
	}
}