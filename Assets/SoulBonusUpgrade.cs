using UnityEngine;

public class SoulBonusUpgrade : Perk {
    
    [Space]
    [Header("Upgrade Settings")]
    [SerializeField]
    private float bonusChance;

    public override void LevelUp()
    {
        base.LevelUp();

        GetCurrentTower().SetSoulBonusEffect(true);
        GetCurrentTower().SetSoulBonusChance(bonusChance);
    }
}
