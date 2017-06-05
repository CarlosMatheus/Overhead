using UnityEngine;
using UnityEngine.UI;

//This class will be used by the UI bottom to buy a new tower
public class Shop : MonoBehaviour {

	private BuildManager buildManager;
	private Image image;

	public void PurcheseStandardTower(){
		//Debug.Log ("Standard Tower Purchased");
		buildManager.SetTowerToBuild (buildManager.standardTowerPrefab);
	}

//	public void PurcheseAnotherTower(){
//		Debug.Log ("Another Tower Purchased");
//		buildManager.SetTowerToBuild (buildManager.anotherTowerPrefab);
//	}

	//At the begginig instatiate the build manager
	private void Start(){
		buildManager = BuildManager.instance;
	}
}
