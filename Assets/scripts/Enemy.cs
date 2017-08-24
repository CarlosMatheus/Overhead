using UnityEngine;
using UnityEngine.SceneManagement;

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
    private GameObject gameMaster;
	private WaveSpawner waveSpawner;
	private WayPointsScript wayPoints;
	private MasterTowerScript masterTowerScript;
    private MouseCursorManager mouserCursorManager;

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
		speed = waveSpawner.GetBaseSpeed () * speedEnemyConst;
	}

	private void Awake(){
		waveSpawner = GameObject.Find ("GameMaster").GetComponent<WaveSpawner> ();
		hp = waveSpawner.GetBaseHP() * hpEnemyConst;
	}

	private void Start()
    {
        gameMaster = GameObject.FindWithTag("GameMaster");
        masterTower = gameMaster.GetComponent<InstancesManager>().GetMasterTowerObj();
		masterTowerScript = masterTower.GetComponent<MasterTowerScript> ();
        mouserCursorManager = gameMaster.GetComponent<MouseCursorManager>();
		SetSpeed (waveSpawner.GetBaseSpeed() * speedEnemyConst);
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
	private void EnemyAttack()
    {
        if (IsInCorrectScene())
        {
            masterTowerScript.EnemyAttack();
            Destroy(gameObject);
        }
	}

    private void OnMouseEnter()
    {
        mouserCursorManager.SetRedCursor();
    }

    private void OnMouseExit()
    {
        mouserCursorManager.SetIdleCursor();
    }

    private bool IsInCorrectScene()
    {
        return (SceneManager.GetActiveScene().buildIndex != 0 && string.Equals(SceneManager.GetActiveScene().name, "MainMenu") == false);
    }

}