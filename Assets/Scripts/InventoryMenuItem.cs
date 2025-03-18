using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryMenuItem : MonoBehaviour
{

    public Item item;
    public int count;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetDisplayText();

        Button inventoryButton = GameObject.Find("Inventory Menu Item").GetComponent<Button>(); // Replace with the actual button name
        if (inventoryButton != null)
        {
            inventoryButton.onClick.AddListener(OnInventoryItemClicked);
        }
        else
        {
            Debug.LogError("Inventory button not found.");
        }
    }

    string GetDisplayText()
    {
        return $"{item.itemName} x {count}";
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

    private void OnInventoryItemClicked()
    {
        Debug.Log("Inventory Menu Item clicked!");
    }
}
