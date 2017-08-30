using UnityEngine;

public class InstancesManager : MonoBehaviour {

    [SerializeField] private GameObject shopGObj = null;
    [SerializeField] private GameObject masterTower = null;
    [SerializeField] private GameObject cameraPlayer = null;
    [SerializeField] private Mesh voxel4x4 = null;
    [SerializeField] private Mesh voxel3x3 = null;
    [SerializeField] private Mesh voxel2x2 = null;
    [SerializeField] private Mesh voxel1x1 = null;
	[SerializeField] private GameObject player = null;
    [SerializeField] private GameObject upgradeCanvas = null;

    private UpgradeCanvasManager towerOfTheTime;
	private SphereShop sphereShop;

	public UpgradeCanvasManager GetTowerOfTheTime () {
		return towerOfTheTime;
    }

    public void SetTowerOfTheTime (UpgradeCanvasManager _set) {
		if (towerOfTheTime == null) {
			towerOfTheTime = _set;
			towerOfTheTime.GetUpCanvas ().SetActive (true);

			if (sphereShop.IsShopping ()) {
				sphereShop.DesactiveShop ();
			}

			return;
		}

		// Maybe we can put some animation on deactivating one canvas
		towerOfTheTime.GetUpCanvas ().SetActive (false);

		if (_set == towerOfTheTime) {
			towerOfTheTime = null;
			return;
		}

		towerOfTheTime = _set;

		// And an animation to activate other
		towerOfTheTime.GetUpCanvas ().SetActive (true);
	}

    public GameObject GetShopGObj()
    {
		return shopGObj;
    }

    public GameObject GetMasterTowerObj()
    {
        return masterTower;
    }

    public GameObject GetCameraPlayer()
    {
        return cameraPlayer;
    }

    public Mesh GetVoxel4x4()
    {
        return voxel4x4;
    }

    public Mesh GetVoxel3x3()
    {
        return voxel3x3;
    }

    public Mesh GetVoxel2x2()
    {
        return voxel2x2;
    }

    public Mesh GetVoxel1x1()
    {
        return voxel1x1;
    }

	 public GameObject GetPlayerObj ()
    {
		    return player;
    }

    public GameObject GetUpgradeCanvas()
    {
        return upgradeCanvas;
    }

    private void Start ()
  {
		sphereShop = GameObject.FindGameObjectWithTag ("Icosphere").GetComponent<SphereShop>();
	}
}
