using UnityEngine;

public class EffectBubbleUpgrade : Perk
{
    [Space]
    [Header("Upgrade Settings")]
    [SerializeField]
    private GameObject rangeEffect = null;
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

        rangeEffect.GetComponent<SideEffect>().SetNormalBulletEffect(sideEffect);

        player.GetComponent<PlayerController>().currentSkill.GetComponent<SkillsProperties>().SetEffect(rangeEffect);
        masterTower.GetComponent<TowerScript>().bulletPrefab.GetComponent<SkillsProperties>().SetEffect(rangeEffect);
    }
}