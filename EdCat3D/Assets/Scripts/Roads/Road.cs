using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BO.Common;

[ExecuteInEditMode()]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Road : MonoBehaviour
{
	public List<RoadNode> nodeList = new List<RoadNode>();

	public float tiling = 3.0f;		// number of meters for 1 texture tile
	public float widthTiling = 3.0f;// wide tiling fir rivers
	public int smoothLevel = 1;	// Smoothing Level
	public float angleLevel = 1.0f;	// Smoothing Level
	public float riverLevel = 0.0f;// River level

	public int priority = 1;		// road priority
	public int prevPriority = 1;	// previous road priority

	public bool isRiver = false;	// it is a river
	public bool freeHeight = false;	// it is a river
	public bool centerSnap = false;	// snap to the terrain on center point
	public bool threePoints = false;	// 3-points snapping

	public Color alphaColor = Color.white;

	[SerializeField]
	private Mesh roadMesh;
	[SerializeField]
	private List<RoadPoint> points = new List<RoadPoint>();		// road points	
	[SerializeField]
	private Vector3[] meshVertices;
	[SerializeField]
	private Vector2[] meshUV;
	[SerializeField]
	private int[] meshTriangles;

	[SerializeField]
	private MeshFilter meshFilter;
	[SerializeField]
	private MeshRenderer meshRenderer;

	public float heightAddition = 0.07f;

	protected virtual void Awake()
	{
		meshFilter = GetComponent<MeshFilter>();
		meshRenderer = GetComponent<MeshRenderer>();
	}
	void Start()
	{
		roadMesh = new Mesh();
		roadMesh.name = name + "_Mesh";
		meshFilter.mesh = roadMesh;
		InitializeMesh();
	}

	public void InitializeMesh()
	{
		if (!meshRenderer)
			meshRenderer = gameObject.AddComponent(typeof(MeshRenderer)) as MeshRenderer;

		meshRenderer.enabled = true;
		meshRenderer.castShadows = false;
		meshRenderer.receiveShadows = true;

		UpdateMesh(false);
	}

	public void UpdateMesh(bool forceUpdate)
	{
		if (nodeList.Count < 2)
			return;

		angleLevel = 6.5f - (float)smoothLevel;

		// adjust node heights
		ApplyRiverLevel();

		// perform interpolation
		Interpolate();
		roadMesh.Clear();
		FillVertexBuffer();
		FillIndexBuffer();
		FillNormalsTangents();

		roadMesh.RecalculateBounds();

		if (priority != prevPriority)
		{
			UpdateShader();
			prevPriority = priority;
		}

		if (isRiver)
			meshRenderer.sharedMaterial.SetColor("_AlphaColor", alphaColor);

	}


	public void Interpolate()
	{
		Terrain terrain = Terrain.activeTerrain;
		points.Clear();

		// perform cubic interpolation
		float[] xLeftCoords = new float[nodeList.Count];
		float[] xRightCoords = new float[nodeList.Count];
		float[] zLeftCoords = new float[nodeList.Count];
		float[] zRightCoords = new float[nodeList.Count];
		//float[] yCoords = new float[nodeList.Count];

		for (int i = 0; i < nodeList.Count; i++)
		{
			Vector3 slide = new Vector3(nodeList[i].direction.z, 0.0f, -nodeList[i].direction.x);
			slide *= nodeList[i].width / 2.0f;

			xLeftCoords[i] = (nodeList[i].pos - slide).x;
			xRightCoords[i] = (nodeList[i].pos + slide).x;
			zLeftCoords[i] = (nodeList[i].pos - slide).z;
			zRightCoords[i] = (nodeList[i].pos + slide).z;
		}

		Spline2D left = new Spline2D(xLeftCoords, zLeftCoords);
		Spline2D right = new Spline2D(xRightCoords, zRightCoords);

		// perform initial Segmentation
		float baseSegmentLength = 10.0f / Mathf.Pow(2.0f, smoothLevel - 1);
		float v = 0.0f;
		for (int i = 0; i < nodeList.Count - 1; i++)
		{
			int baseSegments = Mathf.Max(3, (int)(nodeList[i].segmentlength / baseSegmentLength) + 1);
			float yCoords; 
			for (int j = 0; j < baseSegments; j++)
			{
				RoadPoint r = new RoadPoint();

				yCoords = Mathf.Lerp(nodeList[i].pos.y, nodeList[i + 1].pos.y, (float)j / (float)baseSegments) + transform.position.y;


				r.leftPos = transform.TransformPoint(left.GetValue(i, (float)j / (float)baseSegments).ToUnityVector3());
				//r.leftPos.y = (freeHeight) ? yCoords : (isRiver) ? riverLevel : terrain.SampleHeight(r.leftPos - terrain.PositionOffset) + heightAddition;
				r.leftPos.y = (freeHeight) ? yCoords : (isRiver) ? riverLevel : terrain.SampleHeight(r.leftPos) + heightAddition;
				r.leftPos = transform.InverseTransformPoint(r.leftPos);

				r.rightPos = transform.TransformPoint(right.GetValue(i, (float)j / (float)baseSegments).ToUnityVector3());
				//r.rightPos.y = (freeHeight) ? yCoords : (isRiver) ? riverLevel : terrain.SampleHeight(r.rightPos - terrain.PositionOffset) + heightAddition;
				r.rightPos.y = (freeHeight) ? yCoords : (isRiver) ? riverLevel : terrain.SampleHeight(r.rightPos) + heightAddition;
				r.rightPos = transform.InverseTransformPoint(r.rightPos);

				r.centerPos = transform.TransformPoint(0.5f * (r.leftPos + r.rightPos));
				//r.centerPos.y = (freeHeight) ? yCoords : (isRiver) ? riverLevel : terrain.SampleHeight(r.centerPos - terrain.PositionOffset) + heightAddition;
				r.centerPos.y = (freeHeight) ? yCoords : (isRiver) ? riverLevel : terrain.SampleHeight(r.centerPos) + heightAddition;
				r.centerPos = transform.InverseTransformPoint(r.centerPos);

				if (centerSnap && !threePoints)
				{
					r.leftPos.y = r.centerPos.y;
					r.rightPos.y = r.centerPos.y;
				}

				r.alpha = Math.Lerp(nodeList[i].alpha, nodeList[i + 1].alpha, (float)j / (float)baseSegments);

				if (points.Count == 0)
				{
					r.uvL.x = 0.0f;
					r.uvR.x = 0.0f;

					if (isRiver)
					{
						float width = (r.leftPos - r.rightPos).magnitude;
						r.uvL.y = 0.0f;
						r.uvR.y = width / widthTiling;
					}
					else
					{
						r.uvL.y = 0.0f;
						r.uvR.y = 1.0f;
					}
				}
				else
				{
					v += ((r.centerPos - points[points.Count - 1].centerPos).magnitude / tiling);
					r.uvL.x = v;
					r.uvR.x = v;

					if (isRiver)
					{
						float width = (r.leftPos - r.rightPos).magnitude;
						r.uvL.y = 0.0f;
						r.uvR.y = width / widthTiling;
					}
					else
					{
						r.uvL.y = 0.0f;
						r.uvR.y = 1.0f;
					}
				}

				if (j == 0)
					r.isBasePoint = true;
				else
					r.isBasePoint = false;

				points.Add(r);
			}
		}

		//  separately add the latest point
		RoadPoint r1 = new RoadPoint();

		r1.leftPos = transform.TransformPoint(new Vector3(xLeftCoords[nodeList.Count - 1], 0.0f, zLeftCoords[nodeList.Count - 1]));
		//r1.leftPos.y = (freeHeight) ? nodeList[nodeList.Count - 1].pos.y + transform.position.y : (isRiver) ? riverLevel : terrain.SampleHeight(r1.leftPos - terrain.PositionOffset) + heightAddition;
		r1.leftPos.y = (freeHeight) ? nodeList[nodeList.Count - 1].pos.y + transform.position.y : (isRiver) ? riverLevel : terrain.SampleHeight(r1.leftPos) + heightAddition;
		r1.leftPos = transform.InverseTransformPoint(r1.leftPos);

		r1.rightPos = transform.TransformPoint(new Vector3(xRightCoords[nodeList.Count - 1], 0.0f, zRightCoords[nodeList.Count - 1]));
		//r1.rightPos.y = (freeHeight) ? nodeList[nodeList.Count - 1].pos.y + transform.position.y : (isRiver) ? riverLevel : terrain.SampleHeight(r1.rightPos - terrain.PositionOffset) + heightAddition;
		r1.rightPos.y = (freeHeight) ? nodeList[nodeList.Count - 1].pos.y + transform.position.y : (isRiver) ? riverLevel : terrain.SampleHeight(r1.rightPos) + heightAddition;
		r1.rightPos = transform.InverseTransformPoint(r1.rightPos);

		r1.centerPos = transform.TransformPoint(0.5f * (r1.leftPos + r1.rightPos));
		//r1.centerPos.y = (freeHeight) ? nodeList[nodeList.Count - 1].pos.y + transform.position.y : (isRiver) ? riverLevel : terrain.SampleHeight(r1.centerPos - terrain.PositionOffset) + heightAddition;
		r1.centerPos.y = (freeHeight) ? nodeList[nodeList.Count - 1].pos.y + transform.position.y : (isRiver) ? riverLevel : terrain.SampleHeight(r1.centerPos) + heightAddition;
		r1.centerPos = transform.InverseTransformPoint(r1.centerPos);

		r1.alpha = nodeList[nodeList.Count - 1].alpha;

		v += ((r1.centerPos - points[points.Count - 1].centerPos).magnitude / tiling);
		r1.uvL.x = v;
		r1.uvR.x = v;

		if (isRiver)
		{
			float width = (r1.leftPos - r1.rightPos).magnitude;
			r1.uvL.y = 0.0f;
			r1.uvR.y = width / widthTiling;
		}
		else
		{
			r1.uvL.y = 0.0f;
			r1.uvR.y = 1.0f;
		}

		points.Add(r1);

		// optimize mesh
		int k = 0;
		while (k < points.Count - 2)
		{
			float leftA = Vector3.Angle(points[k + 1].leftPos - points[k].leftPos, points[k + 2].leftPos - points[k + 1].leftPos);
			float rightA = Vector3.Angle(points[k + 1].rightPos - points[k].rightPos, points[k + 2].rightPos - points[k + 1].rightPos);

			if ((leftA < angleLevel) &&
				(rightA < angleLevel) &&
				!points[k + 1].isBasePoint)
				points.RemoveAt(k + 1);
			else
				k++;
		}

	}

	public void FillVertexBuffer()
	{
		meshVertices = new Vector3[points.Count * (threePoints ? 3 : 2)];
		meshUV = new Vector2[points.Count * (threePoints ? 3 : 2)];
		Vector2[] uv1 = new Vector2[points.Count * (threePoints ? 3 : 2)];

		int vertIndex = 0;
		for (int i = 0; i < points.Count; i++)
		{
			meshVertices[vertIndex] = points[i].leftPos;
			meshUV[vertIndex] = points[i].uvL;
			uv1[vertIndex] = new Vector2(0.0f, points[i].alpha);
			vertIndex++;

			if (threePoints)
			{
				meshVertices[vertIndex] = points[i].centerPos;
				meshUV[vertIndex] = 0.5f * (points[i].uvL + points[i].uvR);
				uv1[vertIndex] = new Vector2(0.0f, points[i].alpha);
				vertIndex++;
			}

			meshVertices[vertIndex] = points[i].rightPos;
			meshUV[vertIndex] = points[i].uvR;
			uv1[vertIndex] = new Vector2(1.0f, points[i].alpha);
			vertIndex++;
		}

		roadMesh.vertices = meshVertices;
		roadMesh.uv = meshUV;
		roadMesh.uv2 = uv1;
	}

	public void FillIndexBuffer()
	{
		meshTriangles = new int[6 * (points.Count - 1) * (threePoints ? 2 : 1)];
		int triIndex = 0;

		for (int i = 0; i < points.Count - 1; i++)
		{
			if (!threePoints)
			{
				meshTriangles[triIndex++] = i * 2;
				meshTriangles[triIndex++] = (i + 1) * 2;
				meshTriangles[triIndex++] = (i + 1) * 2 + 1;

				meshTriangles[triIndex++] = i * 2;
				meshTriangles[triIndex++] = (i + 1) * 2 + 1;
				meshTriangles[triIndex++] = i * 2 + 1;
			}

			if (threePoints)
			{
				meshTriangles[triIndex++] = i * 3;
				meshTriangles[triIndex++] = (i + 1) * 3;
				meshTriangles[triIndex++] = (i + 1) * 3 + 1;

				meshTriangles[triIndex++] = i * 3;
				meshTriangles[triIndex++] = (i + 1) * 3 + 1;
				meshTriangles[triIndex++] = i * 3 + 1;

				meshTriangles[triIndex++] = i * 3 + 1;
				meshTriangles[triIndex++] = (i + 1) * 3 + 1;
				meshTriangles[triIndex++] = (i + 1) * 3 + 2;

				meshTriangles[triIndex++] = i * 3 + 1;
				meshTriangles[triIndex++] = (i + 1) * 3 + 2;
				meshTriangles[triIndex++] = i * 3 + 2;
			}
		}

		roadMesh.triangles = meshTriangles;
	}

	public void CalculateNodeParameters()
	{
		if (nodeList.Count < 2)
		{
			for (int i = 0; i < nodeList.Count; i++)
			{
				nodeList[i].direction = Vector3.zero;
			}
			return;
		}

		Vec2[] points = new Vec2[nodeList.Count];

		for (int i = 0; i < nodeList.Count; i++)
			points[i] = new Vec2(nodeList[i].pos.x, nodeList[i].pos.z);

		Spline2D spline = new Spline2D(points);

		Vector3 pos1;
		Vector3 pos2;
		for (int i = 0; i < nodeList.Count - 1; i++)
		{
			pos1 = new Vector3(nodeList[i].pos.x, 0.0f, nodeList[i].pos.z);
			pos2 = spline.GetValue(i, 0.05f).ToUnityVector3();
			nodeList[i].direction = (pos2 - pos1).normalized;
		}

		// calculate the latest direction
		pos1 = spline.GetValue(nodeList.Count - 2, 0.95f).ToUnityVector3();
		pos2 = new Vector3(nodeList[nodeList.Count - 1].pos.x, 0.0f, nodeList[nodeList.Count - 1].pos.z);
		nodeList[nodeList.Count - 1].direction = (pos2 - pos1).normalized;

		// approximate distances to the next nodepoint
		for (int i = 0; i < nodeList.Count - 1; i++)
		{
			int integrationSteps = Mathf.Max(5, (int)((nodeList[i].pos - nodeList[i + 1].pos).magnitude / 25.0f));
			float segmentlength = 0.0f;
			Vector3 prevPoint = new Vector3();
			prevPoint = nodeList[i].pos;
			for (int j = 1; j <= integrationSteps; j++)
			{
				Vector3 curPoint = spline.GetValue(i, (float)j / (float)integrationSteps).ToUnityVector3();
				if (freeHeight)
					curPoint.y = prevPoint.y;
				else
					if (isRiver)
						curPoint.y = Terrain.activeTerrain.SampleHeight(curPoint);
				segmentlength += (curPoint - prevPoint).magnitude;
				prevPoint = curPoint;
			}
			nodeList[i].segmentlength = segmentlength;
		}
	}


	public float GetLength()
	{
		if (points.Count < 2)
			return 0.0f;
		else
			return ((points[points.Count - 1].uvL.x - points[0].uvL.x) * tiling);
	}

	public int GetVerticesCount()
	{
		return points.Count * 2;
	}

	public int GetTrianglesCount()
	{
		if (points.Count < 1)
			return 0;
		else
			return (2 * (points.Count - 1));
	}



	public void FillNormalsTangents()	// http://www.terathon.com/code/tangent.html
	{
		if (points.Count < 2)
			return;

		int vertexCount = meshVertices.Length;
		int triangleCount = (points.Count - 1) * (threePoints ? 4 : 2);

		Vector3[] normals = new Vector3[vertexCount];
		Vector4[] tangents = new Vector4[vertexCount];
		Vector3[] tan1 = new Vector3[vertexCount];
		Vector3[] tan2 = new Vector3[vertexCount];

		int triIndex = 0;

		int i1, i2, i3;
		Vector3 v1, v2, v3, w1, w2, w3, sdir, tdir;
		float x1, x2, y1, y2, z1, z2, s1, s2, t1, t2, r;
		for (int i = 0; i < triangleCount; i++)
		{
			i1 = meshTriangles[triIndex];
			i2 = meshTriangles[triIndex + 1];
			i3 = meshTriangles[triIndex + 2];

			v1 = meshVertices[i1];
			v2 = meshVertices[i2];
			v3 = meshVertices[i3];

			// normal
			Vector3 normal = Vector3.Cross(v2 - v1, v3 - v1);
			normal = normal.normalized;
			normals[i1] += normal;
			normals[i2] += normal;
			normals[i3] += normal;

			//tangent
			if (isRiver)
			{
				sdir = Vector3.right;
				tdir = Vector3.forward;
			}
			else
			{
				w1 = meshUV[i1];
				w2 = meshUV[i2];
				w3 = meshUV[i3];

				x1 = v2.x - v1.x;
				x2 = v3.x - v1.x;
				y1 = v2.y - v1.y;
				y2 = v3.y - v1.y;
				z1 = v2.z - v1.z;
				z2 = v3.z - v1.z;

				s1 = w2.x - w1.x;
				s2 = w3.x - w1.x;
				t1 = w2.y - w1.y;
				t2 = w3.y - w1.y;

				r = 1.0f / (s1 * t2 - s2 * t1);
				sdir = new Vector3((t2 * x1 - t1 * x2) * r, (t2 * y1 - t1 * y2) * r, (t2 * z1 - t1 * z2) * r);
				tdir = new Vector3((s1 * x2 - s2 * x1) * r, (s1 * y2 - s2 * y1) * r, (s1 * z2 - s2 * z1) * r);
			}

			tan1[i1] += sdir;
			tan1[i2] += sdir;
			tan1[i3] += sdir;

			tan2[i1] += tdir;
			tan2[i2] += tdir;
			tan2[i3] += tdir;

			triIndex += 3;
		}

		for (int i = 0; i < vertexCount; i++)
		{
			Vector3 n = normals[i];
			Vector3 t = tan1[i];

			// Gram-Schmidt orthogonalize
			Vector3.OrthoNormalize(ref n, ref t);

			tangents[i].x = t.x;
			tangents[i].y = t.y;
			tangents[i].z = t.z;

			// Calculate handedness
			tangents[i].w = (Vector3.Dot(Vector3.Cross(n, t), tan2[i]) < 0.0f) ? -1.0f : 1.0f;
			normals[i] = n;
		}

		roadMesh.normals = normals;
		roadMesh.tangents = tangents;
	}

	private void UpdateShader()
	{
		/*		switch (priority)
                {
                    case 1:
                        meshRenderer.material.shader = Shader.Find("BO/Road");
                        break;
                    case 2:
                        meshRenderer.material.shader = Shader.Find("BO/Road1");
                        break;
                    case 3:
                        meshRenderer.material.shader = Shader.Find("BO/Road2");
                        break;
                }*/
	}

	private void ApplyRiverLevel()
	{
		if(!freeHeight)
		{
			for (int i = 0; i < nodeList.Count; i++)
			{
				if (isRiver) nodeList[i].pos.y = riverLevel;
				//else
					//Terrain.activeTerrain.SnapPointToTerrain(ref nodeList[i].pos, transform);
			}
		}
	}

	public void Rasterize(Vec2 cellSize, Math.OnPoint processor)
	{
		for (int i = 0; i < points.Count - 1; i++)
		{
			Vec2[] tP = new Vec2[4];
			tP[0] = transform.TransformPoint(points[i].leftPos).ToVec2();
			tP[1] = transform.TransformPoint(points[i + 1].leftPos).ToVec2();
			tP[2] = transform.TransformPoint(points[i + 1].rightPos).ToVec2();
			tP[3] = transform.TransformPoint(points[i].rightPos).ToVec2();

			Math.RasterizePoly2(tP, cellSize, processor);
		}
	}

	// will return a river points in CW order
	public Vec2[] Rasterize2Poly()
	{
		List<Vec2> poly = new List<Vec2>();

		// forward
		for (int i = 0; i < points.Count; i++)
		{
			poly.Add(transform.TransformPoint(points[i].rightPos).ToVec2());
		}

		// backard
		for (int i = points.Count - 1; i >= 0; i--)
		{
			poly.Add(transform.TransformPoint(points[i].leftPos).ToVec2());
		}


		// optimize mesh
		float level = 25.0f;
		int k = 0;
		while (k < poly.Count - 2)
		{
			float angle = Vec2.Angle(poly[k + 1] - poly[k], poly[k + 2] - poly[k + 1]);

			if (angle < level)
				poly.RemoveAt(k + 1);
			else
				k++;
		}

		return poly.ToArray();
	}

	public GameObject DuplicateRoad()
	{
		GameObject newRoadGO = Instantiate(gameObject) as GameObject;
		newRoadGO.name = gameObject.name;

		Road newRoad = newRoadGO.GetComponent<Road>();
		newRoad.DuplicateRoadInternal(this);

		newRoadGO.transform.parent = transform.parent;
		newRoadGO.transform.position = transform.position;
		newRoadGO.transform.rotation = transform.rotation;

		return newRoadGO;
	}

	private void DuplicateRoadInternal(Road src)
	{
		roadMesh = new Mesh();
		roadMesh.name = name + "_Mesh";

		MeshFilter meshFilter = gameObject.GetComponent(typeof(MeshFilter)) as MeshFilter;
		meshFilter.sharedMesh = roadMesh;

		nodeList = new List<RoadNode>();
		foreach (RoadNode node in src.nodeList)
			nodeList.Add(new RoadNode(node));
	}

	public void ReverseRoad()
	{
		nodeList.Reverse();
		UpdateMesh(true);
	}
}