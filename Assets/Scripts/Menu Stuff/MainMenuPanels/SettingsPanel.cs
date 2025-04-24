using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SettingsPanel : MonoBehaviour
{
    // void Save() {}
    // void Load() {}
    public void Quit() {
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #else
        Application.Quit();
        #endif
    }
}
