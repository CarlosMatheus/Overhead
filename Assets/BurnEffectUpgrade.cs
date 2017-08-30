using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnEffectUpgrade : Perk
{
    [Space]
    [Header("Upgrade Settings")]
    [SerializeField]
    private float multiplicationFactor;

    public override void LevelUp()
    {
        base.LevelUp();

        GetCurrentTower().SetBurnRate(multiplicationFactor);
    }
}
