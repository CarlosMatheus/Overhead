using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchCenterPlace : MonoBehaviour {

	[SerializeField] private GameObject researchCanvas = null;
	[SerializeField] private GameObject[] researchCanvasTypes = null;

	private GameObject masterTower;

	void Start () {
		masterTower = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<InstancesManager> ().GetMasterTowerObj ();

		researchCanvas.SetActive (true);

		foreach (GameObject go in researchCanvasTypes) {
			go.SetActive (false);
		}
	}

	public void ResearchOn(int i) {
		researchCanvas.SetActive (false);
		researchCanvasTypes [i-1].SetActive (true);
	}

	public void SetValuesFromMasterTower (SkillsProperties sp) {
		masterTower.GetComponent<PropertiesManager>().SetValues(sp);
	}
}
