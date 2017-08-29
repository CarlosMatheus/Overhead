using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchCenterPlace : MonoBehaviour {

	[SerializeField] private GameObject researchCanvas = null;
	[SerializeField] private GameObject[] researchCanvasTypes = null;

	void Start () {
		researchCanvas.SetActive (true);

		foreach (GameObject go in researchCanvasTypes) {
			go.SetActive (false);
		}
	}

	public void ResearchOn(int i) {
		researchCanvas.SetActive (false);
		researchCanvasTypes [i-1].SetActive (true);
	}
}
