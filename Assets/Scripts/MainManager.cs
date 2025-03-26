using UnityEngine;
using System.IO;


public class MainManager : MonoBehaviour
{
    public static MainManager Instance { get; private set; }

    void Awake()
    {
        Load();
          // Check if an instance already exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy the new instance
            return; // Exit to prevent further execution
        }

        Instance = this; // Set the singleton instance

        // Ensure this GameObject is not destroyed when loading a new scene
        DontDestroyOnLoad(gameObject);
    }

    public void Save()
    {
        SaveData saveData = new SaveData();
        saveData.money = Inventory.Instance.money;
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(Application.persistentDataPath + "/save.json", json);
    }

    public void Load()
    {
        string json = File.ReadAllText(Application.persistentDataPath + "/save.json");
        SaveData saveData = JsonUtility.FromJson<SaveData>(json);
        Inventory.Instance.money = saveData.money;
        Debug.Log($"Loaded money: {Inventory.Instance.money}");
    }
    
}

class SaveData
{
    public int money;
    // public string inventory;
}

