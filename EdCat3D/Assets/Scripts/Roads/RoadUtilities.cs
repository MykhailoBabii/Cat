using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BO.Common;

[System.Serializable]
public class RoadNode
{
	public Vector3 pos;
	public float   alpha = 0.8f;
	public float   width = 5.0f;
	public Vector3 direction;
	public float   segmentlength;
	
	public RoadNode()
	{
		// empty
	}
	
	public RoadNode(RoadNode src)
	{
		pos = new Vector3(src.pos.x, src.pos.y, src.pos.z);
		alpha = src.alpha;
		width = src.width;
		direction = src.direction;
		segmentlength = src.segmentlength;
	}
}

[System.Serializable]
public class RoadPoint
{
	public Vector3 leftPos;
	public Vector3 rightPos;
	public Vector3 centerPos;
	public Vector3 normal;
	public Vector3 tangent;
	public float   alpha;
	public float   width;
	public Vector2 uvL;
	public Vector2 uvR;
	
	public bool isBasePoint;	// the point is based on a NodePoint
}
