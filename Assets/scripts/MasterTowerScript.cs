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
		GameObject gameMaster = GameObject.FindGameObjectWithTag ("GameMaster");

		towerManager = gameMaster.GetComponent<TowerManager> ();
		lifeCounterText = GameObject.Find ("LifeCounter").GetComponent<Text> ();
		lifes = initialLifes;
		UpdateLifeText ();
		AddMasterTowerToTowerManager ();

		GameObject player = gameMaster.GetComponent<InstancesManager> ().GetPlayerObj ();
		GameObject masterTower = gameMaster.GetComponent<InstancesManager> ().GetMasterTowerObj ();

		player.GetComponent<PlayerController> ().currentSkill.GetComponent<SkillsProperties> ().SetEffect (null);
		masterTower.GetComponent<TowerScript> ().bulletPrefab.GetComponent<SkillsProperties> ().SetEffect (null);

	}

    private void AddMasterTowerToTowerManager()
    {
        towerManager.AddTower(this.gameObject, masterTowerIndex);
    }

	private void UpdateLifeText()
    {
		lifeCounterText.text = Mathf.Round (Mathf.Clamp(lifes, 0, 1000)).ToString();
	}
}
