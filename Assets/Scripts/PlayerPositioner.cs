using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class PlayerPositioner : MonoBehaviour
{
    public static PlayerPositioner Instance { get; private set; }

    public Transform[] entryPoints; // Assign these in the Unity Editor
    private SceneTransitionManager sceneTransitionManager;

     private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        sceneTransitionManager = GameObject.Find("Scene Transition Manager").GetComponent<SceneTransitionManager>();

    }

    public void GetSceneSpawnPointAndPositionPlayer() {
        // Get the entry points for the current scene based on name
        entryPoints = GameObject.FindGameObjectsWithTag("SpawnPoint")
                                  .Select(go => go.transform)
                                  .ToArray();   
            

        string transitionPointId = sceneTransitionManager.GetLastTransitionPoint();

        Debug.Log("getLastTransitionPoint: " + transitionPointId);
        Transform entryPoint = FindEntryPointById(transitionPointId);
        if (entryPoint != null)
        {
            transform.position = entryPoint.position;
        }
    }

    private Transform FindEntryPointById(string id)
    {
        foreach (Transform entryPoint in entryPoints)
        {
            if (entryPoint.name.Contains(id)) // Assuming entryPoint GameObjects are named with their IDs
            {
                return entryPoint;
            }
        }
        Debug.LogWarning("FindEntryPointById point not found: " + id);
        return null;
    }
}