using UnityEngine;

public class SphereShop : MonoBehaviour {

	public float initialIntensity = 0.5f;
	public float hoverIntensity = 1f;
	public Transform player;
	public Transform center;

	private GameObject gameMaster;
	private GameObject towerToBuild;
	private SoulsCounter soulsCounter;
	private BuildManager buildManager;
	private Light light;
	private Shop shop;
	private GameObject ShopGObj;
	private DeathManager deathManager;

	public void SetTowerToBuild(GameObject towerToB){
		towerToBuild = towerToB;
	}

	private void Start(){
		deathManager = GameObject.Find ("GameMaster").GetComponent<DeathManager> ();
		gameMaster = GameObject.Find ("GameMaster");
		soulsCounter = gameMaster.GetComponent<SoulsCounter>();
		buildManager = GameObject.Find("GameMaster").GetComponent<BuildManager>();
		ShopGObj = GameObject.Find ("Shop");
		shop = ShopGObj.GetComponent<Shop> ();
		light = GetComponent<Light> ();
		light.intensity = initialIntensity;
		ShopGObj.SetActive (false);
	}

	/// <summary>
	/// Update this instance.
	/// every frame check if player is is main tower and if he 
	/// can buy the tower he selects
	/// </summary>
	private void Update () {
		if (IsInMainTower ()) {
			checkIfCanBuild ();
			//Mouse right click makes the store close
			if (Input.GetMouseButtonDown (1))
				DesactiveShop ();
		} else {
			CantBuild ();
			DesactiveShop ();
		}
	}

	/// <summary>
	/// Checks if can build the tower you have in selection
	/// </summary>
	private void checkIfCanBuild (){
		if(buildManager.GetTowerToBuild() != null)
			if ( !soulsCounter.CanBuild( buildManager.GetTowerToBuildIndex () ) )
				CantBuild ();
	}

	private void OnMouseEnter (){
		if (!deathManager.IsDead ()) {
			light.intensity = hoverIntensity;
		}
	}

	private void OnMouseExit (){
		light.intensity = initialIntensity;
	}

	private void OnMouseDown(){
		if (!deathManager.IsDead ()) {
			ActiveShop ();
		}
	}

	private void ActiveShop(){
		ShopGObj.SetActive (true);
	}

	private void DesactiveShop(){
		buildManager.SetTowerToBuild (null);
		buildManager.DestroySelectionTowerToBuildInstance ();
		ShopGObj.SetActive (false);
	}

	private void CantBuild(){
		buildManager.SetTowerToBuild (null);     	   //
		buildManager.DestroySelectionTowerToBuildInstance ();  //The one that is been used in the moment
	}

	private bool IsInMainTower(){
		if (Vector3.Distance (player.position, center.position) <= 1f) {
			return true;
		} else
			return false;
	}
}
