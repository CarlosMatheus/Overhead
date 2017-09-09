using UnityEngine;

public class RangeGrowUpdate : Perk {

    [Space]
    [Header("Upgrade Settings")]
    [SerializeField]
    private float multiplicationFactor = 0f;

    public override void LevelUp()
    {
        base.LevelUp();

        GetCurrentTowerProperties().SetRange(multiplicationFactor);
    }
}
