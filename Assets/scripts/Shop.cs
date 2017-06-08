using UnityEngine;
using UnityEngine.UI;

//This class will be used by the UI bottom to buy a new tower
public class Shop : MonoBehaviour {

	private int numOfButtons;
	private bool[] canBuildTower;
	private Image image;
	private Transform[] buttons;
	private GameObject[] cantBuildImage;
	private SoulsCounter soulsCounter;
	private ScoreCounter scoreCounter;
	private BuildManager buildManager;

	/// <summary>
	/// Purcheses the tower0.
	/// </summary>
	public void PurcheseTower0(){
		Debug.Log ("0");
		int indexOfThisTower = 0;
		if ( !soulsCounter.CanBuild (indexOfThisTower) )
			return;
		buildManager.SetTowerToBuildIndex (indexOfThisTower);
		buildManager.SetTowerToBuild (buildManager.tower[indexOfThisTower]);
		buildManager.SetSelectionTowerToBuild (buildManager.selectionTower [indexOfThisTower]);
	}

	public void PurcheseTower1(){
		Debug.Log ("1");
		int indexOfThisTower = 1;
		if ( !soulsCounter.CanBuild (indexOfThisTower) )
			return;
		buildManager.SetTowerToBuildIndex (indexOfThisTower);
		buildManager.SetTowerToBuild (buildManager.tower[indexOfThisTower]);
		buildManager.SetSelectionTowerToBuild (buildManager.selectionTower [indexOfThisTower]);
	}

	public void PurcheseTower2(){
		Debug.Log ("2");
		int indexOfThisTower = 2;
		if ( !soulsCounter.CanBuild (indexOfThisTower) )
			return;
		buildManager.SetTowerToBuildIndex (indexOfThisTower);
		buildManager.SetTowerToBuild (buildManager.tower[indexOfThisTower]);
		buildManager.SetSelectionTowerToBuild (buildManager.selectionTower [indexOfThisTower]);
	}

	/// <summary>
	/// Determines whether this instance can build tower the specified index.
	/// </summary>
	/// <returns><c>true</c> if this instance can build tower the specified index; otherwise, <c>false</c>.</returns>
	/// <param name="index">Index.</param>
	public bool CanBuildTower(int index){
		return canBuildTower [index];
	}

	// follow the monkey 
//	public void PurcheseTower0(){
//		int indexOfThisTower = 0;
//		if ( !soulsCounter.CanBuild (indexOfThisTower) )
//			return;
//		buildManager.SetTowerToBuild (buildManager.Tower[indexOfThisTower]);
//	}

	//Will set the store up:
	private void Awake(){
		numOfButtons = transform.childCount;
		setTheShopButtons ();
		setTheCantBuyImages ();
		setCanBuildTower ();
	}

	//get the buttons gameobjects
	private void setTheShopButtons(){
		buttons = new Transform[numOfButtons];
		for (int i = 0; i < numOfButtons; i++){
			buttons [i] = transform.GetChild (i);
		}
	}

	//Get the cant buy images game objects
	private void setTheCantBuyImages(){
		cantBuildImage = new GameObject[numOfButtons];
		for (int i = 0; i < numOfButtons; i++){
			cantBuildImage [i] = buttons [i].gameObject.GetComponentInChildren<CantBuildScript> ().transform.gameObject;
		}
	}

	/// <summary>
	/// Sets the can build tower.
	/// </summary>
	private void setCanBuildTower(){
		canBuildTower = new bool[numOfButtons];
		for (int i = 0; i < numOfButtons; i++){
			canBuildTower [i] = false;
		}
	}

	//Instantiate and initialize 
	private void Start(){
		buildManager = GameObject.Find ("GameMaster").GetComponent<BuildManager> ();
		soulsCounter = SoulsCounter.instance;
		scoreCounter = ScoreCounter.instance;

		InitialCantBuildImageSet ();
	}

	/// <summary>
	/// Update this instance.
	/// every frame verify if the player have enought soul to 
	/// buy each tower
	/// </summary>
	private void Update(){
		UpdateCanBuildTower ();
	}

	/// <summary>
	/// Updates the can build tower.
	/// </summary>
	private void UpdateCanBuildTower(){
		for (int i = 0 ; i < buildManager.tower.Length; i ++){
			if (!soulsCounter.CanBuild (i)) {
				canBuildTower [i] = false;
				cantBuildImage [i].SetActive (true);
			} else {
				canBuildTower [i] = true;
				cantBuildImage [i].SetActive (false);
			}
		}
	}

	/// <summary>
	/// Initials the cant build image set.
	/// </summary>
	private void InitialCantBuildImageSet(){
		for (int i = 0 ; i < buildManager.tower.Length; i ++)
			cantBuildImage[i].SetActive (false);
	}
}
