using UnityEngine;
using UnityEngine.UI;

public class MasterTowerScript : MonoBehaviour {

	public float initialLifes;
	public Text lifeCounterText;

	private float lifes;

	public void SetLifes(float value){
		lifes = value;
	}

	public float GetLifes(){
		return lifes;
	}

	public void EnemyAttack(){
		lifes--;
		UpdateLifeText ();
	}

	private void Start(){
		lifes = initialLifes;
		UpdateLifeText ();
	}

	private void UpdateLifeText(){
		lifeCounterText.text = Mathf.Round (lifes).ToString();
	}
}
