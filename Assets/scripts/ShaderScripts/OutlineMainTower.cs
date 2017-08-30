using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineMainTower : MonoBehaviour 
{
    public GameObject cilinder;

    private TowerScript towerScript;

    private void Start()
    {
        towerScript = GameObject.FindWithTag("GameMaster").GetComponent<InstancesManager>().GetMasterTowerObj().GetComponent<TowerScript>();
    }

    private void OnMouseEnter()
    {
        if( towerScript.IsPlayerInThisTower() == false )
        {
            cilinder.GetComponent<OutlineObjectMainTower>().enabled = true;
            cilinder.GetComponent<OutlineObjectMainTower>().entrou = true;
        }
    }

    private void OnMouseExit()
    {
        cilinder.GetComponent<OutlineObjectMainTower>().enabled = true;
        cilinder.GetComponent<OutlineObjectMainTower>().entrou = false;
    }
}
