using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterEffect : Perk
{
    [Space]
    [Header("Upgrade Settings")]
    [SerializeField]
    private GameObject sideEffect = null;

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
        player.GetComponent<PlayerController>().currentSkill.GetComponent<SkillsProperties>().SetEffect(sideEffect);
        masterTower.GetComponent<TowerScript>().bulletPrefab.GetComponent<SkillsProperties>().SetEffect(sideEffect);
    }
}
