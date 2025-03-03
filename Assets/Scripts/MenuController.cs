using UnityEngine;
using UnityEngine.UI; // Include this for UI components
using System.Collections.Generic; // Include this for List<T>
using TMPro;

public class MenuController : MonoBehaviour
{
    public GameObject menuPanel; // Assign the Panel GameObject in the Inspector
    public TextMeshProUGUI inventoryText; // Reference to the Text component for displaying inventory items
    private Inventory inventory; // Reference to the player's inventory
    private bool isMenuOpen = false;

    private void Awake()
    {
        // Ensure this GameObject is not destroyed when loading a new scene
        DontDestroyOnLoad(gameObject);
        inventory = FindObjectOfType<Inventory>(); // Get the player's Inventory component
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
            UpdateInventoryDisplay(); // Update the inventory display when the menu is opened
        }
    }

    void UpdateInventoryDisplay()
    {
        if (inventory != null)
        {
            inventoryText.text = inventory.GetDisplayInventoryText();
        }
        // {
        //     List<Item> items = inventory.GetItems(); // Get the current items from the inventory
        //     inventoryText.text = ""; // Clear previous text

        //     foreach (Item item in items)
        //     {
        //         inventoryText.text += item.ToString() + "\n"; // Append each item's string representation
        //     }
        // }
    }
}