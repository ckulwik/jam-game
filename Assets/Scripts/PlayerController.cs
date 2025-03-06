using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; } // Singleton instance

    [SerializeField] float speed = 3.0f;
    private Inventory inventory;

    private void Awake()
    {
        // Check if an instance already exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy the new instance
            return; // Exit to prevent further execution
        }

        Instance = this; // Set the singleton instance
        DontDestroyOnLoad(gameObject); // Ensure this GameObject is not destroyed when loading a new scene
        inventory = GetComponent<Inventory>(); // Get the player's Inventory component
    }

    // Update is called once per frame
    void Update()
    {
        float horizInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizInput * speed * Time.deltaTime);

        float vertInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * vertInput * speed * Time.deltaTime);
    }

    // for picking up items and adding them to the inventory
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            Item item = collision.gameObject.GetComponent<Item>(); // Get the Item component
            if (item != null)
            {
                inventory.AddItem(item); // Add the item to the inventory
                Destroy(collision.gameObject); // destroy the item from the scene
            }
        }
    }
}
