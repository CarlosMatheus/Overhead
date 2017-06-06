using UnityEngine;

public class Enemy : MonoBehaviour {

	public float speed =10f;
	public float minDist = 0.2f;

	private WayPoints wayPoints;
	private Transform target;
	private int wavepointIndex = 0;
	private GameObject masterTower;
	private MasterTowerScript masterTowerScript;

	public void SetWayPoints(GameObject wayP){
		wayPoints = wayP.GetComponent<WayPoints>();
	}

	//WayPoints is a public static variable that is been accessed from here
	private void Start(){
		masterTower = GameObject.Find("MasterTower");
		masterTowerScript = masterTower.GetComponent<MasterTowerScript> ();
		target = wayPoints.GetPoints (0);
	}

	private void Update (){
		Vector3 dir = target.position - transform.position;
		transform.Translate (dir.normalized * speed * Time.deltaTime, Space.World);
		if (Vector3.Distance (transform.position, target.position) <= minDist) {
			GetNextWayPoint ();
		}
	}

	//In the WayPoints array, get the next or attack the Main Tower
	private void GetNextWayPoint(){
		if (wavepointIndex >= wayPoints.GetPointsLength() - 1) {
			EnemyAttack ();
		} else {
			wavepointIndex++;
			target = wayPoints.GetPoints (wavepointIndex);
		}
	}

	//Attack the main tower
	private void EnemyAttack(){
		masterTowerScript.EnemyAttack ();
		Destroy (gameObject);
	}
}