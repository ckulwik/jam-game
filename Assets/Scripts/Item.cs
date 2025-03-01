using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName; // Name of the item
    public string description; // Description of the item
    public float value; // Value of the item in game currency


    public void PickUp()
    {
        // Logic for picking up the item
        Debug.Log($"{itemName} has been picked up.");
        // Implement additional logic, such as adding the item to the player's inventory
    }

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

    public override string ToString()
    {
        return $"{itemName}: {description} (Value: {value})";
    }
}