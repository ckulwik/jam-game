using UnityEngine;
using UnityEngine.UI; // Include this for UI components
using System.Collections.Generic; // Include this for List<T>
using TMPro;

public class MenuController : MonoBehaviour
{
    public static MenuController Instance { get; private set; } // Singleton instance

    public GameObject menuPanel; // Assign the Panel GameObject in the Inspector
    public TextMeshProUGUI inventoryText; 
    public TextMeshProUGUI moneyText; 
    
    private Inventory inventory; // Reference to the player's inventory
    private bool isMenuOpen = false;

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
        inventory = FindObjectOfType<Inventory>();
    }

    void Update()
    {
        // Check for key press to toggle the menu
        if (Input.GetKeyDown(KeyCode.M)) // Change KeyCode.M to your desired key
        {
            ToggleMenu();
        }
    }

    void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
        menuPanel.SetActive(isMenuOpen);
        
        if (isMenuOpen)
        {
            UpdateInventoryDisplay(); 
            UpdateMoneyDisplay();
        }
    }

    void UpdateInventoryDisplay()
    {
        if (inventory != null)
        {
            inventoryText.text = inventory.GetDisplayInventoryText();
        }
    }

    void UpdateMoneyDisplay()
    {
        if (inventory != null)
        {
            moneyText.text = $"Money: ${inventory.money}";
        }
    }
}