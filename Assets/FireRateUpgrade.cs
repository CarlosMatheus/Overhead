using UnityEngine;

public class FireRateUpgrade : Perk {

    [Space]
    [Header("Upgrade Settings")]
    [Range(0f,1f)]
    [SerializeField] private float multiplicationFactor;

    public override void LevelUp()
    {
        base.LevelUp();
        
        GetCurrentTower().SetCooldown(multiplicationFactor);
    }

}
