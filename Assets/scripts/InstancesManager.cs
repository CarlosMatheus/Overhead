using UnityEngine;

public class InstancesManager : MonoBehaviour {

    [SerializeField] private GameObject shopGObj = null;

	private TowerScript towerOfTheTime;

	public TowerScript GetTowerOfTheTime () {
		return towerOfTheTime;
	}

	public void SetTowerOfTheTime (TowerScript _set) {
		towerOfTheTime = _set;
	}

    public GameObject GetShopGObj()
    {
		return shopGObj;
    }
}
