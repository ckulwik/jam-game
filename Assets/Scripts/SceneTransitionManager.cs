using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance;
    [SerializeField] string lastTransitionPoint;
    private Scene scene;


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
        // It is save to remove listeners even if they
        // didn't exist so far.
        // This makes sure it is added only once
        SceneManager.sceneLoaded -= OnSceneLoaded;

        // Add the listener to be called when a scene is loaded
        SceneManager.sceneLoaded += OnSceneLoaded;

        DontDestroyOnLoad(gameObject);

        // Store the creating scene as the scene to trigger start
        scene = SceneManager.GetActiveScene();

    }

    private void OnDestroy()
    {
        // Always clean up your listeners when not needed anymore
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Listener for sceneLoaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // return if not the start calling scene
        if(!string.Equals(scene.path, this.scene.path)) return;

        PlayerPositioner.Instance.GetSceneSpawnPointAndPositionPlayer();
    }

    public void SetLastTransitionPoint(string transitionPointId)
    {
        Debug.Log("SetLastTransitionPoint: " + transitionPointId);
        lastTransitionPoint = transitionPointId;
    }

    public string GetLastTransitionPoint()
    {
        return lastTransitionPoint;
    }
}