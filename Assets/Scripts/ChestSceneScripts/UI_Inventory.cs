using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;
    public Shooting playerShoot;

    private PlayerInventory inventory;

    InventorySlot[] slots;

    public void SetInventory(PlayerInventory inventory)
    {
        this.inventory = inventory;
        //UpdateUI();

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }

        if (inventoryUI.activeSelf == false)
        {
            playerShoot.enabled = true;
        }
        else
        {
            playerShoot.enabled = false;
        }
    }

    public void UpdateSlotUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.itemList.Count)
            {
                slots[i].Add(inventory.itemList[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}