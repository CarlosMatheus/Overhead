using UnityEngine;

public class SideEffectUpgrade : Perk
{
    [Space]
    [Header("Upgrade Settings")]

    [Header("Slow Effect")]
    [SerializeField]
    private float slowAddFact = 0f;

    [Header("Range Effect")]
    [SerializeField]
    private float rangeMultFact = 0f;

    [Header("Burn Effect")]
    [SerializeField]
    private float burnMultFact = 0f;

    public override void LevelUp()
    {
        base.LevelUp();

        GetCurrentTowerProperties().SetSlowFactor(slowAddFact);

        GetCurrentTowerProperties().SetRangeRadius(rangeMultFact);

        GetCurrentTowerProperties().SetBurnRate(burnMultFact);
    }
}
