using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {

	public GameObject transpTower;

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
		GameObject towerToBuild = BuildManager.instance.GetTowerToBuild ();
		tower = (GameObject)Instantiate (towerToBuild, transform.position, transform.rotation);
		tower.transform.rotation = Quaternion.Euler (0,0,0);
	}
}
