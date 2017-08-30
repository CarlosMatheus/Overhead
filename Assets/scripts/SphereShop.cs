using UnityEngine;
using UnityEngine.SceneManagement;

public class SphereShop : MonoBehaviour 
{
	public float initialIntensity = 0.5f;
	public float hoverIntensity = 1f;
	
	private SoulsCounter soulsCounter;
	private BuildManager buildManager;
	private DeathManager deathManager;
    private PauseManager pauseManager;
    private TowerManager towerManager;
    private GameObject towerToBuild;
    private GameObject gameMaster;
    private GameObject ShopGObj;
    private TowerScript masterTowerTowerScript;
    public Transform player;
    public Transform center;
    private Light icosphereLight;
    private Shop shop;

	public void SetTowerToBuild(GameObject towerToB)
    {
		towerToBuild = towerToB;
	}

	public bool IsShopping () 
    {
		return ShopGObj.activeInHierarchy;
	}

	public void ActiveShop()
    {
		InstancesManager instancesManager = gameMaster.GetComponent<InstancesManager> ();
		UpgradeCanvasManager ucmScript = instancesManager.GetTowerOfTheTime ();
		if (ucmScript != null)
			instancesManager.SetTowerOfTheTime (ucmScript);
		ShopGObj.SetActive (true);
	}

	public void DesactiveShop()
	{
		buildManager.SetTowerToBuild (null);
		buildManager.DestroySelectionTowerToBuildInstance ();
		ShopGObj.SetActive (false);
		towerManager.TowerDiselected();
	}

	private void Start()
    {
        if (IsNotInCutScene())
        {
            gameMaster = GameObject.Find("GameMaster");
            deathManager = gameMaster.GetComponent<DeathManager>();
            pauseManager = gameMaster.GetComponent<PauseManager>();
            soulsCounter = gameMaster.GetComponent<SoulsCounter>();
            buildManager = gameMaster.GetComponent<BuildManager>();
            towerManager = gameMaster.GetComponent<TowerManager>();
            ShopGObj = GameObject.Find("Shop");
            if (IsInCorrectScene())
            {
                ShopGObj.SetActive(false);
                shop = ShopGObj.GetComponent<Shop>();
            }
            icosphereLight = GetComponent<Light>();
            icosphereLight.intensity = initialIntensity;

            masterTowerTowerScript = GameObject.FindWithTag("GameMaster").
                                               GetComponent<InstancesManager>().
                                               GetMasterTowerObj().
                                               GetComponent<TowerScript>();
        }
	}

    private bool IsInCorrectScene()
    {
        bool a = SceneManager.GetActiveScene().buildIndex != 0;
        bool b = string.Equals(SceneManager.GetActiveScene().name, "MainMenu") == false;
        return ( a && b);
    }

    private bool IsNotInCutScene()
    {
        bool c = string.Equals(SceneManager.GetActiveScene().name, "CutScenes") == false;
        return (c);
    }


	/// <summary>
	/// Update this instance.
	/// every frame check if player is is main tower and if he 
	/// can buy the tower he selects
	/// </summary>
	private void Update () 
    {
        if (IsInCorrectScene())
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
        if (IsInCorrectScene())
        {
            if (!deathManager.IsDead() && !pauseManager.IsPaused())
            {
                if (masterTowerTowerScript.IsPlayerInThisTower() == false)
                    return;
                icosphereLight.intensity = hoverIntensity;
            }
        }
	}

	private void OnMouseExit ()
    {
        if(IsInCorrectScene())
            icosphereLight.intensity = initialIntensity;
	}

	private void OnMouseDown()
    {
        if (IsInCorrectScene())
        {
            if (!deathManager.IsDead() && !pauseManager.IsPaused())
            {
                ActiveShop();
            }
        }
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
