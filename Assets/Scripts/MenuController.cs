using UnityEngine;
using UnityEngine.UI; // Include this for UI components
using System.Collections.Generic; // Include this for List<T>
using TMPro;

public class MenuController : MonoBehaviour
{
    public static MenuController Instance { get; private set; } // Singleton instance

    private Inventory inventory; // Reference to the player's inventory
    private bool isPlayerMenuOpen = false;

    public GameObject playerMenuPanel; // Assign the Panel GameObject in the Inspector
    public TextMeshProUGUI inventoryText; 
    public TextMeshProUGUI moneyText; 

    public GameObject shopMenuPanel; // Assign the Panel GameObject in the Inspector
    public GameObject shopInventoryContainer; 
    public TextMeshProUGUI playerShopInventoryText; 
    public TextMeshProUGUI shopMoneyText; 
    private Shop shopInventory; // Reference to the shop's inventory
    private bool isShopMenuOpen = false;
    public bool canOpenShop = false;
    public GameObject inventoryMenuItemPrefab; // Assign the prefab in the Inspector

    private void Awake()
    {
        // Check if an instance already exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy the new instance
            return; // Exit to prevent further execution
        }

        Instance = this; // Set the singleton instance

        // Ensure this GameObject is not destroyed when loading a new scene
        DontDestroyOnLoad(gameObject);
        inventory = FindAnyObjectByType<Inventory>();
        SetShop();
    }

    private void OnBuyButtonClicked()
    {
        Debug.Log("Buy button clicked");
    }

    private void OnSellButtonClicked()
    {
        Debug.Log("Sell button clicked");
    }

    public void SetShop() {
        shopInventory = FindAnyObjectByType<Shop>();
    }

    void Update()
    {
        // Check for key press to toggle the menu
        if (Input.GetKeyDown(KeyCode.E)) // Change KeyCode.M to your desired key
        {
            TogglePlayerMenu();
        }

        if (Input.GetMouseButtonDown(1))
        {
            ToggleShopMenu();
        }

    }

    void TogglePlayerMenu()
    {
        // dont open shop menu when player menu is open
        if (isShopMenuOpen)
        {
            return;
        }

        isPlayerMenuOpen = !isPlayerMenuOpen;
        playerMenuPanel.SetActive(isPlayerMenuOpen);
        
        if (isPlayerMenuOpen)
        {
            UpdatePlayerInventoryDisplay(); 
            UpdateMoneyDisplay();
        }
    }

    void ToggleShopMenu()
    {
        // dont open player menu when shop menu is open
        if (isPlayerMenuOpen)
        {
            return;
        }

        if (!canOpenShop)
        {
            return;
        }

        isShopMenuOpen = !isShopMenuOpen;
        shopMenuPanel.SetActive(isShopMenuOpen);
        
        if (isShopMenuOpen)
        {
            RenderShopMenu();
            UpdatePlayerShopInventoryDisplay();
            UpdateShopMoneyDisplay();
        }
    }

    void UpdatePlayerInventoryDisplay()
    {
        if (inventory != null)
        {
            inventoryText.text = inventory.GetDisplayInventoryText();
        }
        else
        {
            Debug.LogError("Player inventory is null.");
        }
    }

    void UpdatePlayerShopInventoryDisplay()
    {
        if (inventory != null)
        {
            playerShopInventoryText.text = inventory.GetDisplayInventoryText();
        }
        else
        {
            Debug.LogError("Player inventory is null.");
        }
    }

    // void UpdateShopInventoryDisplay()
    // {
    //     if (shopInventory != null)
    //     {
    //         shopInventoryText.text = shopInventory.GetDisplayInventoryText();
    //     }
    //     else
    //     {
    //         Debug.LogError("Shop inventory is null.");
    //     }
    // }

    void UpdateMoneyDisplay()
    {
        if (inventory != null)
        {
            moneyText.text = $"Money: ${inventory.money}";
        }
    }

    void UpdateShopMoneyDisplay()
    {
        if (inventory != null)
        {
            shopMoneyText.text = $"Money: ${inventory.money}";
        }
    }

    public void RenderShopMenu()
    {
        DestroyShopMenu();
        float verticalSpacing = 50f; // Adjust this value for desired spacing
        float currentYPosition = 0f; // Start position for the first item
    
        foreach (var (id, (item, count)) in shopInventory.items)
        {
            var shopMenuItemObject = Instantiate(inventoryMenuItemPrefab);
            var shopMenuItem = shopMenuItemObject.GetComponent<InventoryMenuItem>();
            shopMenuItem.Setup(item, count);
    
            // Set the parent to shopInventoryContainer and adjust the local position
            shopMenuItemObject.transform.SetParent(shopInventoryContainer.transform);
            shopMenuItemObject.transform.localPosition = new Vector3(0, currentYPosition, 0); // Set position with spacing

            currentYPosition -= verticalSpacing; // Decrease Y position for the next item
        }
    }

    void DestroyShopMenu()
    {
        foreach (Transform child in shopInventoryContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void BuyItem(int itemId)
    {
        Debug.Log("Buying item with ID: " + itemId);
        // shopInventory.BuyItem(itemId);
        // UpdateShopInventoryDisplay();
        // UpdateMoneyDisplay();
    }

    public void SellItem(int itemId)
    {
        Debug.Log("Selling item with ID: " + itemId);
        // shopInventory.SellItem(itemId);
        // UpdateShopInventoryDisplay();
        // UpdateMoneyDisplay();
    }
}