using UnityEngine;

public class SlowTimeUpgrade : Perk
{
    [Space]
    [Header("Upgrade Settings")]
    [SerializeField]
    private float bonusChanceMultiplier = 2f;

    private GameObject masterTower;
    private GameObject player;

    public override void Start()
    {
        base.Start();
        player = gameMaster.GetComponent<InstancesManager>().GetPlayerObj();
        masterTower = gameMaster.GetComponent<InstancesManager>().GetMasterTowerObj();
    }

    public override void LevelUp()
    {
        base.LevelUp();

        player.GetComponent<PropertiesManager>().SetSlowTimeEffect(true);
        player.GetComponent<PropertiesManager>().SetSlowTimeChance(bonusChanceMultiplier);
        masterTower.GetComponent<PropertiesManager>().SetSlowTimeEffect(true);
        masterTower.GetComponent<PropertiesManager>().SetSlowTimeChance(bonusChanceMultiplier);
    }
}