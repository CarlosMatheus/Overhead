using UnityEngine;

public class SoulBonusUpgrade : Perk {
    
    [Space]
    [Header("Upgrade Settings")]
    [SerializeField]
    private float bonusChanceMultiplier = 2f;

    public override void LevelUp()
    {
        base.LevelUp();

        GetCurrentTowerProperties().SetSoulBonusEffect(true);
        GetCurrentTowerProperties().SetSoulBonusChance(bonusChanceMultiplier);
    }
}
