using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeCanvasManager : MonoBehaviour {

	private GameObject upSys;
	private InstancesManager instanceManager;

	void Start () {

		instanceManager = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<InstancesManager> ();

		// Upgrade system canvas configuration
		Canvas towerCanvas = GetComponentInChildren<Canvas> ();
		if (towerCanvas == null) {
			//Debug.Log ("Canvas is null no towe" + gameObject.name);
			return;
		}
		upSys = towerCanvas.gameObject;
		upSys.SetActive (false);
	}

	void Update () {
		if (upSys != null)
			CheckMouseButtonDown();
	}

	void OnMouseDown () {

		if (gameObject.name == "MasterTower")
			return;

		instanceManager.SetTowerOfTheTime (this);

		upSys.transform.rotation = Camera.main.transform.rotation;
	}

	private void CheckMouseButtonDown()
	{
		// Mouse right click makes the store close
		if (Input.GetMouseButtonDown(1) && upSys.activeInHierarchy)
			instanceManager.SetTowerOfTheTime (this);
	}

	public GameObject GetUpCanvas () {
		return upSys;
	}
}
