using UnityEngine;

public class SoulBonusUpgrade : Perk {
    
    [Space]
    [Header("Upgrade Settings")]
    [SerializeField]
    private float bonusChance = 0f;

    public override void LevelUp()
    {
        base.LevelUp();

        GetCurrentTowerProperties().SetSoulBonusEffect(true);
        GetCurrentTowerProperties().SetSoulBonusChance(bonusChance);
    }
}
