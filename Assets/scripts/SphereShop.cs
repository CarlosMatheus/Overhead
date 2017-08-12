using UnityEngine;
using UnityEngine.SceneManagement;

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
    private PauseManager pauseManager;
    private TowerManager towerManager;

	public void SetTowerToBuild(GameObject towerToB){
		towerToBuild = towerToB;
	}

	private void Start(){
        gameMaster = GameObject.Find("GameMaster");
        deathManager = gameMaster.GetComponent<DeathManager> ();
        pauseManager = gameMaster.GetComponent<PauseManager>();
		soulsCounter = gameMaster.GetComponent<SoulsCounter>();
		buildManager = gameMaster.GetComponent<BuildManager>();
        towerManager = gameMaster.GetComponent<TowerManager>();
		ShopGObj = GameObject.Find ("Shop");
        if (SceneManager.GetActiveScene().buildIndex != 0)
		shop = ShopGObj.GetComponent<Shop> ();
		light = GetComponent<Light> ();
		light.intensity = initialIntensity;
        if (SceneManager.GetActiveScene().buildIndex != 0)
            ShopGObj.SetActive (false);
	}

	/// <summary>
	/// Update this instance.
	/// every frame check if player is is main tower and if he 
	/// can buy the tower he selects
	/// </summary>
	private void Update () {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            if ( IsInMainTower() )
            {
                CheckIfCanBuild();
                GetMouseRightMouseButtonDown();
            }
            else
            {
                CantBuild();
                DesactiveShop();
            }
        }
	}

    private void GetMouseRightMouseButtonDown()
    {
        //Mouse right click makes the store close
        if (Input.GetMouseButtonDown(1))
            DesactiveShop();
    }

	/// <summary>
	/// Checks if can build the tower you have in selection
	/// </summary>
	private void CheckIfCanBuild ()
    {
        if ( buildManager.GetTowerToBuild() != null )
        {
            if ( !soulsCounter.CanBuild( buildManager.GetTowerToBuildIndex() ) )
            {
                CantBuild();
            }
        }
	}

	private void OnMouseEnter ()
    {
		if (!deathManager.IsDead () && !pauseManager.IsPaused()) {
			light.intensity = hoverIntensity;
		}
	}

	private void OnMouseExit (){
		light.intensity = initialIntensity;
	}

	private void OnMouseDown(){
		if (!deathManager.IsDead () && !pauseManager.IsPaused()) {
			ActiveShop ();
		}
	}

	private void ActiveShop(){
		ShopGObj.SetActive (true);
	}

	private void DesactiveShop()
    {
		buildManager.SetTowerToBuild (null);
		buildManager.DestroySelectionTowerToBuildInstance ();
		ShopGObj.SetActive (false);
        towerManager.TowerDiselected();
	}

	private void CantBuild()
    {
		buildManager.SetTowerToBuild (null);     	   //
		buildManager.DestroySelectionTowerToBuildInstance ();  //The one that is been used in the moment
        towerManager.TowerDiselected();
	}

	private bool IsInMainTower()
    {
		if (Vector3.Distance (player.position, center.position) <= 1f) {
			return true;
		} else
			return false;
	}
}
