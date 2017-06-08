using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineMainTower : MonoBehaviour {

    public GameObject cilinder;

    private void OnMouseEnter()
    {
        cilinder.GetComponent<OutlineObjectMainTower>().enabled = true;
        cilinder.GetComponent<OutlineObjectMainTower>().entrou = true;
    }

    private void OnMouseExit()
    {
        cilinder.GetComponent<OutlineObjectMainTower>().enabled = true;
        cilinder.GetComponent<OutlineObjectMainTower>().entrou = false;
    }
}
