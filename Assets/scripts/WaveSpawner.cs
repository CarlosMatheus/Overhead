using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

	[Header("Spawn Attributes")]

	public float initialcountdown = 5f;
	public float timeBetweenWaves = 5f;
	public float spawnDelay = 0.5f;

	[Header("Base Enemy Attributes")]

	public float baseSpeed = 1f;
	public float baseHP = 100f;

	[Header("Define The EnemyPrefab Array")]

	public GameObject[] enemyPrefab;

	[Header("Define The Waves Enemies")]

	public string[] ArrayEnemies;

	private int waveNumber = 1;
	private int moduleIndex = 0;
	private int numOfSpawnEnemy;
	private float countdown;
	private Text waveNumberText;
	private Text waveCountdownText;
	private GameObject masterTower;
	private GameObject[] thisWaveSpawnEnemies;
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
		for (int i = 0; i < numOfSpawnEnemy; i++) {    // not wave number  ???
			EnemySpawn (thisWaveSpawnEnemies[i]);
			yield return new WaitForSeconds (spawnDelay);
		}
		waveNumber++;
		UpdateSoul ();
		UpdateLifes ();
	}

	private void AjustArray(){
		///
		int[] auxArr = new int[ArrayEnemies [waveNumber - 1].Length]; 
		/////
		int auxArrIdx = 0;
		int indexVal = 0;
		int mult = 1;
		int actualVal = 0;
		///
		for ( int i = (ArrayEnemies[waveNumber - 1].Length - 1) ; i >= 0 ; i -- ){
			actualVal = ArrayEnemies[waveNumber - 1][i];
			if(actualVal == ','){
				auxArr[auxArrIdx] = indexVal;
				auxArrIdx++;
				mult = 1;
				indexVal = 0;
				continue;
			}
			else {
				indexVal += actualVal * mult;
				mult = mult * 10;
			}
		}
		auxArr[auxArrIdx] = indexVal;
		///
		thisWaveSpawnEnemies = new GameObject[auxArrIdx + 1];
		/// 
		int j = 0; 
		for (int i = indexVal; i >= 0; i--) {
			thisWaveSpawnEnemies[j] = enemyPrefab[ auxArr[i] ];
			j++;
		}
	}

	private void AjustDifficulty(){

	}

	private void StringToArray(){
		
	}

	private void Awake(){
		moduleIndex = 0;
		spawnPoint = new Transform[4];
		wayPoints = new WayPointsScript[4];
	}

	private void Start(){
		masterTower = GameObject.Find ("MasterTower");
		countdown = initialcountdown;
		soulsConter = this.GetComponent<SoulsCounter> ();
		masterTowerScript = masterTower.GetComponent<MasterTowerScript> ();
		waveNumberText = GameObject.Find ("wave").GetComponent<Text>();
		waveCountdownText = GameObject.Find ("waveCountdownText").GetComponent<Text>();
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

		//teste:
//		string[] coisa = new string[2];
//		coisa[0] = "abcdef";
//		coisa[1] = "ghijk";
//
//		Debug.Log (coisa [0][0]);
//		Debug.Log (coisa [1][1]);
//		Debug.Log (coisa [1][4]);

	}

	//Instantiate the Enemy and set the waypoints
	private void EnemySpawn(GameObject enemyPrefab){
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