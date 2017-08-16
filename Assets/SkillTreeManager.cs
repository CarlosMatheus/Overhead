using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeManager : MonoBehaviour {

	public Canvas skillTreeCanvas;

	void Start () {
		skillTreeCanvas.enabled = false;
	}

	void Update () {
		if (Input.GetKeyDown ("p")) {
			ActivateSkillTree ();
		}
	}

	public void ActivateSkillTree () {
		skillTreeCanvas.enabled = !skillTreeCanvas.enabled;
	}
}
