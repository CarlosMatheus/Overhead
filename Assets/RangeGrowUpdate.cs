using UnityEngine;

public class RangeGrowUpdate : Perk {

    [Space]
    [Header("Upgrade Settings")]
    [SerializeField]
    private float multiplicationFactor;

    public override void LevelUp()
    {
        base.LevelUp();

        GetCurrentTower().SetRange(multiplicationFactor);
    }
}
