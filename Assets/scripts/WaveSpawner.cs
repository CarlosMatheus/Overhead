using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

	[Header("Spawn Attributes")]

	public float initialcountdown = 5f;
	public float timeBetweenWaves = 5f;
	public float spawnDelay = 0.5f;
	public int minSpawnEnemy=1;
	public int maxSpawnEnemy=10;

	[Header("Base Enemy Attributes")]

	public float baseSpeed = 1f;
	public float baseHP = 100f;

	[Header("Define The EnemyPrefab Array")]

	public GameObject[] enemyPrefab;

	[Header("Define The Waves Enemies")]

	public int[][] ArrayEnemies;

	private int waveNumber = 1;
	private int moduleIndex = 0;
	private int numOfSpawnEnemy;
	private float countdown;
	public Text waveNumberText;
	public Text waveCountdownText;
	private GameObject masterTower;
	private Transform[] spawnPoint;
	private SoulsCounter soulsConter;
	private WayPointsScript[] wayPoints;
	private MasterTowerScript masterTowerScript;

	public void SetModule(Transform spawnP, WayPointsScript wayP){
		spawnPoint [moduleIndex] = spawnP;
		wayPoints [moduleIndex] = wayP;
		moduleIndex++;
	}

	//Coroutine for the spawn, it delays spawnDelay for each instantiation
	private IEnumerator SpawnWave(){
		AjustArray ();
		AjustDifficulty();
		SpawnFunc();
		UpdateSoul ();
		UpdateLifes ();
	}

	private void SpawnFunc(){
		for (int i = 0; i < waveNumber; i++) {
			EnemySpawn ();
			yield return new WaitForSeconds (spawnDelay);
		}
		numOfSpawnEnemy++;
		waveNumber++;
	}

	private void AjustArray(){
		numOfSpawnEnemy = Mathf.Clamp (numOfSpawnEnemy, minSpawnEnemy, maxSpawnEnemy);

	}

	private void AjustDifficulty(){

	}

	private void Awake(){
		moduleIndex = 0;
		spawnPoint = new Transform[4];
		wayPoints = new WayPointsScript[4];
	}

	private void Start(){
		numOfSpawnEnemy = 1;
		masterTower = GameObject.Find ("MasterTower");
		countdown = initialcountdown;
		soulsConter = this.GetComponent<SoulsCounter> ();
		masterTowerScript = masterTower.GetComponent<MasterTowerScript> ();
	}

	/// <summary>
	/// Update this instance.
	/// </summary>
	private void Update () {
		if ( countdown <= 0f ) {
			StartCoroutine (SpawnWave ());
			countdown = timeBetweenWaves;
		}
		countdown -= Time.deltaTime;
		UpdateUI();
	}

	//Instantiate the Enemy and set the waypoints
	private void EnemySpawn(){
		for (int i = 0; i < 4; i++) {
			GameObject enemyGameObj = (GameObject)Instantiate (enemyPrefab, spawnPoint [i].position, spawnPoint [i].rotation);
			enemyGameObj.GetComponent<Enemy> ().SetWayPoints (wayPoints [i]);
		}
	}

	//Update the User Interface with wave and time remain for next wave information
	private void UpdateUI(){
		waveCountdownText.text = Mathf.Round(countdown).ToString();
		waveNumberText.text = Mathf.Round (waveNumber - 1 ).ToString ();
	}

	/// <summary>
	/// Updates the soul.
	/// </summary>
	private void UpdateSoul(){
		soulsConter.SetWave (waveNumber - 1);
	}

	/// <summary>
	/// Updates the lifes.
	/// </summary>
	private void UpdateLifes(){
		masterTowerScript.NewWave ();
	}
}