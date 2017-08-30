using UnityEngine;

public class SideEffectUpgrade : Perk
{
    [Space]
    [Header("Upgrade Settings")]

    [Header("Slow Effect")]
    [SerializeField]
    private float slowAddFact;

    [Header("Range Effect")]
    [SerializeField]
    private float rangeMultFact;

    [Header("Burn Effect")]
    [SerializeField]
    private float burnMultFact;

    public override void LevelUp()
    {
        base.LevelUp();

        GetCurrentTower().SetSlowFactor(slowAddFact);

        GetCurrentTower().SetRangeRadius(rangeMultFact);

        GetCurrentTower().SetBurnRate(burnMultFact);
    }
}
