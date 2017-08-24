using UnityEngine;

public class InstancesManager : MonoBehaviour {

    [SerializeField] private GameObject shopGObj = null;
	[SerializeField] private GameObject masterTower = null;
	[SerializeField] private GameObject player = null;

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

	public GameObject GetPlayerObj () {
		return player;
	}

	private void Start ()
    {
		sphereShop = GameObject.FindGameObjectWithTag ("Icosphere").GetComponent<SphereShop>();
	}
}
