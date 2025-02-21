using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositioner : MonoBehaviour
{
    public Transform[] entryPoints; // Assign these in the Unity Editor
    private SceneTransitionManager sceneTransitionManager;

    private void Start()
    {
        sceneTransitionManager = GameObject.Find("Scene Transition Manager").GetComponent<SceneTransitionManager>();
       
        string exitPointId = sceneTransitionManager.GetLastExitPoint();
        Transform entryPoint = FindEntryPointById(exitPointId);
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