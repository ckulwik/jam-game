using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Item> items; // List to store the items in the inventory

    void Start()
    {
        items = new List<Item>(); // Initialize the inventory
    }

    public void AddItem(Item item)
    {
        items.Add(item);
        Debug.Log($"{item.itemName} has been added to the inventory.");
    }

    public void RemoveItem(Item item)
    {
        if (items.Remove(item))
        {
            Debug.Log($"{item.itemName} has been removed from the inventory.");
        }
        else
        {
            Debug.Log($"{item.itemName} is not in the inventory.");
        }
    }

    public void DisplayInventory()
    {
        Debug.Log("Inventory Items:");
        foreach (Item item in items)
        {
            Debug.Log(item.ToString());
        }
    }
}