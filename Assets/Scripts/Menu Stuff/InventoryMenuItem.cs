using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryMenuItem : MonoBehaviour
{

    public Item item;
    public int count;
    public bool isShopItem;

    public void Setup(Item newItem, int newCount, bool newIsShopItem)
    {
        item = newItem;
        count = newCount;
        isShopItem = newIsShopItem;
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
        if (isShopItem)
        {
            return $"{item.itemName} x {count} ({item.buyPrice} gold)";
        }
        return $"{item.itemName} x {count} ({item.sellPrice} gold)";
    }

    public void OnInventoryItemClicked()
    {
        if (isShopItem)
        {
            MenuController.Instance.BuyItem(item);
        }
        else
        {
            MenuController.Instance.SellItem(item);
        }
    }
}
