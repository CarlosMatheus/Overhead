using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEffectUpgrade : Perk
{
    [Space]
    [Header("Upgrade Settings")]
    [SerializeField]
    private float multiplicationFactor;

    public override void LevelUp()
    {
        base.LevelUp();

        GetCurrentTower().SetRangeRadius(multiplicationFactor);
    }
}
