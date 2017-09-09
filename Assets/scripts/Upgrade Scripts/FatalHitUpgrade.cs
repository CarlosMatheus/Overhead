using UnityEngine;

public class FatalHitUpgrade : Perk
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

        player.GetComponent<PropertiesManager>().SetFatalHitEffect(true);
        player.GetComponent<PropertiesManager>().SetFatalHitChance(bonusChanceMultiplier);
        masterTower.GetComponent<PropertiesManager>().SetFatalHitEffect(true);
        masterTower.GetComponent<PropertiesManager>().SetFatalHitChance(bonusChanceMultiplier);
    }
}