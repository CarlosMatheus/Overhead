using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterStrenghUpgrade : Perk
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

        player.GetComponent<PropertiesManager>().SetDamage(multiplicationFactor);
        masterTower.GetComponent<PropertiesManager>().SetDamage(multiplicationFactor);
    }
}
