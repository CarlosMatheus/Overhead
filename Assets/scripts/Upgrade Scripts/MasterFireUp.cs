using UnityEngine;

public class MasterFireUp : Perk
{
    [Space]
    [Header("Upgrade Settings")]
    [Range(0f, 1f)]
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

        // Gives side effect to player's and master tower's spell
        player.GetComponent<PropertiesManager>().SetCooldown(multiplicationFactor);
        masterTower.GetComponent<PropertiesManager>().SetCooldown(multiplicationFactor);
    }

}
