using UnityEngine;
using UnityEngine.UI;

public class MasterTowerScript : MonoBehaviour {

    [SerializeField] private int masterTowerIndex;

	public float initialLifes;

	private float lifes;
	private Text lifeCounterText;

    private TowerManager towerManager;

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
        towerManager = GameObject.Find("GameMaster").GetComponent<TowerManager>();
		lifeCounterText = GameObject.Find ("LifeCounter").GetComponent<Text> ();
		lifes = initialLifes;
		UpdateLifeText ();
        AddMasterTowerToTowerManager();
	}

    private void AddMasterTowerToTowerManager()
    {
        towerManager.AddTower(this.gameObject, masterTowerIndex);
    }

	private void UpdateLifeText(){
		lifeCounterText.text = Mathf.Round (Mathf.Clamp(lifes, 0, 1000)).ToString();
	}
}
