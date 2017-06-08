using System.Collections;
using UnityEngine;

public class ModuleScript : MonoBehaviour {

	private Transform spawnPoint;
	private WayPointsScript wayPoints;
	private WaveSpawner waveSpawner;

	private void Awake () {
		spawnPoint = GetComponentInChildren<SpawnPointScript> ().transform;
		wayPoints = GetComponentInChildren<WayPointsScript> ();
	}

	private void Start(){
		waveSpawner = GameObject.Find ("GameMaster").GetComponent<WaveSpawner> ();
		waveSpawner.SetModule (spawnPoint,wayPoints);
	}
}