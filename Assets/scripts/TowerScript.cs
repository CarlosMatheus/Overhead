using System.Collections;
using UnityEngine;

public class TowerScript : MonoBehaviour {

	public Transform target;
	public Transform witch;
	public Transform rangePiece;
	public Transform partToRotate;

	public float range = 3f;
	public string enemyTag = "Enemy";
	public float turnSpeed = 10f;

	void Start () {
		//This will reapeat every 0.5 sec
		InvokeRepeating ("UpdateTarget", 0f, 0.5f);
	}

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

	void Update () {
		//do nothing in case there is no target
		if (target == null)
			return;

		//rotation to follow the enemy direction
		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
		partToRotate.rotation = Quaternion.Euler (0f, rotation.y, 0f);
	}
	void OnDrawGizmosSelected(){
		Gizmos.color = new Color(255,0,0,0.75f);
		Gizmos.DrawWireSphere (rangePiece.position, range);
	}
}
