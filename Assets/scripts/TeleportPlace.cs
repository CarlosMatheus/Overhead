using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlace : MonoBehaviour {

	private GameObject player;
	private Transform partToRotate;
	private Transform playerSpawnOnTower;

	void Start () {
		// Finding the player gameObject
		player = GameObject.FindGameObjectWithTag ("Player");
		if (GetComponent<TowerScript> () == null)
			return;
		partToRotate = GetComponent<TowerScript> ().partToRotate;
		playerSpawnOnTower = GetComponent<TowerScript> ().playerSpawnOnTower;
	}

	private void OnMouseOver()
	{
		if (Input.GetMouseButtonDown(1))
		{
			if (IsAround(playerSpawnOnTower, player.transform))
			{   // If player is at this tower, returns
				return;
			}
			else if (!player.GetComponent<PlayerController>().teleporting)
			{   // If not and player is not teleporting
				player.GetComponent<PlayerController>().teleporting = true;
				StartCoroutine(TeleportEvents());
			}
		}
	}

	bool IsAround (Transform trA, Transform trB) {

		if (Vector3.Magnitude (trA.position - trB.position) < 1.0f)
			return true;
		else
			return false;

	}

	IEnumerator TeleportEvents () {

		// Animates
		player.GetComponent<Teleporter>().TeleportFor(playerSpawnOnTower.gameObject);

		yield return new WaitUntil (() => !player.GetComponent<Animator> ().GetBool ("start"));

		// Teleports
		player.transform.position = playerSpawnOnTower.position;
		player.transform.rotation = partToRotate.transform.rotation;

		// Sets new target to player if this isn't already his
		GameObject target = GetComponent<TowerScript>().GetTarget();
		if (target != null && player.GetComponent<PlayerController> ().GetTarget() != null) {
			if (player.GetComponent<PlayerController> ().GetTarget() != target) {
				player.GetComponent<PlayerController> ().SetTarget (target);
			}
		}

		// Telling player where he is
		player.GetComponent<PlayerController>().currentTower = this.gameObject;

		// Habiliting player's LookAt target position
		player.GetComponent<PlayerController> ().teleporting = false;

		StopCoroutine (TeleportEvents ());
	}
}
