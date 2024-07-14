using UnityEngine;
using UnityEditor;

public class ClearPlayerPrefs : EditorWindow
{
    [MenuItem("Tools/Clear PlayerPrefs")]
    public static void ShowWindow()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}