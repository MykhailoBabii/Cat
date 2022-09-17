using System.IO;
using UnityEngine;
using UnityEditor;
using System.Collections;
using BO.Common;


[CustomEditor(typeof(Road))]
public class RoadEditor : Editor
{
    private Vector3? _position = new Vector3();
    private int selectedNode = -1;
    private bool HandleType;
	private LayerMask layerMaskHeroMoveSquare = 8;
	private GameObject goTerrain;

	void OnEnable()
	{
		Debug.Log ("ON ENABLE");
		goTerrain = GameObject.Find ("Terrain");
	}

    public void OnSceneGUI()
    {
        Road r = target as Road;
        Handles.matrix = r.transform.localToWorldMatrix;

        #region Controls
        if (Event.current.type == EventType.MouseDown)	// mouse click
        {
            if ((Event.current.button == 1) && (!Event.current.alt))	// right button
            {
                Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
				RaycastHit hit = new RaycastHit();
				Vector3 hitPoint = new Vector3();
                bool succ = true;
                //Vector3 hitPoint = Terrain.activeTerrain.Raycast(ray, ref succ);
				//Vector3 hitPoint = Physics.Raycast(ray, out hit);
				Debug.Log(layerMaskHeroMoveSquare.value);
				//if (Physics.Raycast(ray, out hit, 100, layerMaskHeroMoveSquare.value))
				if (goTerrain.GetComponent<Collider>().Raycast (ray, out hit, Mathf.Infinity))
				{
					hitPoint = hit.point;
					Debug.Log("Create Node click");
				}
				Debug.Log(hit.point.x + ", " + hit.point.y + ", " + hit.point.z);
                if (succ)
                {
                    // add new node
                    Undo.RecordObject(target, "adding new road node");

                    RoadNode newNode = new RoadNode();
                    newNode.pos = r.transform.InverseTransformPoint(hitPoint);
                    r.nodeList.Add(newNode);

                    r.UpdateMesh(false);
                }
            }

            if (Event.current.button == 0)	// left button 
            {
                // "select" the closest node
                float minDist = float.PositiveInfinity;
                int closestNode = -1;
                for (int i = 0; i < r.nodeList.Count; i++)
                {
                    float curDist = (HandleUtility.WorldToGUIPoint(r.nodeList[i].pos) - Event.current.mousePosition).magnitude;
                    if (curDist < minDist)
                    {
                        minDist = curDist;
                        closestNode = i;
                    }
                    if (minDist < 20.0f)
                        selectedNode = closestNode;
                    //else
                      //  selectedNode = -1;
                }
            }
        }

        if (Event.current.type == EventType.KeyDown)	// keyboard
        {
            if (Event.current.keyCode == KeyCode.Delete)
            {
                if (selectedNode >= 0)
                {
                    // delete selected node
                    Undo.RecordObject(target, "deleting road node");

                    r.nodeList.RemoveAt(selectedNode);
                    Event.current.Use();
                    selectedNode = -1;

                    r.UpdateMesh(false);
                }
            }

            if (Event.current.keyCode == KeyCode.Insert)
            {
                if (selectedNode > 0)	// it is impossible to add the node before the first one
                {
                    // interpolate nodes
                    Vec2[] points = new Vec2[r.nodeList.Count];

                    for (int i = 0; i < r.nodeList.Count; i++)
                        points[i] = new Vec2(r.nodeList[i].pos.x, r.nodeList[i].pos.z);

                    Spline2D spline = new Spline2D(points);

                    // insert a new node between selected and previous nodes
                    Undo.RecordObject(target, "adding new road node");

                    RoadNode newNode = new RoadNode();
                    newNode.pos = spline.GetValue(selectedNode - 1, 0.5f).ToUnityVector3();
                    newNode.pos.y = (r.isRiver) ? r.riverLevel : Terrain.activeTerrain.SampleHeight(newNode.pos);
                    newNode.width = 0.5f * (r.nodeList[selectedNode - 1].width + r.nodeList[selectedNode].width);
                    newNode.alpha = 0.5f * (r.nodeList[selectedNode - 1].alpha + r.nodeList[selectedNode].alpha);
                    r.nodeList.Insert(selectedNode, newNode);

                    Event.current.Use();
                    selectedNode++;

                    r.UpdateMesh(false);
                }
            }

            // Road splitting. Don't work
            if (Event.current.control && Event.current.shift && (Event.current.keyCode == KeyCode.D))
            {
                if ((selectedNode > 0) && (selectedNode < r.nodeList.Count - 1))
                {
                    Undo.RegisterSceneUndo("Road splitting");

                    GameObject newRoad = new GameObject();
                    newRoad.name = target.name + "_2";
                    Road r2 = newRoad.AddComponent<Road>() as Road;
                    target.name += "_1";

                    r2.isRiver = r.isRiver;
                    r2.priority = r.priority;
                    r2.riverLevel = r.riverLevel;
                    r2.smoothLevel = r.smoothLevel;
                    r2.tiling = r.tiling;
                    r2.widthTiling = r.widthTiling;

                    r2.nodeList.Add(r.nodeList[selectedNode]);
                    while (r.nodeList.Count != selectedNode + 1)
                    {
                        r2.nodeList.Add(r.nodeList[selectedNode + 1]);
                        r.nodeList.RemoveAt(selectedNode + 1);
                    }

                    r.UpdateMesh(false);
                }
            }

        }
        if (Event.current.type == EventType.KeyUp)
        {
            if (Event.current.keyCode == KeyCode.CapsLock)	// keyboard
            {
                HandleType = !HandleType;
            }
        }
        #endregion

        #region DrowNode
        Handles.color = Color.gray;
        // draw position handles
        for (int i = 0; i < r.nodeList.Count; i++)
        {
            Vector3 newpos;
            if (!(HandleType && selectedNode == i))
                newpos = Handles.FreeMoveHandle(r.nodeList[i].pos, Quaternion.identity, Mathf.Min(r.nodeList[i].width, 30f) / 15, Vector3.zero, Handles.SphereCap);
            else
                newpos = Handles.PositionHandle(r.nodeList[i].pos, Quaternion.identity);

            if (GUI.changed)
            {
                if (r.freeHeight)
                {
                    r.nodeList[i].pos = newpos;
                }
                else
                {
                    if (r.isRiver)
                    {
                        Undo.RecordObject(target, "road node moving");
                        r.nodeList[i].pos = newpos;
                    }
                    else
                    {
						RaycastHit hit = new RaycastHit();
                        Ray ray = HandleUtility.GUIPointToWorldRay(HandleUtility.WorldToGUIPoint(newpos));
						//RaycastHit hit;
                        bool succ = false;
                        //Vector3 hitPoint = Terrain.activeTerrain.Raycast(ray, ref succ);
						Vector3 hitPoint = hit.point;
                        if (succ)
                        {
                            Undo.RecordObject(target, "road node moving");
                            r.nodeList[i].pos = r.transform.InverseTransformPoint(hitPoint);
                        }
                    }
                }

                r.UpdateMesh(false);
                break;
            }
        }

        // calculate directions
        r.CalculateNodeParameters();

        // draw width and alpha handles
        for (int i = 0; i < r.nodeList.Count; i++)
        {
            Vector3 slide = new Vector3(-r.nodeList[i].direction.z, 0.0f, r.nodeList[i].direction.x);
            Vector3 nodePos = r.nodeList[i].pos;
            Vector3 posW = nodePos - r.nodeList[i].width * 0.5f * slide;
            Vector3 posA = nodePos + r.nodeList[i].width * 0.5f * slide;

            Handles.color = Color.magenta;
            Vector3 newpos = Handles.FreeMoveHandle(posW, Quaternion.identity, Mathf.Min(r.nodeList[i].width , 10f) / 5, Vector3.zero, Handles.SphereCap);
            float newWidth = (newpos - nodePos).magnitude * 2f;
            newWidth = Mathf.Max(newWidth, 0.1f);

            Handles.color = Color.blue;
            float newAlpha = Handles.ScaleValueHandle(r.nodeList[i].alpha + 0.02f, posA, Quaternion.identity, Mathf.Min(r.nodeList[i].width,10f), Handles.CubeCap, 0.0f) - 0.02f;

            if (GUI.changed)
            {
                Undo.RecordObject(target, "road parameters changing");

                if (Event.current.shift)		// massive parameters changing
                {
                    float scaleFactor = newWidth / r.nodeList[i].width;
                    float shiftFactor = newAlpha - r.nodeList[i].alpha;
                    for (int j = 0; j < r.nodeList.Count; j++)
                    {
                        r.nodeList[j].width *= scaleFactor;
                        r.nodeList[j].alpha = Mathf.Clamp01(r.nodeList[j].alpha + shiftFactor);
                    }
                }
                else
                {
                    r.nodeList[i].width = newWidth;
                    r.nodeList[i].alpha = Mathf.Clamp01(newAlpha);
                }

                r.UpdateMesh(false);
                break;
            }
        }

        // show node's information
        GUIStyle s = new GUIStyle();
        s.fontSize = 10;
        s.fontStyle = FontStyle.Bold;

        for (int i = 0; i < r.nodeList.Count; i++)
        {
            if (selectedNode == i)
            {
                s.normal.textColor = Color.cyan;
                Handles.color = Color.yellow;
            }
            else
                Handles.color = Color.gray;

            Quaternion q = new Quaternion();
            q.SetLookRotation(r.nodeList[i].direction);
            Handles.ConeCap(0, r.nodeList[i].pos, q, Mathf.Min(r.nodeList[i].width, 20f) / 5);

            Handles.Label(r.nodeList[i].pos,
                            "Width = " + r.nodeList[i].width +
                            "\nAlpha = " + r.nodeList[i].alpha +
                            "\nHeight = " + r.transform.TransformPoint(r.nodeList[i].pos).y, s);

            if (selectedNode == i)
                s.normal.textColor = Color.black;
        }
        Handles.matrix = BO.Common.Math.Matrix44_ident;
        #endregion
    }


    public override void OnInspectorGUI()
    {
        Road r = target as Road;

        r.freeHeight = EditorGUILayout.Toggle("Free nodes", r.freeHeight, GUILayout.Width(175));
        if (!r.freeHeight)
        {
            //r.isRiver = EditorGUILayout.Toggle("River?", r.isRiver, GUILayout.Width(175));
			r.isRiver = EditorGUILayout.Toggle("River?", r.isRiver);

            if (r.isRiver)
            {
                r.riverLevel = EditorGUILayout.FloatField("River Level", r.riverLevel, GUILayout.Width(300));
                r.alphaColor = EditorGUILayout.ColorField("Alpha Color", r.alphaColor, GUILayout.Width(300));
            }
        }
        EditorGUILayout.Separator();

        bool tP = EditorGUILayout.Toggle("3-points snap?", r.threePoints, GUILayout.Width(175));

        if (tP != r.threePoints)
            r.threePoints = tP;

        if (!r.threePoints)
            r.centerSnap = EditorGUILayout.Toggle("Center Point?", r.centerSnap, GUILayout.Width(175));

        EditorGUILayout.Separator();

        //r.priority = EditorGUILayout.IntSlider("Priority", r.priority, 1, 3, GUILayout.Width(300));

        //EditorGUILayout.Separator();

        r.tiling = EditorGUILayout.Slider("Texture Tiling", r.tiling, 0.05f, 25.0f, GUILayout.Width(300));
        if (!r.freeHeight)
        {
            if (r.isRiver)
                r.widthTiling = EditorGUILayout.Slider("Texture Tiling U", r.widthTiling, 0.05f, 50.0f, GUILayout.Width(300));
        }
        EditorGUILayout.Separator();

        
        r.smoothLevel = EditorGUILayout.IntSlider("Smoothing", r.smoothLevel, 1, 6, GUILayout.Width(300));
        if (!r.freeHeight)
        {
            r.heightAddition = EditorGUILayout.Slider("Height Addition", r.heightAddition, 0.0f, 0.5f, GUILayout.Width(300));
        }
        EditorGUILayout.Separator();
        //EditorGUILayout.LabelField("Nodes", "" + r.nodeList.Count, GUILayout.ExpandWidth(true));
        //EditorGUILayout.LabelField("Length", "" + r.GetLength(), GUILayout.ExpandWidth(true));
        //EditorGUILayout.LabelField("Vertices", "" + r.GetVerticesCount(), GUILayout.ExpandWidth(true));
        //EditorGUILayout.LabelField("Triangles", "" + r.GetTrianglesCount(), GUILayout.ExpandWidth(true));

        // avoid scaling
        //r.gameObject.transform.localScale = Vector3.one;
        if (GUI.changed)
        {
            Undo.RecordObject(target, "road parameters changing");

            r.UpdateMesh(false);
        }

        Vector3 position = ((Road)target).gameObject.transform.position;
        if (_position == null)
        {
            _position = position;
        }
        if (position != _position.Value)
        {
            _position = position;
            
            r.UpdateMesh(false);
        }

        EditorGUILayout.Separator();
        /*if (GUILayout.Button("Duplicate the road", GUILayout.Width(300)))
        {
            GameObject newRoad = r.DuplicateRoad();
            Selection.objects = new Object[] {newRoad};
        }*/

        if (GUILayout.Button("Reverse the road", GUILayout.Width(300)))
        {
            r.ReverseRoad();
        }
    }
}
