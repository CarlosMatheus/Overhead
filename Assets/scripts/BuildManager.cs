using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A general class that control the build system
public class BuildManager : MonoBehaviour {

	public static BuildManager instance;
	public GameObject standardTowerPrefab;
	public GameObject anotherTowerPrefab;

	private GameObject towerToBuild;

	//Return what tower is to build:
	public GameObject GetTowerToBuild(){
		return towerToBuild;
	}

	//This will set what tower to build:
	public void SetTowerToBuild (GameObject tower){
		towerToBuild = tower;
	}

	//Will instantiate this class to be accessed outside
	private void Awake(){
		//To avoid more the one instance of this class:
		if ( instance != null) {
			Debug.LogError ("More then one BuildManager in the scene!");
			return;
		}
		instance = this;
	}
}
	