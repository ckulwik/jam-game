using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName; // Name of the item
    public string description; // Description of the item
    public float value; // Value of the item in game currency
    public int id;

    public void Buy()
    {
        // Logic for buying the item
        Debug.Log($"You bought {itemName} for {value} currency.");
        // Implement additional logic for purchasing the item
    }

    public void Sell()
    {
        // Logic for selling the item
        Debug.Log($"You sold {itemName} for {value} currency.");
        // Implement additional logic for selling the item
    }
}