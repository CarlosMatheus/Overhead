using UnityEngine;

public class Enemy : MonoBehaviour {

	public float speed =10f;
	public float minDist = 0.2f;

	private Transform target;
	private int wavepointIndex = 0;

	void Start(){
		target = WayPoints.points [0];
	}
	void Update (){
		Vector3 dir = target.position - transform.position;
		transform.Translate (dir.normalized * speed * Time.deltaTime);
		if (Vector3.Distance (transform.position, target.position) <= minDist) {
			GetNextWayPoint ();
		}
	}
	void GetNextWayPoint(){
		if (wavepointIndex >= WayPoints.points.Length - 1) {
			EnemyAttack ();
		} else {
			wavepointIndex++;
			target = WayPoints.points [wavepointIndex];
		}
	}
	void EnemyAttack(){
		Destroy (gameObject);
	}
}
