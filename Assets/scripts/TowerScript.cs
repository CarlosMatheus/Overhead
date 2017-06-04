using System.Collections;
using UnityEngine;

public class TowerScript : MonoBehaviour {

	private Transform target;

	[Header("Unity Setup Fields")]

	public Transform witch;
	public Transform rangePiece;
	public Transform partToRotate;
	public GameObject bulletPrefab;
	public Transform firePoint;
	public string enemyTag = "Enemy";

	[Header("Attributes")]

	public float range = 4f;
	public float turnSpeed = 10f;
	public float fireRate = 1f;
	public float fireCountdown = 0f;

	void Start () {
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
	void UpdateTarget(){
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

	void Fire(){
		if( fireCountdown <= 0f ){
			Shoot ();
			fireCountdown = 1f / fireRate;
		}
		fireCountdown -= Time.deltaTime;
	}

	void Shoot(){
		Debug.Log ("shoot");
	}

	void OnDrawGizmosSelected(){
		Gizmos.color = new Color(255,0,0,0.75f);
		Gizmos.DrawWireSphere (rangePiece.position, range);
	}
}