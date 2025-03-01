using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 3.0f;
    private Inventory inventory;

    // Start is called before the first frame update;
    void Start()
    {
        inventory = GetComponent<Inventory>();
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
                Destroy(collision.gameObject); // Optionally destroy the item from the scene
            }
        }
    }
}