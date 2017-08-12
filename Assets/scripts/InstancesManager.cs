using UnityEngine;

public class InstancesManager : MonoBehaviour {

    [SerializeField] private GameObject shopGObj;

    public GameObject GetShopGObj()
    {
        return shopGObj;
    }
}
