using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeManager : MonoBehaviour {

	public Canvas skillTreeCanvas;

	void Start () {
		if (skillTreeCanvas == null)
			return;

		skillTreeCanvas.enabled = false;
		skillTreeCanvas.gameObject.SetActive (true);
	}

	void Update () {
		if (Input.GetKeyDown ("p")) {
			ActivateSkillTree ();
		}
	}

	public void ActivateSkillTree () {
		
		if (skillTreeCanvas == null)
			return;
		
		skillTreeCanvas.enabled = !skillTreeCanvas.enabled;
	}
}
