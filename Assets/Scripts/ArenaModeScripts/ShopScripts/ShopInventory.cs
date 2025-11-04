using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ShopItems;


public class ShopInventoryList<T>
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

public class ShopInventory : MonoBehaviour
{
    [SerializeField] private UI_Shop uiShop;

    public ShopInventoryList<ShopItems> shopItemList = new ShopInventoryList<ShopItems>();

    public void AddItem(ShopItems item)
    {
        shopItemList.Add(item);
        uiShop.UpdateShopSlotUI();
    }

    public void AddItemManually(string itemName, ShopItemType itemType, int cost, Sprite itemSprite, Vector3 itemScale)
    {
        ShopItems newItem = new ShopItems
        {
            itemName = itemName,
            itemType = itemType,
            itemCost = cost,
            icon = itemSprite,
            scale = itemScale
        };

        AddItem(newItem);
    }

    public ShopInventoryList<ShopItems> GetItemList()
    {
        return shopItemList;
    }
}