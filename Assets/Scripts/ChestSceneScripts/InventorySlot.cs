using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;
    public TextMeshProUGUI itemNameSlotText;
    public TextMeshProUGUI itemAmountSlotText;

    private GameObject player;
    private PlayerInventory playerInventory;

    Items item;

    private void Start()
    {
        player = GameObject.Find("Player");
        playerInventory = player.GetComponent<PlayerInventory>();
    }

    public void Add(Items newItem)
    {
        item = newItem;
        icon.enabled = true;
        icon.sprite = newItem.icon;
        icon.rectTransform.localScale = newItem.scale;
        itemNameSlotText.enabled = true;
        itemNameSlotText.text = newItem.itemName;
        itemAmountSlotText.enabled = true;
        itemAmountSlotText.text = "x" + newItem.amount;
        removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.enabled = false;
        itemNameSlotText.enabled = false;
        itemNameSlotText.text = null;
        itemAmountSlotText.enabled = false;
        itemAmountSlotText.text = null;
        removeButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        playerInventory.DropItem(item);
    }

    public void UseItem()
    {
        if (item != null)
        {
            if (item.itemType == Items.ItemType.HealthPotion)
            {
                playerInventory.UseConsumable(item);
            }
            else if (item.itemType == Items.ItemType.Arrow)
            {
                playerInventory.EquipArrow(item);
            }
            else if (item.itemType == Items.ItemType.Weapon)
            {
                playerInventory.EquipWeapon(item);
            }
        }
    }
}