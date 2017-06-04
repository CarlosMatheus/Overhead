using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour {

	private GameObject player;

	public GameObject towerSkill;
	public Transform playerSpawnOnTower;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void OnMouseDown () {
		if (IsAround (playerSpawnOnTower, player.transform)) {  // If player is at this tower, returns
			return;
		} else {  // If not
			
			// Teleports
			player.transform.position = playerSpawnOnTower.position;

			// Sets new skill to player
			player.GetComponent<PlayerController>().currentSkill = towerSkill;

		}
	}

	bool IsAround (Transform trA, Transform trB) {
		
		if (Vector3.Magnitude (trA.position - trB.position) < 1.0f)
			return true;
		else
			return false;
		
	}
}
