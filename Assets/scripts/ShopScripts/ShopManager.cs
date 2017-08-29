using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour 
{
    [SerializeField] private ButtonClass[] buttons = null;

    public enum Kind
    {
        AttackTower, UpgradeTower, AttackTowerUpgrade, UpgradeTowerUpgrade
    }

    public ButtonClass[] GetAttackTowers()
    {
        return FindButtonOfKind(Kind.AttackTower);
    }

    public ButtonClass GetResearchTower()
    {
        return FindButtonOfKind(Kind.UpgradeTower)[0];
    }

    public ButtonClass GetButtonClass(int index)
    {
        return buttons[index];
    }

    private ButtonClass[] FindButtonOfKind(Kind kind)
    {
        List<ButtonClass> buttonVector = new List<ButtonClass>();
        for (int i = 0; i < buttons.Length; i ++ )
        {
            if(buttons[i].kind == kind)
            {
                buttonVector.Add( buttons[i] );
            }
        }
        return buttonVector.ToArray();
    }
}
