using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneDoor : MonoBehaviour
{
    [SerializeField] private string sceneToLoad = "Outside";  // Set this in the Inspector
    private SceneTransitionManager sceneTransitionManager;
    void Start()
    {
        sceneTransitionManager = GameObject.Find("Scene Transition Manager").GetComponent<SceneTransitionManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (sceneTransitionManager)
            {
                sceneTransitionManager.SetLastTransitionPoint(transform.name);
            }
            else
            {
                Debug.LogWarning("SceneTransitionManager not found in the scene!");
            }
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
