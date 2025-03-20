using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.EventSystems;

public class EventSystemAdder : MonoBehaviour
{
    [MenuItem("Tools/Add Event System to All Scenes")]
    public static void AddEventSystemToAllScenes()
    {
        string[] scenePaths = AssetDatabase.FindAssets("t:Scene");
        foreach (string scenePath in scenePaths)
        {
            string path = AssetDatabase.GUIDToAssetPath(scenePath);
            EditorSceneManager.OpenScene(path);

            if (GameObject.FindFirstObjectByType<EventSystem>() == null)
            {
                GameObject eventSystemPrefab = Resources.Load<GameObject>("EventSystem"); // Make sure your prefab is in Resources folder
                Instantiate(eventSystemPrefab);
            }

            EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
        }
    }
}