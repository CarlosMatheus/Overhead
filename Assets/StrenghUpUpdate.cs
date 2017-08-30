using UnityEngine;

public class StrenghUpUpdate : Perk {
    
    [Space]
    [Header("Upgrade Settings")]
    [SerializeField]
    private float multiplicationFactor;

    public override void LevelUp()
    {
        base.LevelUp();

        GetCurrentTower().SetDamage(multiplicationFactor);
    }
}
