using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnEffectUpgrade : Perk
{
    [Space]
    [Header("Upgrade Settings")]
    [SerializeField]
    private float multiplicationFactor = 0f;

    public override void LevelUp()
    {
        base.LevelUp();

        GetCurrentTowerProperties().SetBurnRate(multiplicationFactor);
    }
}
