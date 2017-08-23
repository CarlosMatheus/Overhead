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
	[SerializeField] private Button perkButton;

	//private List<Perk> childs;
	[SerializeField] private Perk[] childs;

	private GameObject gameMaster;
	private SoulsCounter soulsCounter;
	private ScoreCounter scoreCounter;

	public bool isCallable = false;
	private int level = 0;

	void Start () 
    {
		name = gameObject.name;
		buttonName.text = name;

		gameMaster = GameObject.FindGameObjectWithTag("GameMaster");
		soulsCounter = gameMaster.GetComponent<SoulsCounter> ();
		scoreCounter = gameMaster.GetComponent<ScoreCounter> ();

		/* // If you wanna set automatically, try this. In our case, we choose not because of Canvas Horizontal Layout Group
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
		*/

		GetButton().interactable = isCallable;
	}

	public void LevelUp () {
		
		if (level <= maxLevel && soulsCounter.GetSouls() >= cost) {
			if (gameMaster.GetComponent<WaveSpawner> ().GetWave () >= minWaveToActivate) {
				if (isCallable) {
					// Set childs callables
					foreach (Perk p in childs) {
						p.TurnCallable ();
						p.GetButton().interactable = true;
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

	private Button GetButton () {
		return perkButton;
	}
}

// https://unity3d.com/pt/learn/tutorials/topics/scripting/overriding
