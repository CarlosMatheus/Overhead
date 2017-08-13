﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A general class that control the build system
public class BuildManager : MonoBehaviour {

	public GameObject[] tower;
	public GameObject[] selectionTower;
	public float[] initialTowerValue;
	public float[] initialTowerScore;

	private int towerToBuildIndex;
	private GameObject towerToBuild;
	private GameObject selectionTowerToBuild;
	private GameObject selectionTowerToBuildInstance;
	private SoulsCounter soulsCounter;

	/// <summary>
	/// Gets the selection tower to build.
	/// This is used by the Node
	/// </summary>
	/// <returns>The selection tower to build.</returns>
	public GameObject GetSelectionTowerToBuild()
    {
		return selectionTowerToBuild;
	}

	/// <summary>
	/// Sets the selection tower to build.
	/// This is used by the Shop
	/// </summary>
	/// <param name="selecTower">Selec tower.</param>
	public void SetSelectionTowerToBuild (GameObject selecTower)
    {
		selectionTowerToBuild = selecTower;
	}

	/// <summary>
	/// Destroies the selection tower.
	/// This is used by the SphereShop
	/// </summary>
	public void DestroySelectionTowerToBuildInstance()
    {
		Destroy(selectionTowerToBuildInstance);
	}

	/// <summary>
	/// Sets the selection tower to build instance.
	/// this is used by the node
	/// </summary>
	/// <param name="selectionT">Selection t.</param>
	public void SetSelectionTowerToBuildInstance(GameObject selectionT)
    {
		selectionTowerToBuildInstance = selectionT;
	}

	/// <summary>
	/// Gets the tower to build.
	/// This is accessed by the node, by the SphereShop
	/// </summary>
	/// <returns>The tower to build.</returns>
	public GameObject GetTowerToBuild()
    {
		return towerToBuild;
	}
		
	/// <summary>
	/// Gets the index of the tower to build.
	/// This is accessed by the SphereShop
	/// </summary>
	/// <returns>The tower to build index.</returns>
	public int GetTowerToBuildIndex()
    {
		return towerToBuildIndex;
	}

	/// <summary>
	/// Sets the tower to build.
	/// This is set by the Shop when the user click on 
	/// the shops button
	/// </summary>
	/// <param name="tower">Tower.</param>
	public void SetTowerToBuild (GameObject tower)
    {
		towerToBuild = tower;
	}

	/// <summary>
	/// Sets the index of the tower to build.
	/// This is set by the Shop when the user click on 
	/// the shops button
	/// </summary>
	/// <param name="index">Index.</param>
	public void SetTowerToBuildIndex (int index)
    {
		towerToBuildIndex = index;
	}

	private void Awake()
    {
		towerToBuildIndex = 0;
		towerToBuild = null;
		soulsCounter = gameObject.GetComponent<SoulsCounter> ();
	}

	private void Start()
    {
		soulsCounter.SetInitialTowersValues (initialTowerValue);
	}
}	