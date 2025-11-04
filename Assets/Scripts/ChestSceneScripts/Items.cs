using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Items
{
    public string itemName;
    public ItemType itemType;
    public int amount;
    public int maxStack;
    public bool stackable;
    public Sprite icon;
    public Vector3 scale;

    public enum ItemType
    {
        Weapon,
        Arrow,
        HealthPotion,
        Coin
    }
}