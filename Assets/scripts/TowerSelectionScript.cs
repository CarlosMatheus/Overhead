using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelectionScript : MonoBehaviour {

    [SerializeField] private GameObject rangeObject;
    [SerializeField] private GameObject tower;

    private TowerScript towerScript;
    private Transform rangeObjectTransform;
    private float range;

    private void Start()
    {
        towerScript = tower.GetComponent<TowerScript>();
        rangeObjectTransform = rangeObject.transform;

        range = towerScript.GetRange();
        rangeObjectTransform.localScale = new Vector3(range*2,0.01f,range*2);
    }
}
