using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class Perk : MonoBehaviour {

	private new string name;

	[SerializeField] private Text buttonName = null;
	[SerializeField] private float minWaveToActivate = 0f;
	[SerializeField] private float maxLevel = 0f;
	[SerializeField] private float cost = 0f;
	[SerializeField] private float addScore = 0f;

	private List<Perk> childs;

	private GameObject gameMaster;
	private SoulsCounter soulsCounter;
	private ScoreCounter scoreCounter;

	public bool isCallable = false;
	private int level = 0;

	void Start () {
		name = gameObject.name;

		gameMaster = GameObject.FindGameObjectWithTag("GameMaster");
		soulsCounter = gameMaster.GetComponent<SoulsCounter> ();
		scoreCounter = gameMaster.GetComponent<ScoreCounter> ();

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
		
		if (level <= maxLevel && soulsCounter.GetSouls() >= cost) {
			if (gameMaster.GetComponent<WaveSpawner> ().GetWave () >= minWaveToActivate) {
				if (isCallable) {
					// Set childs callables
					foreach (Perk p in childs) {
						p.TurnCallable ();
						p.GetComponent<Button> ().interactable = true;
					}

					// Upgrade perk
					level++;

					// Consume souls to level up
					soulsCounter.SetSouls (soulsCounter.GetSouls() - cost);

					// Add score
					scoreCounter.SetScore (scoreCounter.GetScore() + addScore);

					// Call a default function on skill gameObject to alter values
					Debug.Log ("LevelUp to level" + level);
				}
			}
		}
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

	public void Upgrade (int increment) {

	}
}

// https://unity3d.com/pt/learn/tutorials/topics/scripting/overriding
