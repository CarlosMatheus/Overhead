using UnityEngine;

public class MasterTowerScript : MonoBehaviour {

	public float initialLifes;

	private float lifes;

	public void SetLifes(float value){
		lifes = value;
	}

	public float GetLifes(){
		return lifes;
	}

	public void EnemyAttack(){
		lifes--;
	}

	private void Start(){
		lifes = initialLifes;
	}
		
	private void Update(){
		Debug.Log (lifes);
	}
}
