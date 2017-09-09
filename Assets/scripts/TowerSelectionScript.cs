using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelectionScript : MonoBehaviour {

    [SerializeField] private GameObject rangeObject = null;
    [SerializeField] private GameObject tower = null;

    private TowerScript towerScript;
    private Transform rangeObjectTransform;
    private float range;

    private void Start()
    {
        if (rangeObject == null) return;
        towerScript = tower.GetComponent<TowerScript>();
        rangeObjectTransform = rangeObject.transform;
		range = towerScript.bulletPrefab.GetComponent<SkillsProperties>().GetRange();
        rangeObjectTransform.localScale = new Vector3(range*2,0.01f,range*2);
    }
}
