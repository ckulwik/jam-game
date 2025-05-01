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

    [SerializeField] private List<Item> itemDb = new List<Item>();

    private Dictionary<int, Item> itemById = new Dictionary<int, Item>();

    private void OnEnable()
    {
        itemById.Clear();
        foreach (var item in itemDb)
        {
            itemById[item.id] = item;
        }
    }

    public Item GetItemById(int id)
    {
        if (itemById.TryGetValue(id, out Item item))
        {
            return item;
        }
        return null;
    }
}