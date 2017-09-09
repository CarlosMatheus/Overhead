using UnityEngine;

public class StrenghUpUpdate : Perk {
    
    [Space]
    [Header("Upgrade Settings")]
    [SerializeField]
    private float multiplicationFactor = 0f;

    public override void LevelUp()
    {
        base.LevelUp();

        GetCurrentTowerProperties().SetDamage(multiplicationFactor);
    }
}
