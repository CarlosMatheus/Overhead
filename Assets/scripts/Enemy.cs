using UnityEngine;

public class Enemy : MonoBehaviour {

	[Header("This Enemy Prefab constants")]

	public float speedEnemyConst = 1f;
	public float hpEnemyConst = 1f;
	public float minDistToChangeDirection = 0.2f;

	private int wavepointIndex = 0;
	private float hp;
	private float speed;
	private float originalHeight;

	private Transform target;
	private GameObject masterTower;
	private WaveSpawner waveSpawner;
	private WayPointsScript wayPoints;
	private MasterTowerScript masterTowerScript;

	//WaveSpawner will use this to set the waypoint
	public void SetWayPoints(WayPointsScript wayP){
		wayPoints = wayP;
	}

	/// <summary> Gets the HP </summary>
	/// This is accessed by the targetSelection
	/// <returns> The HP </returns>
	public float getHP() {
		return hp;
	}

	public void SetSpeed ( float _speed ) {
		speed = _speed;
	}

	public float GetSpeed () {
		return speed;
	}

	public void ReturnToOriginalSpeed () {
		speed = waveSpawner.getBaseSpeed () * speedEnemyConst;
	}

	private void Awake(){
		waveSpawner = GameObject.Find ("GameMaster").GetComponent<WaveSpawner> ();
		hp = waveSpawner.getBaseHP() * hpEnemyConst;
	}

	private void Start(){
		masterTower = GameObject.Find("MasterTower");
		masterTowerScript = masterTower.GetComponent<MasterTowerScript> ();
		SetSpeed (waveSpawner.getBaseSpeed() * speedEnemyConst);
		target = wayPoints.GetPoints (0);
		originalHeight = transform.position.y;
	}

	private void Update (){
		Vector3 dir = target.position - transform.position;
		transform.Translate (dir.normalized * GetSpeed() * Time.deltaTime, Space.World);
		transform.LookAt (target.position);
		if (Vector3.Distance (transform.position, target.position) <= minDistToChangeDirection) {
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