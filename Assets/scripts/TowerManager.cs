using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour {

    [SerializeField] private int numOfTowers;

    private List<GameObject>[] towerListArr;

    public void AddTower(GameObject tower,int indexOfTower)
    {
        towerListArr[indexOfTower].Add(tower);
    }

    public void TowerSelected()
    {
        for (int j = 0; j < numOfTowers; j++)
        {
            for (int i = 0; i < towerListArr[j].Count; i++)
            {
                towerListArr[j][i].GetComponent<TowerScript>().AppearRange();
            }
        }
    }

    public void TowerDiselected()
    {
		for (int j = 0; j < numOfTowers; j++)
		{
			for (int i = 0; i < towerListArr[j].Count; i++)
			{
				towerListArr[j][i].GetComponent<TowerScript>().DisappearRange();
			}
		}
    }

    private void Awake()
    {
        towerListArr = new List<GameObject>[numOfTowers];
        for(int i = 0; i < numOfTowers ; i++)
        {
            towerListArr[i] = new List<GameObject>();
        }
    }
}
