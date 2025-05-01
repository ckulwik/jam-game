using UnityEngine;
using System.IO;


public class MainManager : MonoBehaviour
{
    public static MainManager Instance { get; private set; }

     void Awake()
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

        // This seems to always print the error log, but I don't see any related issues
        // if (Inventory.Instance == null)
        // {
        //     Debug.LogError("Inventory not found! Will not attempt to load");
        //     return;
        // }

        // Load is here for testing only
        // Load();
    }

    public void Save()
    {
        // Get the inventory state
        string inventoryJson = Inventory.Instance.SerializeInventory();
        
        // Create save data
        SaveData saveData = new SaveData();
        saveData.money = Inventory.Instance.money;
        saveData.inventory = inventoryJson;
        
        // Save to file
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(Application.persistentDataPath + "/save.json", json);
    }

    public void Load()
    {
        string savePath = Application.persistentDataPath + "/save.json";
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);
            
            // Load money
            Inventory.Instance.money = saveData.money;
            
            // Load inventory if it exists
            if (!string.IsNullOrEmpty(saveData.inventory))
            {
                Inventory.Instance.DeserializeInventory(saveData.inventory);
            }
        }
        else
        {
            Debug.Log("No save file found. Starting with default values.");
        }
    }
    
}

class SaveData
{
    public int money;
    public string inventory;
}

