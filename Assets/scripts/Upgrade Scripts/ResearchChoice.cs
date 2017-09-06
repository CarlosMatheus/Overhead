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

        Text[] aux = gameObject.GetComponentsInChildren<Text>();

        foreach (Text t in aux)
        {

            if (t.gameObject.name == "TowerName")
            {
                t.text = "Reasearch type " + t.text;
            }
        }
    }

    public override void LevelUp()
    {
        base.LevelUp();

        GetCurrentTower().GetComponent<SearchCenterPlace>().ResearchOn(researchIndex);
    }
}
