using UnityEngine;
using UnityEngine.UI;

//This class will be used by the UI bottom to buy a new tower
public class Shop : MonoBehaviour {

	public GameObject insuficientSouls;

	private SoulsCounter soulsCounter;
	private ScoreCounter scoreCounter;
	private BuildManager buildManager;
	private Image image;


	public void PurcheseStandardTower(){
		if (!soulsCounter.CanBuild ())
			return;
		buildManager.SetTowerToBuild (buildManager.standardTowerPrefab);
	}

//	public void PurcheseAnotherTower(){
//		Debug.Log ("Another Tower Purchased");
//		buildManager.SetTowerToBuild (buildManager.anotherTowerPrefab);
//	}

	//At the begginig instatiate the build manager
	private void Start(){
		buildManager = BuildManager.instance;
		soulsCounter = SoulsCounter.instance;
		scoreCounter = ScoreCounter.instance;
		insuficientSouls.SetActive (false);
	}

	private void Update(){
		if (!soulsCounter.CanBuild ())
			insuficientSouls.SetActive (true);
		else
			insuficientSouls.SetActive (false);
	}



}
