using UnityEngine;

public class MasterTowerUp : Perk
{
    [Space]
    [Header("Upgrade Settings")]
    [SerializeField]
    private SkillsProperties sp = null;

    private GameObject masterTower;
    private GameObject player;

    public override void Start ()
    {
        base.Start();
        player = gameMaster.GetComponent<InstancesManager>().GetPlayerObj();
        masterTower = gameMaster.GetComponent<InstancesManager>().GetMasterTowerObj();
    }

    public override void LevelUp()
    {
        base.LevelUp();
        masterTower.GetComponent<PropertiesManager>().SetValues(sp);
        masterTower.GetComponent<TowerScript>().DoFireAction();
    }

}
