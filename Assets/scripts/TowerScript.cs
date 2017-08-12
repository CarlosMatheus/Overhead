using System.Collections;
using UnityEngine;

public class TowerScript : MonoBehaviour {

	[Header("Unity Setup Fields")]

	public Transform rangePiece;
	public Transform partToRotate;
	public Transform firePoint;
	public Transform playerSpawnOnTower;
	public GameObject bulletPrefab;
	public GameObject towerSkill;
	public GameObject teleportEffect;
	public string enemyTag = "Enemy";
    [SerializeField] private GameObject rangeObject;

	[Header("Attributes")]

	public float range = 4f;
	public float turnSpeed = 10f;
	public float fireRate = 1f;
	public float fireCountdown = 0f;

	private Transform target;
	private GameObject player;

	public GameObject GetTarget() {
		if (target != null)
			return target.gameObject;
		else
			return null;
	}

    public float GetRange()
    {
        return range;
    }

    public void AppearRange()
    {
        rangeObject.SetActive(true);
    }

    public void DisappearRange()
    {
        rangeObject.SetActive(false);
    }

	void Start () {
		// Finding the player gameObject
		player = GameObject.FindGameObjectWithTag ("Player");
        SetRangeObject();

		if (player == null)
			Debug.Log ("It's goind bad");

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
		if (nearestEnemy != null && shortestDistance <= range) {
			
			target = nearestEnemy.transform;

			if (IsAround (playerSpawnOnTower, player.transform)) {
				if 
				(
					player.GetComponent<PlayerController> ().currentTarget == null ||
				    !player.GetComponent<PlayerController> ().IsInRange
					(
					    player.GetComponent<PlayerController> ().currentTarget.transform, 
					    player.transform
				    )
				) {

					player.GetComponent<PlayerController> ().SetTarget (nearestEnemy);   // Redefine player target

				}
			}

		} else {
			
			target = null;

		}
	}

	//rotation to follow the enemy direction
	void FollowRotation(){
		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
		partToRotate.rotation = Quaternion.Euler (0f, rotation.y, 0f);
	}

	//Will make it fire with the right rate
	void Fire(){
		if( fireCountdown <= 0f ){
			Shoot ();
			fireCountdown = 1f / fireRate;
		}
		fireCountdown -= Time.deltaTime;
	}

	//Will instantiete the shot and make it fallow the target
	void Shoot(){
		GameObject spellGO = ( GameObject ) Instantiate (bulletPrefab,firePoint.position,firePoint.rotation);
		TowerSpell towerSpell = spellGO.GetComponent<TowerSpell>();

		// Speel need to know who instantiated him
		spellGO.GetComponent<SkillsProperties> ().invoker = gameObject;

		if (towerSpell != null)
			towerSpell.Seek (target);
	}

	void OnDrawGizmosSelected(){
		Gizmos.color = new Color(255,0,0,0.75f);
		Gizmos.DrawWireSphere (rangePiece.position, range);
	}

	void OnMouseDown () {
		if (IsAround (playerSpawnOnTower, player.transform)) {  // If player is at this tower, returns
			return;
		} else if (!player.GetComponent<PlayerController> ().teleporting) {  // If not and player is not teleporting
			player.GetComponent<PlayerController> ().teleporting = true;
			StartCoroutine (TeleportEvents ());
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

		// Sets new skill to player
		player.GetComponent<PlayerController>().currentSkill = towerSkill;

		// Sets new target to player if this isn't already his
		if (target != null && player.GetComponent<PlayerController> ().currentTarget != null) {
			if (player.GetComponent<PlayerController> ().currentTarget != target.gameObject) {
				player.GetComponent<PlayerController> ().SetTarget (target.gameObject);
			}
		}

		// Telling player where he is
		player.GetComponent<PlayerController>().currentTower = this.gameObject;

		// Habiliting player's LookAt target position
		player.GetComponent<PlayerController> ().teleporting = false;

		StopCoroutine (TeleportEvents ());
	}

    private void SetRangeObject()
    {
        rangeObject.transform.localScale = new Vector3(range * 2, 0.01f, range * 2);
        rangeObject.SetActive(false);
    }

}