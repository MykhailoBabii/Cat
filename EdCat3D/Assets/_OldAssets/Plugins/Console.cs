using UnityEngine;
using System.Collections;

public class Console : MonoBehaviour {
    private bool consoleishidden;
    private string output;
    private string stack;
    private Vector2 scroll;

        void Start () {
       
        }
        void Update () {
              Application.RegisterLogCallback(HandleLog);
        if (Input.GetKeyDown("`") && Input.GetKey("left ctrl"))
        {
            ShowHideConsole();
        }
        }
    void OnGUI()
    {
        GUI.depth = -10000;
        if (consoleishidden)
        {
            ShowConsole();
        }
    }
    void ShowHideConsole()
    {
        if (consoleishidden)
        {
            consoleishidden = false;
        }
        else
        {
            consoleishidden = true;
        }
    }
    void ShowConsole()
    {
		GUI.Box(new Rect(-10, -10, Screen.width + 10, Screen.height + 10), "");
        GUILayout.BeginArea(new Rect(0,0,Screen.width,Screen.height/2));
		GUI.Box(new Rect(-10, -10, Screen.width + 10, Screen.height/2 + 10), "");
		GUI.Box(new Rect(-10, -10, Screen.width + 10, Screen.height/2 + 10), "");
		GUI.Box(new Rect(-10, -10, Screen.width + 10, Screen.height/2 + 10), "");
		GUI.Box(new Rect(-10, -10, Screen.width + 10, Screen.height/2 + 10), "");
		GUI.Box(new Rect(-10, -10, Screen.width + 10, Screen.height/2 + 10), "");
        scroll = GUILayout.BeginScrollView(scroll);
        GUILayout.Label(output);
        GUILayout.EndScrollView();
        GUILayout.EndArea();
    }

    void OnEnable()
    {
        Application.RegisterLogCallback(HandleLog);
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        output +=type+": " + logString+"\n";
        stack += stackTrace;
        scroll.y = 10000000000;
    }
}
 