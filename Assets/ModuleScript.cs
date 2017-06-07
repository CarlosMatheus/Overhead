using System.Collections;
using UnityEngine;

public class ModuleScript : MonoBehaviour {

	private Transform spawnPoint;
	private GameObject WayPoints;

	void Awake () {
		spawnPoint = GetComponentInChildren<SpawnPointScript> ().transform;
		Debug.Log (spawnPoint.position);
	}
}
