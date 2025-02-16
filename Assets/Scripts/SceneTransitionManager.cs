using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance;
    [SerializeField] string lastExitPoint;

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
    }

    public void SetLastExitPoint(string exitPointId)
    {
        Debug.Log("SetLastExitPoint: " + exitPointId);
        lastExitPoint = exitPointId;
    }

    public string GetLastExitPoint()
    {
        return lastExitPoint;
    }
}