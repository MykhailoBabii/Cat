using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class ClearPlayerPrefs 
{
    [MenuItem("Tools/Clear Player Prefs")]
    public static void ClearPlayersPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
