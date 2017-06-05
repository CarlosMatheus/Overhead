using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {

	public static BuildManager instance;
	public GameObject standardTowerPrefab;

	private GameObject towerToBuild;

	public GameObject GetTowerToBuild(){
		return towerToBuild;
	}

	private void Awake(){
		if ( instance != null) {
			Debug.LogError ("More then one BuildManager in the scene!");
			return;
		}
		instance = this;
	}

	private void Start(){
		towerToBuild = standardTowerPrefab;
	}

}

