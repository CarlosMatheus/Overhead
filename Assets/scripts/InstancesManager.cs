using UnityEngine;

public class InstancesManager : MonoBehaviour {

    [SerializeField] private GameObject shopGObj = null;
    [SerializeField] private GameObject masterTower = null;

	private TowerScript towerOfTheTime;
	private SphereShop sphereShop;

	public TowerScript GetTowerOfTheTime () 
    {
		return towerOfTheTime;
	}

	public void SetTowerOfTheTime (TowerScript _set) {
		if (towerOfTheTime == null) {
			towerOfTheTime = _set;
			towerOfTheTime.GetUpCanvas ().SetActive (true);

			if (sphereShop.IsShopping ()) {
				sphereShop.DesactiveShop ();
			}

			return;
		}

		towerOfTheTime.GetUpCanvas ().SetActive (false);

		if (_set == towerOfTheTime) {
			towerOfTheTime = null;
			return;
		}

		towerOfTheTime = _set;
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

	private void Start () 
    {
		sphereShop = GameObject.FindGameObjectWithTag ("Icosphere").GetComponent<SphereShop>();
	}
}
