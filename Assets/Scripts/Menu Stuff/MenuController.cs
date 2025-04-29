using UnityEngine;
using UnityEngine.UI; // Include this for UI components
using System.Collections.Generic; // Include this for List<T>
using TMPro;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour
{
    public static MenuController Instance { get; private set; } // Singleton instance

    private Inventory playerInventory; // Reference to the player's inventory
    private bool isPlayerMenuOpen = false;

    public GameObject playerMenuPanel; // Assign the Panel GameObject in the Inspector
    public TextMeshProUGUI inventoryText; 
    public TextMeshProUGUI moneyText; 

    public GameObject shopMenuPanel; // Assign the Panel GameObject in the Inspector
    public GameObject shopInventoryContainer; 
    public GameObject playerShopInventoryContainer;
    public TextMeshProUGUI shopMoneyText; 
    private Shop shopInventory; // Reference to the shop's inventory
    private bool isShopMenuOpen = false;
    public bool canOpenShop = false;
    public GameObject inventoryMenuItemPrefab; // Assign the prefab in the Inspector

    public GameObject inventoryPanel;
    public GameObject collectionsPanel;
    public GameObject storyPanel;
    public GameObject settingsPanel;

    public Button firstMenuButton;

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
        playerInventory = FindAnyObjectByType<Inventory>();
        SetShop();
    }

    public void SetShop() {
        shopInventory = FindAnyObjectByType<Shop>();
    }

    void Update()
    {
        // Check for key press to toggle the menu
        if (Input.GetKeyDown(KeyCode.E)) // Change KeyCode.M to your desired key
        {
            // when the menu is opened, click the first button
            EventSystem.current.SetSelectedGameObject(firstMenuButton.gameObject);
            firstMenuButton.onClick.Invoke();

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
            UpdateShopMoneyDisplay();
        }
    }

    void UpdatePlayerInventoryDisplay()
    {
        if (playerInventory != null)
        {
            inventoryText.text = playerInventory.GetDisplayInventoryText();
        }
        else
        {
            Debug.LogError("Player playerInventory is null.");
        }
    }

    void UpdateMoneyDisplay()
    {
        if (playerInventory != null)
        {
            moneyText.text = $"Money: ${playerInventory.money}";
        }
    }

    void UpdateShopMoneyDisplay()
    {
        if (playerInventory != null)
        {
            shopMoneyText.text = $"Money: ${playerInventory.money}";
        }
    }

    public void RenderShopMenu()
    {
        DestroyShopMenu();
        RenderShopInventory();
        RenderPlayerInventory();
    }

    public void RenderShopInventory(){
        float verticalSpacing = 50f; // Adjust this value for desired spacing
        float currentYPosition = 0f; // Start position for the first item
    
        foreach (var (id, (item, count)) in shopInventory.items)
        {
            var shopMenuItemObject = Instantiate(inventoryMenuItemPrefab);
            var shopMenuItem = shopMenuItemObject.GetComponent<InventoryMenuItem>();
            shopMenuItem.Setup(item, count, true);
    
            // Set the parent to shopInventoryContainer and adjust the local position
            shopMenuItemObject.transform.SetParent(shopInventoryContainer.transform);
            shopMenuItemObject.transform.localPosition = new Vector3(0, currentYPosition, 0); // Set position with spacing

            currentYPosition -= verticalSpacing; // Decrease Y position for the next item
        }
    }

    public void RenderPlayerInventory(){
        float verticalSpacing = 50f; // Adjust this value for desired spacing
        float currentYPosition = 0f; // Start position for the first item
    
        foreach (var (id, (item, count)) in playerInventory.items)
        {
            var playerMenuItemObject = Instantiate(inventoryMenuItemPrefab);
            var playerMenuItem = playerMenuItemObject.GetComponent<InventoryMenuItem>();
            playerMenuItem.Setup(item, count, false);
    
            // Set the parent to playerShopInventoryContainer and adjust the local position
            playerMenuItemObject.transform.SetParent(playerShopInventoryContainer.transform);
            playerMenuItemObject.transform.localPosition = new Vector3(0, currentYPosition, 0); // Set position with spacing
    
            currentYPosition -= verticalSpacing; // Decrease Y position for the next item
        }
    }
  

    void DestroyShopMenu()
    {
        foreach (Transform child in shopInventoryContainer.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in playerShopInventoryContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void BuyItem(Item item)
    {
        Debug.Log("Buying item with ID: " + item.id);
        shopInventory.BuyItem(item, playerInventory.money);
        playerInventory.BuyItem(item);
        
        RenderShopMenu();
        UpdateShopMoneyDisplay();
    }

    public void SellItem(Item item)
    {
        Debug.Log("Selling item with ID: " + item.id);
        playerInventory.SellItem(item);
        // should a sold item be added to the shop inventory? player could then buy back if 
        // a mistake was made
        // shopInventory.SellItem(item);
    
        RenderShopMenu();
        UpdateShopMoneyDisplay();
    }

    private void ShowOnlyPanel(GameObject panelToShow)
    {
        inventoryPanel.SetActive(false);
        collectionsPanel.SetActive(false);
        storyPanel.SetActive(false);
        settingsPanel.SetActive(false);

        panelToShow.SetActive(true);
    }

    public void InventoryButtonClicked() {
        ShowOnlyPanel(inventoryPanel);
    }
    public void CollectionsButtonClicked() {
        ShowOnlyPanel(collectionsPanel);
    }
    public void StoryButtonClicked() {
        ShowOnlyPanel(storyPanel);
    }
    public void SettingsButtonClicked() {
        ShowOnlyPanel(settingsPanel);
    }
}