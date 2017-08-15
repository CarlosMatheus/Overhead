using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModuleScript : MonoBehaviour {

	private Transform spawnPoint;
	private WayPointsScript wayPoints;
	private WaveSpawner waveSpawner;

	private void Awake () {
		spawnPoint = GetComponentInChildren<SpawnPointScript> ().transform;
		wayPoints = GetComponentInChildren<WayPointsScript> ();
	}

	private void Start(){
        if (IsNotInCutScene())
        {
            waveSpawner = GameObject.Find("GameMaster").GetComponent<WaveSpawner>();
            waveSpawner.SetModule(spawnPoint, wayPoints);
        }
	}

    private bool IsNotInCutScene()
    {
        bool c = string.Equals(SceneManager.GetActiveScene().name, "CutScenes") == false;
        return (c);
    }
}