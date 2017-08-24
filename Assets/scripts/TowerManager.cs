using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour {

    [SerializeField] private int numOfTowers = 0;
    [SerializeField] private GameObject icosphere = null;

    private List<TowerListArr>[] towerListArr;
    private float originalIcosphereColiderRadius;

    public void AddTower(GameObject tower,int indexOfTower)
    {
        TowerListArr _towerListArr = new TowerListArr();
        _towerListArr.tower = tower;
        _towerListArr.originalColliderSize = tower.GetComponent<BoxCollider>().size;
        towerListArr[indexOfTower].Add(_towerListArr);
    }

    public void TowerSelected()
    {
        for (int j = 0; j < numOfTowers; j++)
        {
            for (int i = 0; i < towerListArr[j].Count; i++)
            {
                towerListArr[j][i].tower.GetComponent<TowerScript>().AppearRange();
                towerListArr[j][i].tower.GetComponent<BoxCollider>().size = new Vector3(0,0,0);
            }
        }
        icosphere.GetComponent<SphereCollider>().radius = 0;
    }

    public void TowerDiselected()
    {
		for (int j = 0; j < numOfTowers; j++)
		{
			for (int i = 0; i < towerListArr[j].Count; i++)
			{
				towerListArr[j][i].tower.GetComponent<TowerScript>().DisappearRange();
                towerListArr[j][i].tower.GetComponent<BoxCollider>().size = towerListArr[j][i].originalColliderSize;
			}
		}
        icosphere.GetComponent<SphereCollider>().radius = originalIcosphereColiderRadius;
    }

    private void Awake()
    {
        towerListArr = new List<TowerListArr>[numOfTowers];
        for(int i = 0; i < numOfTowers ; i++)
        {
            towerListArr[i] = new List<TowerListArr>();
        }
    }

    private void Start()
    {
        originalIcosphereColiderRadius = icosphere.GetComponent<SphereCollider>().radius;
    }

    private class TowerListArr
    {
        public GameObject tower;
        public Vector3 originalColliderSize;
    }

}
