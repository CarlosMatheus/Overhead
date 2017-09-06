using UnityEngine;

public class FireRateUpgrade : Perk {

    [Space]
    [Header("Upgrade Settings")]
    [Range(0f,1f)]
    [SerializeField] private float multiplicationFactor = 0f;

    public override void LevelUp()
    {
        base.LevelUp();

        GetCurrentTowerProperties().SetCooldown(multiplicationFactor);
    }

}
