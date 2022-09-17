using UnityEngine;
using System;
using System.Collections.Generic;
using BO.Common;
using Math = BO.Common.Math;

namespace BO.Common
{
	public partial class Math
	{
		public static UnityEngine.Matrix4x4 Matrix44_ident = UnityEngine.Matrix4x4.identity;
		public static UnityEngine.Vector3 Vector3_zero = UnityEngine.Vector3.zero;
		public static UnityEngine.Vector4 Vector4_zero = UnityEngine.Vector4.zero;
		public static UnityEngine.Quaternion Quaternion_ident = UnityEngine.Quaternion.identity;

		//----------------------------------------------------------------------------------
		public static int CheckCulling(Bounds bound, Matrix4x4 viewProjection)        
		{
			return CheckCulling(bound.max, bound.min, viewProjection);
		}


		static Vector4[] points = new Vector4[8];
		public static int CheckCulling(Vector3 max, Vector3 min, Matrix4x4 viewProjection)
		{
			int andFlags = 0xffff;
			int orFlags  = 0;
			int i;
			Vector4 v1;

			points[0].Set(max.x, max.y, max.z, 1.0f);
			points[1].Set(min.x, max.y, max.z, 1.0f);
			points[2].Set(max.x, min.y, max.z, 1.0f);
			points[3].Set(min.x, min.y, max.z, 1.0f);
			points[4].Set(max.x, max.y, min.z, 1.0f);
			points[5].Set(min.x, max.y, min.z, 1.0f);
			points[6].Set(max.x, min.y, min.z, 1.0f);
			points[7].Set(min.x, min.y, min.z, 1.0f);

			for (i = 0; i < 8; i++)
			{
				int clip = 0;
				v1 = viewProjection * points[i];

				if (v1.x < -v1.w)
					clip |= 1<<0;			// ClipLeft
				else
					if (v1.x > v1.w)
						clip |= 1<<1;		// ClipRight

				if (v1.y < -v1.w)
					clip |= 1<<2;			// ClipBottom
				else
					if (v1.y > v1.w)
						clip |= 1<<3;		// ClipTop

				if (v1.z < -v1.w)
					clip |= 1<<5;			// ClipFar
				else
					if (v1.z > v1.w)
						clip |= 1<<4;		// ClipNear

				andFlags &= clip;
				orFlags  |= clip;

				if (andFlags == 0)
					return 1;				// Clipped
			}

			if (0 == orFlags)
				return 2;			// Inside
			else
				if (0 != andFlags)
					return 0;		// Outside
				else
					return 1;		// Clipped

		}		
	}
}