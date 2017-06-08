using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

	[Header("Unity Setup Filds")]

	public GameObject enemyPrefab;
	public Text waveNumberText;
	public Text waveCountdownText;

	[Header("Attributes")]

	public float Initialcountdown = 5f;
	public float timeBetweenWaves = 5f;
	public float spawnDelay = 0.5f;

	private float countdown;
	private int waveNumber = 1;
	private SoulsCounter soulsConter;
	private GameObject masterTower;
	private MasterTowerScript masterTowerScript;
	private Transform[] spawnPoint;
	private WayPointsScript[] wayPoints;
	private int moduleIndex = 0;

	public void SetModule(Transform spawnP, WayPointsScript wayP){
		spawnPoint [moduleIndex] = spawnP;
		wayPoints [moduleIndex] = wayP;
		moduleIndex++;
	}

	//Coroutine for the spawn, it delays spawnDelay for each instantiation
	private IEnumerator SpawnWave(){
		for (int i = 0; i < waveNumber; i++) {
			EnemySpawn ();
			yield return new WaitForSeconds (spawnDelay);
		}
		waveNumber++;
		UpdateSoul ();
		UpdateLifes ();
	}

	private void Awake(){
		moduleIndex = 0;
		spawnPoint = new Transform[4];
		wayPoints = new WayPointsScript[4];
	}

	private void Start(){
		masterTower = GameObject.Find ("MasterTower");
		countdown = Initialcountdown;
		soulsConter = this.GetComponent<SoulsCounter> ();
		masterTowerScript = masterTower.GetComponent<MasterTowerScript> ();
	}

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

	private void UpdateSoul(){
		soulsConter.SetWave (waveNumber - 1);
	}

	private void UpdateLifes(){
		masterTowerScript.NewWave ();
	}
}