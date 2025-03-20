using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryMenuItem : MonoBehaviour
{

    public Item item;
    public int count;

    public void Setup(Item newItem, int newCount)
    {
        item = newItem;
        count = newCount;
        SetDisplayText(); // Update the display text with the new item and count
    }

    void SetDisplayText()
    {
        if (transform.Find("Display Text") == null)
        {
            Debug.LogError("TextMeshProUGUI component not found.");
            return;
        }
        transform.Find("Display Text").GetComponent<TextMeshProUGUI>().text = GetDisplayText();
    }

    string GetDisplayText()
    {
        return $"{item.itemName} x {count}";
    }

    public void OnInventoryItemClicked()
    {
        Debug.Log($"{item.itemName} clicked!");
    }
}
