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

    private float price;
    private float damage;
    private float range;
    private float cooldown;

    public void SetPrice(float _price)
    {
        price = _price;
    }

    public float GetPrice()
    {
        return price;
    }

    public void SetDamage(float _damage)
    {
        damage = _damage;
    }

    public float GetDamage()
    {
        return damage;
    }

    public void SetRange(float _range)
    {
        range = _range;
    }

    public float GetRange()
    {
        return range;
    }

    public void SetCooldown(float _cooldown)
    {
        cooldown = _cooldown;
    }

    public float GetCooldown()
    {
        return cooldown;
    }
}
