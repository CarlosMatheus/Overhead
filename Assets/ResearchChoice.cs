using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchChoice : Perk
{
    private int researchIndex;

    public override void Start()
    {
        base.Start();

        int.TryParse(gameObject.name, out researchIndex); Debug.Log(researchIndex);
    }

    public override void LevelUp()
    {
        base.LevelUp();

        GetCurrentTower().GetComponent<SearchCenterPlace>().ResearchOn(researchIndex);
    }
}
