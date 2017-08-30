using UnityEngine;
using System.Collections;

[System.Serializable]
public class ButtonClass
{
    public ShopManager.Kind kind;
    public string name;
    public Sprite image;
    public GameObject spell = null;
    public int indexOfThisTower;
    public string effect = null;
    public string description = null;

}
