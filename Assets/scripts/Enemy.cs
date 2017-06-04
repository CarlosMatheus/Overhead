using UnityEngine;

public class Enemy : MonoBehaviour {

	private Transform target;
	private int wavepointIndex = 0;

	public float speed =10f;
	public float minDist = 0.2f;

	//WayPoints is a public static variable that is been accessed from here
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

	//In the WayPoints array, get the next or attack the Main Tower
	void GetNextWayPoint(){
		if (wavepointIndex >= WayPoints.points.Length - 1) {
			EnemyAttack ();
		} else {
			wavepointIndex++;
			target = WayPoints.points [wavepointIndex];
		}
	}

	//Attack the main tower
	void EnemyAttack(){
		Destroy (gameObject);
	}
}