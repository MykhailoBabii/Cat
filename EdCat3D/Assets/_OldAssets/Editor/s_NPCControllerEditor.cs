using UnityEngine;
using UnityEditor;
using System.Collections;

[System.Serializable]
[UnityEditor.CustomEditor(typeof(s_NPCController))]
public class s_NPCControllerEditor : Editor
{
    public virtual void OnEnable()
    {
        s_NPCController s_NPCControllerScript = (s_NPCController)this.target;
        if (s_NPCControllerScript.dlgCount == 0)
        {
            s_NPCControllerScript.dlgCount = 1;
        }
        if ((s_NPCControllerScript.dlgTitle == null) || (s_NPCControllerScript.dlgTitle.Length != s_NPCControllerScript.dlgCount))
        {
            s_NPCControllerScript.dlgTitle = new string[s_NPCControllerScript.dlgCount];
            s_NPCControllerScript.dlgIsActive = new bool[s_NPCControllerScript.dlgCount];
            EditorUtility.SetDirty(s_NPCControllerScript);
        }
    }

    public override void OnInspectorGUI()
    {
        s_NPCController s_NPCControllerScript = (s_NPCController)this.target;
        GUILayout.Label("NPC options", "BoldLabel", new GUILayoutOption[] {});
        s_NPCControllerScript.npcwho = EditorGUILayout.TextField("NPC type", s_NPCControllerScript.npcwho, new GUILayoutOption[] {});
        s_NPCControllerScript.creatureCanSpeak = EditorGUILayout.Toggle("NPC can speak?", s_NPCControllerScript.creatureCanSpeak, new GUILayoutOption[] {});
        GUILayout.Label("Dialogues", "BoldLabel", new GUILayoutOption[] {});
        int i = 0;
        while (i < s_NPCControllerScript.dlgCount)
        {
            GUILayout.Label(string.Format("Dialogue{0}", i), "BoldLabel", new GUILayoutOption[] {});
            s_NPCControllerScript.dlgTitle[i] = EditorGUILayout.TextField("Dialogue Title", s_NPCControllerScript.dlgTitle[i], new GUILayoutOption[] {});
            s_NPCControllerScript.dlgIsActive[i] = EditorGUILayout.Toggle("Dialogue is active?", s_NPCControllerScript.dlgIsActive[i], new GUILayoutOption[] {});
            i++;
        }
        if (GUILayout.Button("Add Dialogue", new GUILayoutOption[] {}))
        {
            this.AddDialogue();
        }
        if (GUILayout.Button("Remove Dialogue", new GUILayoutOption[] {}))
        {
        }
    }

    private void AddDialogue()
    {
        s_NPCController s_NPCControllerScript = (s_NPCController)this.target;
        int dlgCount = s_NPCControllerScript.dlgCount;
        string[] dlgTitle = new string[dlgCount + 1];
        bool[] dlgIsActive = new bool[dlgCount + 1];
        int i = 0;
        while (i < dlgCount)
        {
            dlgTitle[i] = s_NPCControllerScript.dlgTitle[i];
            dlgIsActive[i] = s_NPCControllerScript.dlgIsActive[i];
            i++;
        }
        s_NPCControllerScript.dlgTitle = dlgTitle;
        s_NPCControllerScript.dlgIsActive = dlgIsActive;
        s_NPCControllerScript.dlgCount = s_NPCControllerScript.dlgCount + 1;
        EditorUtility.SetDirty(s_NPCControllerScript);
    }

}