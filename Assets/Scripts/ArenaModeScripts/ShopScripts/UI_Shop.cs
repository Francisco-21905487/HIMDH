using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_Shop : MonoBehaviour
{
    public GameObject shop;
    public GameObject player;
    public Shooting playerShoot;
    public Transform itemsParent;
    public GameObject shopUI;
    public ShopInventory shopInventory;
    public AMManager amManager;

    ShopSlots[] slots;

    private float distance = 2f;

    public void Start()
    {
        slots = itemsParent.GetComponentsInChildren<ShopSlots>();
        //UpdateShopSlotUI();
    }

    public void Update()
    {
        if (Vector2.Distance(shop.transform.position, player.transform.position) < distance && Input.GetKeyDown(KeyCode.E) && amManager.canOpenShop)
        {
            shopUI.SetActive(!shopUI.activeSelf);
        }

        if (shopUI.activeSelf == false)
        {
            playerShoot.enabled = true;
        }
        else
        {
            playerShoot.enabled = false;
        }
    }

    public void UpdateShopSlotUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < shopInventory.shopItemList.Count)
            {
                slots[i].AddItemToShop(shopInventory.shopItemList[i]);
            }
        }
    }
}