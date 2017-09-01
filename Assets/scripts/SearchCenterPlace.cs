using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SearchCenterPlace : MonoBehaviour {

	private GameObject researchCanvas = null;
	private GameObject[] researchCanvasTypes = null;

	private GameObject masterTower;

    void Start () {
		masterTower = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<InstancesManager> ().GetMasterTowerObj ();
        GetResearchCanvas();
    }

    private void GetResearchCanvas()
    {
        researchCanvas = GetComponent<UpgradeCanvasManager>().GetUpCanvas();
        if (researchCanvas == null)
        {
            Debug.Log("Aqui");
            GetResearchCanvas();
        }
        else
        {
            SetUpResearchCanvas();
            return;
        }
    }

    public void SetUpResearchCanvas ()
    {
        HorizontalLayoutGroup[] hlg = researchCanvas.GetComponentsInChildren<HorizontalLayoutGroup>();
        researchCanvasTypes = new GameObject[hlg.Length];
        foreach (HorizontalLayoutGroup h in hlg)
        {
            researchCanvasTypes[Array.FindIndex(hlg, _h => _h == h)] = h.gameObject;
        }
        foreach (GameObject g in researchCanvasTypes)
        {
            g.SetActive(false);
        }
        researchCanvasTypes[0].SetActive(true);
    }

	public void ResearchOn(int i)
    {
        researchCanvasTypes[0].SetActive(false);
        researchCanvasTypes [i].SetActive (true);
	}

	public void SetValuesFromMasterTower (SkillsProperties sp) {
		masterTower.GetComponent<PropertiesManager>().SetValues(sp);
	}
}
