using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour 
{
    private SkillsProperties skillProperties;
    private ShopManager shopManager;
    private ButtonClass thisButton;
    private GameObject description;
    private GameObject shopTowerItemImage;
    private GameObject AttackTowerValues;
    private GameObject upgradeProperty;
    private GameObject gameMaster;

    private SoulsCounter soulsCounter;

    public void StartButton(ButtonClass _thisButton)
    {
        thisButton = _thisButton;
        InitializeVariables();

        SetTowerName();
        SetTowerImage();
        SetTowerPrice();
        SetSpecificTowerProperties();
    }

    private void InitializeVariables()
    {
        gameMaster = GameObject.FindWithTag("GameMaster");
        shopManager = gameMaster.GetComponent<ShopManager>();
        soulsCounter = gameMaster.GetComponent<SoulsCounter>();
        skillProperties = thisButton.spell.GetComponent<SkillsProperties>();

        description = gameObject.transform.GetChild(1).GetChild(0).gameObject;
        shopTowerItemImage = gameObject.transform.GetChild(1).GetChild(1).GetChild(1).gameObject;
        AttackTowerValues = description.transform.GetChild(3).GetChild(0).gameObject;
        upgradeProperty = description.transform.GetChild(2).gameObject;
    }

    private void SetTowerName()
    {
        description.transform.GetChild(0).GetComponent<Text>().text = thisButton.name;
    }

    private void SetTowerImage()
    {
        shopTowerItemImage.GetComponent<Image>().sprite = thisButton.image;
    }

    private void SetTowerPrice()
    {
        description.transform.GetChild(1).GetComponent<Text>().text = soulsCounter.GetTowerPrice(thisButton.indexOfThisTower).ToString();
    }

    private void SetTowerAttackTowerProperties()
    {
        AttackTowerValues.transform.GetChild(0).GetComponent<Text>().text = skillProperties.GetDamage().ToString();
        AttackTowerValues.transform.GetChild(1).GetComponent<Text>().text = skillProperties.GetCooldown().ToString();
        AttackTowerValues.transform.GetChild(2).GetComponent<Text>().text = skillProperties.GetRange().ToString();
        AttackTowerValues.transform.GetChild(3).GetComponent<Text>().text = thisButton.effect;
    }

    private void SetUpgradeProperty()
    {
        upgradeProperty.GetComponent<Text>().text = thisButton.description;
    }

    private void SetSpecificTowerProperties()
    {
        if (thisButton.kind == ShopManager.Kind.AttackTower)
        {
            SetTowerAttackTowerProperties();
            upgradeProperty.SetActive(false);
        }
        else
        {
            SetUpgradeProperty();
            AttackTowerValues.transform.parent.gameObject.SetActive(false);
        }
    }
}
