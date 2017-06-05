using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {

	public GameObject transpTower;
	public GameObject buildEffect;

	private Material originalMaterial;
	private GameObject tower;
	private BuildManager buildManager;
	private GameObject transpTowerInst;

	void Start(){
		buildManager = BuildManager.instance;
	}
		
	void OnMouseEnter (){
		if (EventSystem.current.IsPointerOverGameObject ()) {
			return;
		}
		//if the towerToBuild variable is null dont do anything 
		if (buildManager.GetTowerToBuild () == null) {
			return;
		}
		transpTowerInst = (GameObject) Instantiate (transpTower, transform.position, transform.rotation);
		transpTowerInst.transform.rotation = Quaternion.Euler (0,0,0);
	}

	void OnMouseExit (){
		Destroy (transpTowerInst);
	}

	void OnMouseDown(){
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
		Destroy (transpTowerInst);

		StartCoroutine (EventInstantiator ());
	}

	IEnumerator EventInstantiator () {

		GameObject tempBuildEffect = Instantiate (buildEffect, transform.position, Quaternion.identity);

		yield return new WaitUntil (() => tempBuildEffect.GetComponent<Animator> ().GetBool ("finished"));

		BuildTower ();
	}

	void BuildTower () {
		StopCoroutine (EventInstantiator ());
		GameObject towerToBuild = BuildManager.instance.GetTowerToBuild ();
		tower = (GameObject)Instantiate (towerToBuild, transform.position, transform.rotation);
		tower.transform.rotation = Quaternion.Euler (0,0,0);
	}
}
