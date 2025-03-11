using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 1)]
public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public string description;
    public int buyPrice; 
    public int sellPrice;

    public Item(int id, string itemName, string description, int sellPrice, int buyPrice)
    {
        this.id = id;
        this.itemName = itemName;
        this.description = description;
        this.sellPrice = sellPrice;
        this.buyPrice = buyPrice;
    }
}