using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShopItems
{
    public string itemName;
    public ShopItemType itemType;
    public int itemCost;
    public Sprite icon;
    public Vector3 scale;

    public enum ShopItemType
    {
        Weapon,
        Arrow,
        HealthPotion,
        Coin
    }
}