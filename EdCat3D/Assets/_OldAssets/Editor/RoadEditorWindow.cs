using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

class RoadEditorWindow : EditorWindow {
	[MenuItem("N-Game/Road Editor")]
	public static void ShowRoadEditor() 
	{
		EditorWindow.GetWindow(typeof(RoadEditorWindow), true, "Road editor");
	}

	public void OnGUI()
	{
		#region roads
		Object[] roadObjs = GameObject.FindObjectsOfType(typeof(Road));

		EditorGUILayout.Separator();
		EditorGUILayout.BeginHorizontal();

		if (GUILayout.Button("Create New Road", GUILayout.ExpandWidth(false)))
		{

			GameObject newRoad = new GameObject();
			newRoad.name = "Road";
			Road r = newRoad.AddComponent<Road>();

			Undo.RegisterCreatedObjectUndo(newRoad, "Adding a new Road");

			Selection.activeObject = r.gameObject;
		}

		if (GUILayout.Button("Restore all roads", GUILayout.ExpandWidth(false)))
		{
			Undo.RegisterCompleteObjectUndo(roadObjs, "Roads retstoring");

			foreach (Road r in roadObjs)
			{
				r.UpdateMesh(true);
			}
		}

		EditorGUILayout.EndHorizontal();
		#endregion
	}
}