using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeCanvasManager : MonoBehaviour {

	private InstancesManager instanceManager;
    private GameObject upSysRef;
    private GameObject upSys;

    void Start ()
    {
        // Take reference of instance manager to manage different canvas
        instanceManager = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<InstancesManager> ();

        // See if it is an Research Tower
        SearchCenterPlace scp = GetComponent<SearchCenterPlace>();
        if (scp == null)
            // If it's not, get UpgradeCanvas
            upSysRef = instanceManager.GetUpgradeCanvas();
        else
            // Else, get ResearchCanvas from InstancesManager
            upSysRef = instanceManager.GetResearchCanvas();

        // Instantiate the correct canvas object in this tower
        upSys = Instantiate(upSysRef, transform.position, transform.rotation, transform);

        // Check if there is a Canvas on this tower
        Canvas towerCanvas = GetComponentInChildren<Canvas> ();
		if (towerCanvas == null) {
			Debug.Log ("Canvas is null no tower " + gameObject.name);
			return;
		}

        // If there is, deactivate it for now
		upSys.SetActive (false);
	}

	void Update () 
    {
		if (upSys != null)
			CheckMouseButtonDown();
	}

	void OnMouseDown ()
    {
		if (gameObject.name == "MasterTower")
			return;
        //if ( gameObject.GetComponent<TowerScript>().IsPlayerInThisTower() == false )
        //    return;

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
