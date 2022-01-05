using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    public event EventHandler OnItemListChanged;
    public List<Item> itemList;

    public Inventory()
    {
        itemList = new List<Item>();
    }

    public Inventory(Inventory other)
    {
        itemList = new List<Item>();
        foreach(Item item in other.itemList)
        {
            AddItem(new Item(item));
        }
        // hack to not have changed callbacks copy over (by not copying this over in the copy ctor)
        //OnItemListChanged = other.OnItemListChanged;
    }

    public bool InventoryHasSpace()
    {
        int numItemsNotEquipped = 0;
        foreach(Item item in itemList)
        {
            if(!item.Equipped)
            {
                numItemsNotEquipped++;
            }
        }
        return numItemsNotEquipped < 28;
    }

    public void AddItem(Item item)
    {
        if (item.isStackable())
        {
            bool itemAlreadyInInventory = false;
            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount += item.amount;
                    itemAlreadyInInventory = true;
                }
            }
            if (!itemAlreadyInInventory)
            {
                itemList.Add(item);
            }
        }
        else
        {
            itemList.Add(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }
    public void RemoveItem(Item item)
    {
        if (item.isStackable())
        {
            Item itemInInventory = null;
            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount -= item.amount;
                    itemInInventory = inventoryItem;
                }
            }
            if (itemInInventory != null && itemInInventory.amount <= 0)
            {
                itemList.Remove(itemInInventory);
            }
        }
        else
        {
            itemList.Remove(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void EquipItem(Item itemToEquip)
    {
        foreach(Item item in itemList)
        {
            if(item == itemToEquip)
            {
                item.Equipped = true;
                return;
            }
        }
    }

    public void UnEquipItem(Item itemToUnequip)
    {
        foreach (Item item in itemList)
        {
            if (item.Equipped)
            {
                if (item == itemToUnequip)
                {
                    item.Equipped = false;
                    return;
                }
            }
        }
    }
}
