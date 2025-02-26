using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject menuPanel; // Assign the Panel GameObject in the Inspector
    private bool isMenuOpen = false;

    private void Awake()
    {
        // Ensure this GameObject is not destroyed when loading a new scene
        DontDestroyOnLoad(gameObject);
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
    }
}