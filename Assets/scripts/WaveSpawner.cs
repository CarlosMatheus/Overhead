using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

	private float countdown;
	private int waveNumber = 1;

	[Header("Unity Setup Filds")]

	public Transform enemyPrefab;
	public Transform spawnPoint;
	public Text waveNumberText;
	public Text waveCountdownText;

	[Header("Attributes")]

	public float Initialcountdown = 5f;
	public float timeBetweenWaves = 5f;
	public float spawnDelay = 0.5f;

	void Start(){
		countdown = Initialcountdown;
	}

	void Update () {
		if ( countdown <= 0f ) {
			StartCoroutine (SpawnWave ());
			countdown = timeBetweenWaves;
		}
		countdown -= Time.deltaTime;
		UpdateUI();
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
		Instantiate (enemyPrefab,spawnPoint.position,spawnPoint.rotation);
	}

	//Update the User Interface with wave and time remain for next wave information
	void UpdateUI(){
		waveCountdownText.text = Mathf.Round(countdown).ToString();
		waveNumberText.text = Mathf.Round (waveNumber - 1 ).ToString ();
	}
}
