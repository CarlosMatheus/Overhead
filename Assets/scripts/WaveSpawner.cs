using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

	[Header("Unity Setup Filds")]

	public GameObject enemyPrefab;
	public Transform spawnPoint1;
	public Transform spawnPoint2;
	public Transform spawnPoint3;
	public Transform spawnPoint4;
	public GameObject WayPoints1;
	public GameObject WayPoints2;
	public GameObject WayPoints3;
	public GameObject WayPoints4;
	public Text waveNumberText;
	public Text waveCountdownText;

	[Header("Attributes")]

	public float Initialcountdown = 5f;
	public float timeBetweenWaves = 5f;
	public float spawnDelay = 0.5f;

	private float countdown;
	private int waveNumber = 1;
	private SoulsCounter soulsconter;

	void Start(){
		countdown = Initialcountdown;
		soulsconter = this.GetComponent<SoulsCounter> ();
	}

	void Update () {
		if ( countdown <= 0f ) {
			StartCoroutine (SpawnWave ());
			countdown = timeBetweenWaves;
		}
		countdown -= Time.deltaTime;
		UpdateUI();
		UpdateSoul ();
	}

	//Coroutine for the spawn, it delays spawnDelay for each instantiation
	IEnumerator SpawnWave(){
		for (int i = 0; i < waveNumber; i++) {
			EnemySpawn ();
			yield return new WaitForSeconds (spawnDelay);
		}
		waveNumber++;
	}

	//instantiate the Enemy
	void EnemySpawn(){
		GameObject instance1 = ( GameObject ) Instantiate (enemyPrefab,spawnPoint1.position,spawnPoint1.rotation);
		GameObject instance2 = ( GameObject ) Instantiate (enemyPrefab,spawnPoint2.position,spawnPoint2.rotation);
		GameObject instance3 = ( GameObject ) Instantiate (enemyPrefab,spawnPoint3.position,spawnPoint3.rotation);
		GameObject instance4 = ( GameObject ) Instantiate (enemyPrefab,spawnPoint4.position,spawnPoint4.rotation);

		instance1.GetComponent<Enemy> ().SetWayPoints(WayPoints1);
		instance2.GetComponent<Enemy> ().SetWayPoints(WayPoints2);
		instance3.GetComponent<Enemy> ().SetWayPoints(WayPoints3);
		instance4.GetComponent<Enemy> ().SetWayPoints(WayPoints4);
	}

	//Update the User Interface with wave and time remain for next wave information
	void UpdateUI(){
		waveCountdownText.text = Mathf.Round(countdown).ToString();
		waveNumberText.text = Mathf.Round (waveNumber - 1 ).ToString ();
	}

	void UpdateSoul(){
		soulsconter.SetWave (waveNumber - 1);
	}
}