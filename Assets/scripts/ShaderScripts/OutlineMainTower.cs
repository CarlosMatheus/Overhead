using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineMainTower : MonoBehaviour 
{
    public GameObject cilinder;

    private TowerScript towerScript;

    private void Start()
    {
        if (SceneVerifier.IsInMainSceneOrTutorial() == false) return;
        towerScript = GameObject.FindWithTag("GameMaster").GetComponent<InstancesManager>().GetMasterTowerObj().GetComponent<TowerScript>();
    }

    private void OnMouseEnter()
    {
        if (SceneVerifier.IsInMainSceneOrTutorial() == false) return;
        if( towerScript.IsPlayerInThisTower() == false )
        {
            cilinder.GetComponent<OutlineObjectMainTower>().enabled = true;
            cilinder.GetComponent<OutlineObjectMainTower>().entrou = true;
        }
    }

    private void OnMouseExit()
    {
        if (SceneVerifier.IsInMainSceneOrTutorial() == false) return;
        cilinder.GetComponent<OutlineObjectMainTower>().enabled = true;
        cilinder.GetComponent<OutlineObjectMainTower>().entrou = false;
    }
}
