using UnityEngine;

public class ShopCounter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player entered shop counter.");
        if (other.CompareTag("Player")) // Ensure the player has the "Player" tag
        {
            // Find the MenuController in the scene
            MenuController menuController = FindAnyObjectByType<MenuController>();
            if (menuController != null)
            {
                menuController.canOpenShop = true; // Allow the player to open the shop
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Player exited shop counter.");
        if (other.CompareTag("Player"))
        {
            // Find the MenuController in the scene
            MenuController menuController = FindAnyObjectByType<MenuController>();
            if (menuController != null)
            {
                menuController.canOpenShop = false; // Disallow the player from opening the shop
            }
        }
    }
}