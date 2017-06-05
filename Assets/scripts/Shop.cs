using UnityEngine;

public class Shop : MonoBehaviour {

	BuildManager buildManager;

	public void PurcheseStandardTower(){
		Debug.Log ("Standard Tower Purchased");
		buildManager.SetTowerToBuild (buildManager.standardTowerPrefab);
	}

	public void PurcheseAnotherTower(){
		Debug.Log ("Another Tower Purchased");
		buildManager.SetTowerToBuild (buildManager.anotherTowerPrefab);
	}
	private void Start(){
		buildManager = BuildManager.instance;
	}
}
