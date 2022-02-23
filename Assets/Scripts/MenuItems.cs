using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MenuItems : MonoBehaviour
{
    [MenuItem("Tool/Clear prefs")]
    private static void MenuItem()
    {
        Debug.Log("delete prefs");
        PlayerPrefs.DeleteAll();
    }
}
