using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	const int FIRST = 0, SECOND = 1;

	public Slider healthBar;
	public GameObject enemyHealthBar;
	public GameObject[] currentTarget;
	public Material targetMaterial;

	float maximumHealth = 100.0f;
	float currentHealth;

	// Use this for initialization
	void Start () {
		currentHealth = maximumHealth;
		healthBar.value = SetHealth(currentHealth);
		enemyHealthBar.SetActive (false);
		currentTarget = new GameObject [2];
	}

	void Update () {
		if (currentTarget [0] != null) {   // If has target someone
			enemyHealthBar.SetActive (true);   // Active HP on top screen
			enemyHealthBar.GetComponent<Slider> ().value = SetHealth (currentTarget [0].GetComponent<TargetSelection> ().HP);  // Shows enemy HP
		
			// Turns target material color into original material color and atribute it to target object
			targetMaterial.color = currentTarget [0].GetComponent<TargetSelection> ().originalMaterial.color;
			currentTarget [0].GetComponent<MeshRenderer> ().material = targetMaterial;

			// Garants that untarget objects turn their original material back
			if (currentTarget [1] != null) {
				currentTarget [1].GetComponent<MeshRenderer> ().material = currentTarget [1].GetComponent<TargetSelection> ().originalMaterial;
			}

		} else {
			enemyHealthBar.SetActive (false);
		}
	}

	float SetHealth (float currentHealth) {
		return currentHealth / maximumHealth;
	}
}
