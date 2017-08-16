using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class Perk : MonoBehaviour {

	[SerializeField] private new string name;
	[SerializeField] private GameObject skill;
	[SerializeField] private Text buttonName;
	[SerializeField] private float minWaveToActivate = 0f;

	private List<Perk> childs;

	public bool isCallable = false;
	private int level = 0;

	void Start () {
		Perk[] _childs = gameObject.GetComponentsInChildren<Perk> ();
		foreach (Perk p in _childs) {
			if (p.transform.parent != this.transform)
				_childs[Array.FindIndex (_childs, perk => perk == p)] = null;
		}

		childs = new List<Perk> ();

		foreach (Perk p in _childs) {
			if (p != null) {
				childs.Add (p);
			}
		}

		buttonName.text = name;

		GetComponentInChildren<Button> ().interactable = isCallable;
	}

	public void LevelUp () {

		//if (gameObject.GetComponent<WaveSpawner> ().GetWave () >= minWaveToActivate) {
			if (isCallable) {
				// Set childs callables
				foreach (Perk p in childs) {
					p.TurnCallable ();
					p.GetComponent<Button> ().interactable = true;
				}

				level++;

				// Call a default function on skill gameObject to alter values
				Debug.Log ("LevelUp!");
			}
		//}
	}

	public void LevelDown () {
		if (level > 0) {
			// Set childs callables
			foreach (Perk p in childs) {
				p.TurnUncallable ();
			}

			level--;

			// Call a default function on skill gameObject to alter values
			Debug.Log("LevelDown!");

			if (level == 0) {
				TurnUncallable ();
			}
		}
	}

	public void TurnCallable () {
		isCallable = true;
	}
	public void TurnUncallable () {
		isCallable = false;
	}
}

// https://unity3d.com/pt/learn/tutorials/topics/scripting/overriding
