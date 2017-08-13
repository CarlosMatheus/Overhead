﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour {

    [SerializeField] private int numOfTowers;
    [SerializeField] private GameObject icosphere;

    private List<TowerListArr>[] towerListArr;
    private float originalIcosphereColiderRadius;

    public void AddTower(GameObject tower,int indexOfTower)
    {
        TowerListArr _towerListArr = new TowerListArr();
        _towerListArr.tower = tower;
        towerListArr[indexOfTower].Add(_towerListArr);
    }

    public void TowerSelected()
    {
        for (int j = 0; j < numOfTowers; j++)
        {
            for (int i = 0; i < towerListArr[j].Count; i++)
            {
                towerListArr[j][i].tower.GetComponent<TowerScript>().AppearRange();
                towerListArr[j][i].originalColliderSize = towerListArr[j][i].tower.GetComponent<BoxCollider>().size;
                towerListArr[j][i].tower.GetComponent<BoxCollider>().size = new Vector3(0,0,0);
            }
        }
        originalIcosphereColiderRadius = icosphere.GetComponent<SphereCollider>().radius;
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

    private class TowerListArr
    {
        public GameObject tower;
        public Vector3 originalColliderSize;
    }

}
