using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Items;

public class ShopSlots : MonoBehaviour
{
    public Image itemIcon;
    public Image coinIcon;
    //public Button itemButton;
    public TextMeshProUGUI itemNameSlotText;
    public TextMeshProUGUI itemCostSlotText;
    public bool startTimer = false;
    public float timerToDelete = 1f;
    public bool isBuying = false;

    public Transform itemsParent;
    private ShopItems shopItem;
    private GameObject player;
    private PlayerInventory playerInventory;
    private GameObject confirmationPanel;
    private TextMeshProUGUI shopText;
    private TextMeshProUGUI informationToPlayer;
    private ShopSlots[] slots;

    private void Awake()
    {
        confirmationPanel = GameObject.Find("ConfirmPanel");
        shopText = confirmationPanel.GetComponentInChildren<TextMeshProUGUI>();
        informationToPlayer = GameObject.Find("InformationToPlayer").GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        slots = itemsParent.GetComponentsInChildren<ShopSlots>();
        player = GameObject.Find("Player");
        playerInventory = player.GetComponent<PlayerInventory>();
        if (confirmationPanel != null)
        {
            confirmationPanel.SetActive(false);
        }
    }

    private void Update()
    {
        if (startTimer)
        {
            timerToDelete -= Time.deltaTime;
            if (timerToDelete <= 0)
            {
                informationToPlayer.text = "";
                startTimer = false;
            }
        }
        else
        {
            timerToDelete = 1f;
        }

    }

    public void AddItemToShop(ShopItems newItem)
    {
        shopItem = newItem;
        itemIcon.enabled = true;
        itemIcon.sprite = newItem.icon;
        itemIcon.rectTransform.localScale = newItem.scale;
        coinIcon.enabled = true;
        itemNameSlotText.enabled = true;
        itemNameSlotText.text = shopItem.itemName;
        itemCostSlotText.enabled = true;
        itemCostSlotText.text = "" + shopItem.itemCost;
        //itemButton.interactable = true;
    }

    public void OpenConfirmationPanelToBuyItem()
    {
        confirmationPanel.SetActive(true);
        shopText.text = "Do you want to buy " + shopItem.itemName + " for " + shopItem.itemCost + " coins?";
        isBuying = true;
        //BuyItemFromShop();
    }

    public void CloseConfirmationPanelToBuyItem()
    {
        //Close the panel
        confirmationPanel.SetActive(false);
        isBuying = false;
    }

    public void ConfirmToBuyItem(bool confirmation)
    {
        if (confirmation)
        {
            BuyItemFromShop();
            CloseConfirmationPanelToBuyItem();
        }
        else
        {
            CloseConfirmationPanelToBuyItem();
        }
    }

    public void BuyItemFromShop()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].isBuying)
            {
                if (playerInventory.ExistsItem("Coin"))
                {
                    Items itemToBuy = playerInventory.FindItemByName(slots[i].shopItem.itemName);
                    Items coins = playerInventory.FindItemByName("Coin");

                    if (itemToBuy != null)
                    {
                        if (coins.amount >= slots[i].shopItem.itemCost)
                        {
                            if(itemToBuy.stackable == true)
                            {
                                if (itemToBuy.amount < itemToBuy.maxStack)
                                {
                                    startTimer = true;
                                    informationToPlayer.text = "Bought item: " + slots[i].shopItem.itemName;
                                    playerInventory.RemoveCoin(coins, "Coin", slots[i].shopItem.itemCost);
                                    playerInventory.AddItemFromShop(slots[i].shopItem);
                                }
                                else
                                {
                                    startTimer = true;
                                    informationToPlayer.text = "You already have the max of this item";
                                }
                            }
                            else
                            {
                                startTimer = true;
                                informationToPlayer.text = "You already have this item";
                            }
                        }
                        else
                        {
                            startTimer = true;
                            informationToPlayer.text = "You don't have enough coins";
                        }
                    }
                    else
                    {
                        if (coins.amount >= slots[i].shopItem.itemCost)
                        {
                            startTimer = true;
                            informationToPlayer.text = "Bought item: " + slots[i].shopItem.itemName;
                            playerInventory.RemoveCoin(coins, "Coin", slots[i].shopItem.itemCost);
                            playerInventory.AddItemFromShop(slots[i].shopItem);
                        }
                        else
                        {
                            startTimer = true;
                            informationToPlayer.text = "You don't have enough coins";
                        }
                    }
                }
                else
                {
                    startTimer = true;
                    informationToPlayer.text = "You don't have coins";
                }
                slots[i].isBuying = false;
            }
        }
        
    }
}