using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 1)]
public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public string description;
    public int buyPrice; 
    public int sellPrice; 
    
    
    private void OnEnable()
    {
        // Initialize default values
        id = 0;
        itemName = "Unnamed Item";
        description = "No description";
        buyPrice = 0;
        sellPrice = 0;
    }

    // public Item(int id, string itemName, string description, int sellPrice, int buyPrice)
    // {
    //     this.id = id;
    //     this.itemName = itemName;
    //     this.description = description;
    //     this.sellPrice = sellPrice;
    //     this.buyPrice = buyPrice;
    // }

    //  public Item(int id)
    // {
    //     Item sourceItem = ItemDatabase.Instance.GetItemById(id);
    //     if (sourceItem != null)
    //     {
    //         this.id = sourceItem.id;
    //         this.itemName = sourceItem.itemName;
    //         this.description = sourceItem.description;
    //         this.sellPrice = sourceItem.sellPrice;
    //         this.buyPrice = sourceItem.buyPrice;
    //     }
    //     else
    //     {
    //         Debug.LogError($"Failed to create Item with ID {id}: Item not found in database");
    //     }
    // }

    public static Item CreateItem(int id)
    {
        Item item = ScriptableObject.CreateInstance<Item>();
        Item sourceItem = ItemDatabase.Instance.GetItemById(id);
        if (sourceItem != null)
        {
            item.id = sourceItem.id;
            item.itemName = sourceItem.itemName;
            item.description = sourceItem.description;
            item.sellPrice = sourceItem.sellPrice;
            item.buyPrice = sourceItem.buyPrice;
        }
        else
        {
            Debug.LogError($"Failed to create Item with ID {id}: Item not found in database");
        }
        return item;
    }
}

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "ScriptableObjects/ItemDatabase", order = 2)]
public class ItemDatabase : ScriptableObject
{
    private static ItemDatabase _instance;
    public static ItemDatabase Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<ItemDatabase>("ItemDatabase");
                if (_instance == null)
                {
                    Debug.LogError("ItemDatabase not found in Resources folder!");
                }
            }
            return _instance;
        }
    }

    [SerializeField] private List<Item> itemDb = new List<Item>(); // Serialize this field

    private Dictionary<int, Item> itemById = new Dictionary<int, Item>(); // This will be our lookup dictionary

    private void OnEnable()
    {
        InitializeItems();
    }

    private void InitializeItems()
    {
        itemDb.Clear();
        itemById.Clear();

        Item testItem = ScriptableObject.CreateInstance<Item>();
        testItem.id = 0;
        testItem.itemName = "Test pickup Item";
        testItem.description = "A test item that can be picked up.";
        testItem.buyPrice = 10;
        testItem.sellPrice = 10;
        itemDb.Add(testItem);
        itemById[testItem.id] = testItem;

        Item healthPotion = ScriptableObject.CreateInstance<Item>();
        healthPotion.id = 1;
        healthPotion.itemName = "Health Potion";
        healthPotion.description = "A potion that restores 10 health.";
        healthPotion.buyPrice = 10;
        healthPotion.sellPrice = 15;
        itemDb.Add(healthPotion);
        itemById[healthPotion.id] = healthPotion;

        Item manaPotion = ScriptableObject.CreateInstance<Item>();
        manaPotion.id = 2;
        manaPotion.itemName = "Mana Potion";
        manaPotion.description = "A potion that restores 10 mana.";
        manaPotion.buyPrice = 5;
        manaPotion.sellPrice = 10;
        itemDb.Add(manaPotion);
        itemById[manaPotion.id] = manaPotion;
    }

    public Item GetItemById(int id)
    {
        Debug.Log("items in itemdb: " + itemDb.Count);
        if (itemById.TryGetValue(id, out Item item))
        {
            return item;
        }
        Debug.LogWarning($"Item with ID {id} not found in database");
        return null;
    }
}