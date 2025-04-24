using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public Dictionary<int, (Item item, int count)> items = new Dictionary<int, (Item, int)>();

    public void BuyItem(Item item, int playersMoney)
    {
        if (playersMoney < item.buyPrice)
        {
            Debug.Log("Not enough money to buy this item.");
            return;
        }
        RemoveItem(item);
    }

    public bool RemoveItem(Item item)
    {
        if (items.ContainsKey(item.id))
        {
            var currentItem = items[item.id];
            if (currentItem.count > 1)
            {
                // Decrement the count if more than one exists
                items[item.id] = (currentItem.item, currentItem.count - 1);
            }
            else
            {
                // Remove the item if count is 1
                items.Remove(item.id);
            }
            Debug.Log($"Removed item: {item.itemName} with ID: {item.id} from the shop inventory.");
            return true;
        }
        Debug.Log($"Item with ID: {item.id} not found in shop inventory.");
        return false;
    }

    public void SellItem(Item item)
    {
       
    }

    public string GetDisplayInventoryText()
    {
        string display = "Shop Items:\n";

        // Build the display string using item ID as the key
        foreach (var kvp in items)
        {
            int itemId = kvp.Key;
            var (item, count) = kvp.Value; // Deconstruct the tuple
            display += $"{count}x {item.itemName}: {item.description} (Value: {item.sellPrice * count})\n"; // Use the item's properties
        }

        return display;
    }

    private void Start()
    {

        Item itemToAdd = ScriptableObject.CreateInstance<Item>();
        itemToAdd.id = 3;
        itemToAdd.itemName = "Health Potion";
        itemToAdd.description = "A potion that restores 10 health.";
        itemToAdd.sellPrice = 10;
        itemToAdd.buyPrice = 15;
        items.Add(3, (itemToAdd, 5));
        
        itemToAdd = ScriptableObject.CreateInstance<Item>();
        itemToAdd.id = 4;
        itemToAdd.itemName = "Mana Potion";
        itemToAdd.description = "A potion that restores 10 mana.";
        itemToAdd.sellPrice = 5;
        itemToAdd.buyPrice = 10;
        items.Add(4, (itemToAdd, 8));
    }
}
