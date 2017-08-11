using UnityEngine;
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

	private Material originalMaterial;
	private GameObject tower;
	private BuildManager buildManager;
	private SoulsCounter soulsCounter;
	private ScoreCounter scoreCounter;
	private GameObject currentBuildingTower;
	private SphereShop sphereShop;
	private GameObject towerToBuild;
	private DeathManager deathManager;

	void Start(){
		deathManager = GameObject.Find ("GameMaster").GetComponent<DeathManager> ();
		buildManager = GameObject.Find ("GameMaster").GetComponent<BuildManager> ();
		sphereShop = GameObject.Find ("Icosphere").GetComponent<SphereShop> ();
		soulsCounter = SoulsCounter.instance;
		scoreCounter = ScoreCounter.instance;
		towerToBuild = null;
	}
		
	void OnMouseEnter (){
        if (SceneManager.GetActiveScene().buildIndex != 0)
            if (!deathManager.IsDead ()) {
			    if (EventSystem.current.IsPointerOverGameObject ()) {
				    return;
			    }
			    //if the towerToBuild variable is null dont do anything 
			    if (buildManager.GetTowerToBuild () == null) {
				    return;
			    }
			    GameObject selecTowerInst = (GameObject)Instantiate (buildManager.GetSelectionTowerToBuild (), transform.position, transform.rotation);
			    selecTowerInst.transform.rotation = Quaternion.Euler (0, 0, 0);
			    buildManager.SetSelectionTowerToBuildInstance (selecTowerInst);
		    }
	}

	void OnMouseExit (){
        if (SceneManager.GetActiveScene().buildIndex != 0)
            buildManager.DestroySelectionTowerToBuildInstance ();
	}

	void OnMouseDown(){
		if (!deathManager.IsDead ()) {
			//Avoid pointing to something with a UI element in front of it
			if (EventSystem.current.IsPointerOverGameObject ()) {
				return;
			}
			//if the towerToBuild variable is null dont do anything 
			if (buildManager.GetTowerToBuild () == null) {
				return;
			}

			if (tower != null) {
				Debug.Log ("can't build there! - TODO: Display on screen.");
				return;
			}
			buildManager.DestroySelectionTowerToBuildInstance ();
			currentBuildingTower = buildManager.GetTowerToBuild ();
			soulsCounter.BuildTower (buildManager.GetTowerToBuildIndex ());
			scoreCounter.BuildTower (buildManager.GetTowerToBuildIndex ());
			GameObject[] treeArr;
			treeArr = GameObject.FindGameObjectsWithTag ("Tree");
			for(int i = 0; i < treeArr.Length;i ++ ){
				if (Vector3.Distance (treeArr [i].transform.position, transform.position) < towerDist + 1.5f) {
					Destroy (treeArr[i]);
					GameObject eff = (GameObject)Instantiate (destroyEffect, treeArr [i].transform.position, treeArr [i].transform.rotation);
					Destroy (eff,2f);
				}
			}
			//StartCoroutine (EventInstantiator ());
			BuildTower ();
		}
	}

	void BuildTower () {
		//StopCoroutine (EventInstantiator ());
		towerToBuild = buildManager.GetTowerToBuild ();
		tower = (GameObject)Instantiate (currentBuildingTower, transform.position, transform.rotation);
		tower.transform.rotation = Quaternion.Euler (0,0,0);
	}

	/// <summary>
	/// Tells the sphere shop tower to build.
	/// </summary>
	private void TellSphereShopTowerToBuild(){
		sphereShop.SetTowerToBuild (towerToBuild);
	}
}
