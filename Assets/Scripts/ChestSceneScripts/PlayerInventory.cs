using UnityEngine;
using System;
using System.Collections.Generic;
using static Items;
using TMPro;


public class PlayerInventoryList<T>
{
    private class Node
    {
        public T Data;
        public Node Next;

        public Node(T data)
        {
            Data = data;
            Next = null;
        }
    }

    private Node head;

    public int Count { get; private set; }

    // Indexer to access elements with []
    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException();

            Node current = head;
            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }

            return current.Data;
        }
    }

    // Add a new item to the list
    public void Add(T data)
    {
        Node newNode = new Node(data);
        newNode.Next = head;
        head = newNode;
        Count++;
    }

    // Find an item in the list
    public T Find(Predicate<T> match)
    {
        Node current = head;
        while (current != null)
        {
            if (match(current.Data))
                return current.Data;
            current = current.Next;
        }
        return default(T);
    }

    // Remove an item from the list
    public bool Remove(T data)
    {
        if (head == null)
            return false;

        if (EqualityComparer<T>.Default.Equals(head.Data, data))
        {
            head = head.Next;
            Count--;
            return true;
        }

        Node current = head;
        while (current.Next != null)
        {
            if (EqualityComparer<T>.Default.Equals(current.Next.Data, data))
            {
                current.Next = current.Next.Next;
                Count--;
                return true;
            }
            current = current.Next;
        }
        return false;
    }
}


public class PlayerInventory : MonoBehaviour
{
    public GameObject player;
    public HealthPotion healAmount;
    public TextMeshProUGUI playerCoinsText;

    // Arrows
    public Shooting activeArrow;
    public GameObject silverArrowPrefab;
    public GameObject chestSilverArrowPrefab;
    public GameObject explosiveArrowPrefab;
    public GameObject chestExplosiveArrowPrefab;
    public GameObject arrowPrefab;
    public GameObject chestArrowPrefab;

    //Potions
    public GameObject potionPrefab;

    //Weapons
    public GameObject chestShortSword;
    public GameObject chestSpear;
    public GameObject chestScythe;
    public GameObject chestBigSword;
    public GameObject shortSword;
    public GameObject spear;
    public GameObject scythe;
    public GameObject bigSword;

    //Sprites
    public Sprite shortSwordSprite;
    public Sprite bigSwordSprite;
    public Sprite spearSprite;
    public Sprite scytheSprite;

    [SerializeField] private UI_Inventory uiInventory;

    public PlayerInventoryList<Items> itemList = new PlayerInventoryList<Items>();

    public void AddItem(Items item)
    {
        Items existingItem = itemList.Find(i => i.itemName == item.itemName && i.itemType == item.itemType);

        if (existingItem != null)
        {
            if (existingItem.stackable)
            {
                if (existingItem.amount < existingItem.maxStack)
                {
                    existingItem.amount += item.amount;

                    if (existingItem.itemType == Items.ItemType.Coin)
                    {
                        playerCoinsText.text = existingItem.amount.ToString();
                    }

                    uiInventory.UpdateSlotUI();
                }
            }
            else
            {
                itemList.Add(item);
                uiInventory.UpdateSlotUI();
            }
        }
        else
        {
            if (item.itemType == Items.ItemType.Coin)
            {
                playerCoinsText.text = item.amount.ToString();
            }

            itemList.Add(item);
            uiInventory.UpdateSlotUI();
        }
    }

    public void AddItemManually(string itemName, ItemType itemType, int amount, int maxStack, bool stackable, Sprite itemSprite, Vector3 itemScale)
    {
        Items newItem = new Items
        {
            itemName = itemName,
            itemType = itemType,
            amount = amount,
            maxStack = maxStack,
            stackable = stackable,
            icon = itemSprite,
            scale = itemScale
        };

        AddItem(newItem);
    }

    public void AddItemFromShop(ShopItems shopItem)
    {
        if (shopItem.itemType.ToString() == Items.ItemType.HealthPotion.ToString())
        {
            Items newItem = new Items
            {
                itemName = shopItem.itemName,
                itemType = (ItemType)Enum.Parse(typeof(ItemType), shopItem.itemType.ToString()),
                amount = 1,
                maxStack = 3,
                stackable = true,
                icon = shopItem.icon,
                scale = shopItem.scale
            };

            AddItem(newItem);
        }

        if (shopItem.itemType.ToString() == Items.ItemType.Arrow.ToString())
        {
            Items newItem = new Items
            {
                itemName = shopItem.itemName,
                itemType = (ItemType)Enum.Parse(typeof(ItemType), shopItem.itemType.ToString()),
                amount = 1,
                maxStack = 1,
                stackable = false,
                icon = shopItem.icon,
                scale = shopItem.scale
            };

            AddItem(newItem);
        }

        if (shopItem.itemType.ToString() == Items.ItemType.Weapon.ToString())
        {
            Items newItem = new Items
            {
                itemName = shopItem.itemName,
                itemType = (ItemType)Enum.Parse(typeof(ItemType), shopItem.itemType.ToString()),
                amount = 1,
                maxStack = 1,
                stackable = false,
                icon = shopItem.icon,
                scale = shopItem.scale
            };

            AddItem(newItem);
        }
    }

    public void RemoveItem(Items item)
    {
        Items existingItem = itemList.Find(i => i.itemName == item.itemName && i.itemType == item.itemType);

        if (existingItem != null)
        {
            if (existingItem.amount > 1)
            {
                existingItem.amount--;
                uiInventory.UpdateSlotUI();
            }
            else
            {
                itemList.Remove(existingItem);
                uiInventory.UpdateSlotUI();
            }
        }
        else
        {
            itemList.Remove(item);
            uiInventory.UpdateSlotUI();
        }
    }

    public void RemoveItemByName(string itemName)
    {
        Items itemToRemove = itemList.Find(item => item.itemName == itemName);

        if (itemToRemove != null)
        {
            RemoveItem(itemToRemove);
        }
        else
        {
            Debug.LogWarning("Item not found in the inventory: " + itemName);
        }
    }

    public void UseConsumable(Items item)
    {
        Health playerHealth = player.GetComponent<Health>();
        if (item.itemName == "BigPotion")
        {
            playerHealth.GiveHealthFromInventory(200, item);
        }
        else
        {

            playerHealth.GiveHealthFromInventory(healAmount.heal, item);
        }
    }

    public void RemoveCoin(Items item, string itemName, int cost)
    {
        Items existingItem = itemList.Find(i => i.itemName == item.itemName && i.itemType == item.itemType);

        if (existingItem != null)
        {
            if (existingItem.amount > 1)
            {
                existingItem.amount -= cost;

                playerCoinsText.text = existingItem.amount.ToString();

                if (existingItem.amount > 1)
                {
                    uiInventory.UpdateSlotUI();
                }
                else
                {
                    itemList.Remove(existingItem);
                    uiInventory.UpdateSlotUI();
                }
            }
            else
            {
                itemList.Remove(existingItem);
                uiInventory.UpdateSlotUI();
            }
        }
        else
        {
            itemList.Remove(item);
            uiInventory.UpdateSlotUI();
        }
    }

    public void EquipArrow(Items item)
    {
        GameObject newArrow;
        GameObject previousArrow;
        Vector3 previousScale = new Vector3(0.9f, 0.5f, 0);

        if (item.itemName == explosiveArrowPrefab.name)
        {
            previousArrow = activeArrow.bulletPrefab;
            newArrow = explosiveArrowPrefab;
            activeArrow.bulletPrefab = newArrow;

            previousScale = new Vector3(0.9f, 0.5f, 0);

            RemoveItemByName(explosiveArrowPrefab.name);
            AddItemManually(previousArrow.name, ItemType.Arrow, 1, 1, false, previousArrow.GetComponent<SpriteRenderer>().sprite, previousScale);
        }

        if (item.itemName == silverArrowPrefab.name)
        {
            previousArrow = activeArrow.bulletPrefab;
            newArrow = silverArrowPrefab;
            activeArrow.bulletPrefab = newArrow;

            RemoveItemByName(silverArrowPrefab.name);
            AddItemManually(previousArrow.name, ItemType.Arrow, 1, 1, false, previousArrow.GetComponent<SpriteRenderer>().sprite, previousScale);
        }

        if (item.itemName == arrowPrefab.name)
        {
            previousArrow = activeArrow.bulletPrefab;
            newArrow = arrowPrefab;
            activeArrow.bulletPrefab = newArrow;

            RemoveItemByName(arrowPrefab.name);
            AddItemManually(previousArrow.name, ItemType.Arrow, 1, 1, false, previousArrow.GetComponent<SpriteRenderer>().sprite, previousScale);
        }
    }

    public void EquipWeapon(Items item)
    {
        GameObject newWeapon;
        GameObject previousWeapon;
        Sprite previousSprite;
        Vector3 previousScale;

        if (shortSword.name == item.itemName)
        {
            newWeapon = shortSword;
        }
        else if (spear.name == item.itemName)
        {
            newWeapon = spear;
        }
        else if (scythe.name == item.itemName)
        {
            newWeapon = scythe;
        }
        else if (bigSword.name == item.itemName)
        {
            newWeapon = bigSword;
        }
        else
        {
            newWeapon = null;
        }

        if (shortSword.activeSelf == true)
        {
            previousWeapon = shortSword;
            previousSprite = shortSwordSprite;
            previousScale = new Vector3(0.5f, 1.1f, 0);
        }
        else if (spear.activeSelf == true)
        {
            previousWeapon = spear;
            previousSprite = spearSprite;
            previousScale = new Vector3(0.4f, 1.1f, 0);
        }
        else if (scythe.activeSelf == true)
        {
            previousWeapon = scythe;
            previousSprite = scytheSprite;
            previousScale = new Vector3(0.8f, 1.1f, 0);
        }
        else if (bigSword.activeSelf == true)
        {
            previousWeapon = bigSword;
            previousSprite = bigSwordSprite;
            previousScale = new Vector3(0.5f, 1.1f, 0);
        }
        else
        {
            previousWeapon = null;
            previousSprite = null;
            previousScale = new Vector3(0, 0, 0);
        }

        newWeapon.SetActive(true);
        previousWeapon.SetActive(false);
        RemoveItemByName(newWeapon.name);
        AddItemManually(previousWeapon.name, ItemType.Weapon, 1, 1, false, previousSprite, previousScale);
    }

    public void DropItem(Items item)
    {
        if(item.itemName == explosiveArrowPrefab.name)
        {
            Instantiate(chestExplosiveArrowPrefab, player.transform.position, Quaternion.identity);
            RemoveItem(item);
        }

        if (item.itemName == silverArrowPrefab.name)
        {
            Instantiate(chestSilverArrowPrefab, player.transform.position, Quaternion.identity);
            RemoveItem(item);
        }

        if (item.itemName == arrowPrefab.name)
        {
            Instantiate(chestArrowPrefab, player.transform.position, Quaternion.identity);
            RemoveItem(item);
        }

        if (item.itemName == potionPrefab.name)
        { 
            Instantiate(potionPrefab, player.transform.position, Quaternion.identity);
            RemoveItem(item);
        }

        if (item.itemName == shortSword.name)
        {
            Instantiate(chestShortSword, player.transform.position, Quaternion.identity);
            RemoveItem(item);
        }
        if (item.itemName == spear.name)
        {
            Instantiate(chestSpear, player.transform.position, Quaternion.identity);
            RemoveItem(item);
        }
        if (item.itemName == scythe.name)
        {
            Instantiate(chestScythe, player.transform.position, Quaternion.identity);
            RemoveItem(item);
        }

        if (item.itemName == bigSword.name)
        {
            Instantiate(chestBigSword, player.transform.position, Quaternion.identity);
            RemoveItem(item);
        }
    }

    public bool ExistsItem(string itemName)
    {
        Items foundItem = itemList.Find(item => item.itemName == itemName);

        if (foundItem != null)
        {
            Debug.Log("Item found: " + foundItem.itemName);
            return true;
        }
        else
        {
            Debug.Log("Item not found");
            return false;
        }
    }

    public Items FindItemByName(string itemName)
    {
        Items foundItem = itemList.Find(item => item.itemName == itemName);

        if (foundItem != null)
        {
            Debug.Log("Item found: " + foundItem.itemName);
            return foundItem;
        }
        else
        {
            Debug.Log("Item not found");
            return null;
        }
    }

    public PlayerInventoryList<Items> GetItemList()
    {
        return itemList;
    }
}
