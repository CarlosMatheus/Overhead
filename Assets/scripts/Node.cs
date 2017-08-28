﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Node : MonoBehaviour {

    public GameObject buildEffect;
	public GameObject destroyEffect;
	public GameObject deathNode;
	public float towerDist = 2.2f;
    //public GameObject[] buildEffect;
    //public GameObject[] buildSample;

    //private Material originalMaterial;
    private DeathManager deathManager;
    private TowerManager towerManager;
    private BuildManager buildManager;
	private SoulsCounter soulsCounter;
	private ScoreCounter scoreCounter;
	private GameObject currentBuildingTower;
    private MouseCursorManager mouseCursorManager;
    private GameObject towerToBuild;
    private GameObject gameMaster;
    private GameObject tower;
    private SphereShop sphereShop;
    private Shop shopScript;

    private bool isAlreadBuilt;
    private int towerTobuildIdx;

    void Start()
    {
        if (IsInCorrectScene())
        {
            gameMaster = GameObject.FindGameObjectWithTag("GameMaster");
            sphereShop = GameObject.FindGameObjectWithTag("Icosphere").GetComponent<SphereShop>();
            mouseCursorManager = gameMaster.GetComponent<MouseCursorManager>();
            deathManager = gameMaster.GetComponent<DeathManager>();
            buildManager = gameMaster.GetComponent<BuildManager>();
            towerManager = gameMaster.GetComponent<TowerManager>();
            shopScript = gameMaster.GetComponent<InstancesManager>().GetShopGObj().GetComponent<Shop>();
            soulsCounter = SoulsCounter.instance;
            scoreCounter = ScoreCounter.instance;
            towerToBuild = null;
            isAlreadBuilt = false;
        }
	}

    private bool IsInCorrectScene()
    {
        return (SceneManager.GetActiveScene().buildIndex != 0 && string.Equals(SceneManager.GetActiveScene().name, "MainMenu") == false);
    }

    public void SetTowerToBuildIdx(int idx)
    {
        towerTobuildIdx = idx;
    }
		
	void OnMouseEnter ()
    {
        if (IsInCorrectScene())
        {
            if (!deathManager.IsDead())
            {
                if (isAlreadBuilt == true)
                {
                    return;
                }
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return;
                }
                //if the towerToBuild variable is null dont do anything 
                if (buildManager.GetTowerToBuild() == null)
                {
                    return;
                }
                mouseCursorManager.SetInvisibleCursor();
                GameObject selecTowerInst = (GameObject)Instantiate(buildManager.GetSelectionTowerToBuild(), transform.position, transform.rotation);
                selecTowerInst.transform.rotation = Quaternion.Euler(0, 0, 0);
                buildManager.SetSelectionTowerToBuildInstance(selecTowerInst);
            }
        }
	}

	void OnMouseExit ()
    {
        if (IsInCorrectScene())
        {
            mouseCursorManager.SetIdleCursor();
            buildManager.DestroySelectionTowerToBuildInstance();
        }
	}

	void OnMouseDown()
    {
        if (IsInCorrectScene())
        {
            if (!deathManager.IsDead())
            {
                //Avoid pointing to something with a UI element in front of it
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return;
                }
                //if the towerToBuild variable is null dont do anything 
                if (buildManager.GetTowerToBuild() == null)
                {
                    return;
                }

                if (tower != null)
                {
                    Debug.Log("can't build there! - TODO: Display on screen.");
                    return;
                }

                if (isAlreadBuilt == true)
                {
                    return;
                }

                buildManager.DestroySelectionTowerToBuildInstance();
                currentBuildingTower = buildManager.GetTowerToBuild();
                soulsCounter.BuildTower(buildManager.GetTowerToBuildIndex());
                scoreCounter.BuildTower(buildManager.GetTowerToBuildIndex());
                GameObject[] treeArr;
                treeArr = GameObject.FindGameObjectsWithTag("Tree");
                for (int i = 0; i < treeArr.Length; i++)
                {
                    if (Vector3.Distance(treeArr[i].transform.position, transform.position) < towerDist + 1.5f)
                    {
                        Destroy(treeArr[i]);
                        GameObject eff = (GameObject)Instantiate(destroyEffect, treeArr[i].transform.position, treeArr[i].transform.rotation);
                        Destroy(eff, 2f);
                    }
                }
                //StartCoroutine (EventInstantiator ());

                mouseCursorManager.SetIdleCursor();

                BuildTower();

            }
        }
	}

	void BuildTower ()
    {
		//StopCoroutine (EventInstantiator ());
		towerToBuild = buildManager.GetTowerToBuild ();
		tower = (GameObject)Instantiate (currentBuildingTower, transform.position, transform.rotation);
		tower.transform.rotation = Quaternion.Euler (0,0,0);
        towerManager.AddTower(tower, shopScript.GetTowerToBuildIndex());
        buildManager.SetTowerToBuild(null);
        buildManager.SetSelectionTowerToBuild(null);
        towerManager.TowerDiselected();
        isAlreadBuilt = true;
	}

	/// <summary>
	/// Tells the sphere shop tower to build.
	/// </summary>
	private void TellSphereShopTowerToBuild()
    {
		sphereShop.SetTowerToBuild (towerToBuild);
	}
}
