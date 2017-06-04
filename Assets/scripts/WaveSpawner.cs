using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

	public Transform enemyPrefab;
	public Transform spawnPoint;
	public Text waveNumberText;
	public Text waveCountdownText;
	public float timeBetweenWaves = 5f;
	public float spawnDelay = 0.5f;

	private float countdown = 5f;
	private int waveNumber = 1;

	void Update () {
		if ( countdown <= 0f ) {
			StartCoroutine (SpawnWave ());
			countdown = timeBetweenWaves;
		}
		countdown -= Time.deltaTime;
		UpdateUI();
	}

	IEnumerator SpawnWave(){
		Debug.Log("Wave incomming! GoGoBruxao");
		for (int i = 0; i < waveNumber; i++) {
			EnemySpawn ();
			yield return new WaitForSeconds (spawnDelay);
		}
		waveNumber++;
	}

	void EnemySpawn(){
		Instantiate (enemyPrefab,spawnPoint.position,spawnPoint.rotation);
	}

	void UpdateUI(){
		waveCountdownText.text = Mathf.Round(countdown).ToString();
		waveNumberText.text = Mathf.Round (waveNumber - 1 ).ToString ();
	}
}
