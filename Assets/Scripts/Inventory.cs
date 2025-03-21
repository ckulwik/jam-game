using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Linq;

// the player's inventory
public class Inventory : MonoBehaviour
{
    // Dictionary to hold items and their counts
    public Dictionary<int, (Item item, int count)> items = new Dictionary<int, (Item, int)>();

    public int money = 100;

    public void AddItem(Item item)
    {
        if (items.ContainsKey(item.id))
        {
            // Increment the count if the item already exists
            items[item.id] = (items[item.id].item, items[item.id].count + 1);
        }
        else
        {
            // Add the new item with a count of 1
            items.Add(item.id, (item, 1));
        }
        Debug.Log($"Added item: {item.itemName} with ID: {item.id} to the inventory.");
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
            Debug.Log($"Removed item: {item.itemName} with ID: {item.id} from the inventory.");
            return true;
        }
        Debug.Log($"Item with ID: {item.id} not found in inventory.");
        return false;
    }

    public void SellItem(Item item)
    {
        bool removeSuccess = RemoveItem(item);
        if (removeSuccess)
        {
            money += item.sellPrice;
        }
    }

    public void BuyItem(Item item)
    {
        if (money < item.buyPrice)
        {
            Debug.Log("Not enough money to buy this item.");
            return;
        }
        AddItem(item);
        money -= item.buyPrice;
    }

    public string GetDisplayInventoryText()
    {
        string display = "Inventory Items:\n";

        // Build the display string using item ID as the key
        foreach (var kvp in items)
        {
            int itemId = kvp.Key;
            var (item, count) = kvp.Value; // Deconstruct the tuple
            display += $"{count}x {item.itemName}: {item.description} (Value: {item.sellPrice * count})\n"; // Use the item's properties
        }

        Debug.Log(display);
        return display;
    }

}