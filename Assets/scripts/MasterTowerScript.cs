using UnityEngine;
using UnityEngine.UI;

public class MasterTowerScript : MonoBehaviour {

	public float initialLifes;

	private float lifes;
	private Text lifeCounterText;

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

	public void NewWave(){
		lifes++;
		UpdateLifeText ();
	}

	private void Start(){
		lifeCounterText = GameObject.Find ("LifeCounter").GetComponent<Text> ();
		lifes = initialLifes;
		UpdateLifeText ();
	}

	private void UpdateLifeText(){
		lifeCounterText.text = Mathf.Round (Mathf.Clamp(lifes, 0, 1000)).ToString();
	}
}
