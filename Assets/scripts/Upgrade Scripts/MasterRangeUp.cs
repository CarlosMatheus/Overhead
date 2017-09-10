using UnityEngine;

public class MasterRangeUp : Perk
{
    [Space]
    [Header("Upgrade Settings")]
    [SerializeField]
    private float multiplicationFactor = 0f;

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

        player.GetComponent<PropertiesManager>().SetRange(multiplicationFactor);
        masterTower.GetComponent<PropertiesManager>().SetRange(multiplicationFactor);
    }
}
