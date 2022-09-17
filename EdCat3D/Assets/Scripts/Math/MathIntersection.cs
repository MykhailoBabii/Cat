namespace BO.Common
{
    public partial class Math
    {
        //----------------------------------------------------------------------------------
        // Circle vs circle intersection
        public static bool CircleVsCircleIntersection(Vec2 p1, float r1, Vec2 p2, float r2)
        {
			float r = r1 + r2;
			return Vec2.SqrDistance(p1, p2) <= r * r;
        }

		//----------------------------------------------------------------------------------
        // Circle vs rectangle intersection
        // (x,y) - rect pos
        // (sx,sy) - HALF sides
        // (cx,cy) - circle position
        // r - circle radius
        public static bool CircleVsRectIntersection(float x, float y, float sx, float sy, float cx, float cy, float r)
        {
            float xLocal = System.Math.Abs(x - cx) - sx;
            float yLocal = System.Math.Abs(y - cy) - sy;
            if (xLocal > 0)
            {
                return (yLocal > 0) ? (xLocal * xLocal + yLocal * yLocal) <= r * r : (xLocal <= r);
            }
            else
            {
                return (yLocal > 0) ? (yLocal <= r) : true;
            }

        }

        //----------------------------------------------------------------------------------
        // Circle vs rectangle intersection
        public static bool CircleVsRectIntersection(Vec2 pos, Vec2 halfSize, Vec2 dir, Vec2 circlePos, float r)
        {
            Vec2 d = circlePos - pos;
            float y = Vec2.Dot(d, dir);
            float x = Vec2.Cross(d, dir);
            return CircleVsRectIntersection(0, 0, halfSize.x, halfSize.y, x, y, r);
        }
        
        //----------------------------------------------------------------------------------
        private static bool IntersectRayBox2DSide(float rayOX, float rayOY, float rayDirX, float rayDirY,
                                                  float boxMinX, float boxMaxX, float boxY, ref float iSectX, ref float tValue)
        {
            tValue = (boxY - rayOY) / rayDirY;
            iSectX = rayOX + rayDirX * tValue;
            return boxMinX <= iSectX && iSectX <= boxMaxX;
        }
  
        
        //----------------------------------------------------------------------------------
        public static bool IntersectRayBox2D(Vec2 rayOrigin, Vec2 rayDir, Vec2 boxMin, Vec2 boxMax, out Vec2 iPoint, out Vec2 iNormal)
        {
            float iSectCoord = 0.0f;
            float tValue = 0.0f;
            float minTValue = float.MaxValue;
            iPoint = Vec2.zero;
			iNormal = Vec2.zero;

            // Check ray is not directed to box
            if ((rayOrigin.x < boxMin.x && rayDir.x < 0.0f) ||
                (rayOrigin.x > boxMax.x && rayDir.x > 0.0f) ||
                (rayOrigin.y < boxMin.y && rayDir.y < 0.0f) ||
                (rayOrigin.y > boxMax.y && rayDir.y > 0.0f))
            {
                return false;
            }

            // MinX - MaxX @ MinY
            if (rayDir.y > EPSILON &&
                IntersectRayBox2DSide(rayOrigin.x, rayOrigin.y, rayDir.x, rayDir.y, boxMin.x, boxMax.x, boxMin.y,
                                      ref iSectCoord, ref tValue))
            {
                if (tValue < minTValue)
                {
                    minTValue = tValue;
                    iPoint.x = iSectCoord;
                    iPoint.y = boxMin.y;
					iNormal.x = 0.0f;
					iNormal.y = -1.0f;
                }
            }

            // MinX - MaxX @ MaxY
            if (rayDir.y < -EPSILON &&
                IntersectRayBox2DSide(rayOrigin.x, rayOrigin.y, rayDir.x, rayDir.y, boxMin.x, boxMax.x, boxMax.y,
                                      ref iSectCoord, ref tValue))
            {
                if (tValue < minTValue)
                {
                    minTValue = tValue;
                    iPoint.x = iSectCoord;
                    iPoint.y = boxMax.y;
					iNormal.x = 0.0f;
					iNormal.y = 1.0f;
                }
            }

            // MinY - MaxY @ MinX
            if (rayDir.x > EPSILON &&
                IntersectRayBox2DSide(rayOrigin.y, rayOrigin.x, rayDir.y, rayDir.x, boxMin.y, boxMax.y, boxMin.x,
                                      ref iSectCoord, ref tValue))
            {
                if (tValue < minTValue)
                {
                    minTValue = tValue;
                    iPoint.x = boxMin.x;
                    iPoint.y = iSectCoord;
					iNormal.x = -1.0f;
					iNormal.y = 0.0f;
                }
            }

            // MinY - MaxY @ MaxX
            if (rayDir.x < -EPSILON &&
                IntersectRayBox2DSide(rayOrigin.y, rayOrigin.x, rayDir.y, rayDir.x, boxMin.y, boxMax.y, boxMax.x,
                                      ref iSectCoord, ref tValue))
            {
                if (tValue < minTValue)
                {
                    minTValue = tValue;
                    iPoint.x = boxMax.x;
                    iPoint.y = iSectCoord;
					iNormal.x = 1.0f;
					iNormal.y = 0.0f;
                }
            }

            return minTValue < float.MaxValue;
        }

  
        //----------------------------------------------------------------------------------
        public static bool IntersectRayBox2D(Vec2 rayOrigin, Vec2 rayDir, Vec2 boxMin, Vec2 boxMax, float boxAngle, out Vec2 iPoint)
        {
            var boxCenter = (boxMin + boxMax) * 0.5f;
			Vec2 iNormal;
            return IntersectRayBox2D(Vec2.RotateAroundDeg(rayOrigin, boxCenter, -boxAngle),
                                     Vec2.RotateDeg(rayDir, -boxAngle),
                                     boxMin, boxMax, out iPoint, out iNormal);
        }
			
        
        //----------------------------------------------------------------------------------
        public static bool IntersectRayBox2D(Vec2 rayOrigin, Vec2 rayDir, Vec2 boxMin, Vec2 boxMax, float boxAngle, out float distance)
        {
            Vec2 isectPoint;
            if (IntersectRayBox2D(rayOrigin, rayDir, boxMin, boxMax, boxAngle, out isectPoint))
            {
                distance = Vec2.Distance(rayOrigin, isectPoint);
                return true;
            }

            distance = 0f;
            return false;
        }
		
        //----------------------------------------------------------------------------------
		public static bool ApproximatelyLess(float x, float y)
		{
			bool isLess = x < (y - 0.001f);
			return isLess;
		}
        
        //----------------------------------------------------------------------------------
	 	public static bool AreRectsIntersected(Vec2 minPoint1, Vec2 maxPoint1, Vec2 minPoint2, Vec2 maxPoint2)
		{
			float left = minPoint1.x;
			float right = maxPoint1.x;
			float top = maxPoint1.y;
			float bottom = minPoint1.y;
			
			float other_left = minPoint2.x;
			float other_right = maxPoint2.x;
			float other_top = maxPoint2.y;
			float other_bottom = minPoint2.y;
			
			return ( (left < other_right) && (right > other_left) && (top > other_bottom) && (bottom < other_top) );
		}
	 		
        
        //----------------------------------------------------------------------------------
		public static bool IsPointInsideRect( Vec2 minPoint, Vec2 maxPoint, Vec3 p )
		{
			return ( (p.x > minPoint.x) && (p.z > minPoint.y) && (p.x < maxPoint.x) && (p.z < maxPoint.y) );
		}
  
        
        //----------------------------------------------------------------------------------
		public static bool IsPointFullyInRect(Vec2 point, Vec2 rectCenter, Vec2 rectDimensions, float rectAngle)
		{
            var localPos = point.TransformToLocal(rectCenter, rectAngle);
            var halfDim  = rectDimensions / 2f;
            return ApproximatelyLess(-halfDim.x, localPos.x) 
				&& ApproximatelyLess(localPos.x, halfDim.x) 
				&& ApproximatelyLess(-halfDim.y, localPos.y) 
				&& ApproximatelyLess(localPos.y, halfDim.y);
		}
		
        //----------------------------------------------------------------------------------
		public static bool IsPointInsideRect(Vec2 point, Vec2 rectCenter, Vec2 rectDimensions, float rectAngle)
		{
            var localPos = point.TransformToLocal(rectCenter, rectAngle);
            var halfDim  = rectDimensions / 2;
            return localPos.x >= -halfDim.x && localPos.x <= halfDim.x && localPos.y >= -halfDim.y && localPos.y <= halfDim.y;
		}

        
        //----------------------------------------------------------------------------------
		public static bool IsPointInsideRect( Vec2 minPoint, Vec2 maxPoint, Vec2 p )
		{
			return ( (p.x > minPoint.x) && (p.y > minPoint.y) && (p.x < maxPoint.x) && (p.y < maxPoint.y) );
		}
		
		
        //----------------------------------------------------------------------------------
		public static bool ArePolysIntersected(Vec2[] poly1, Vec2[] poly2)
		{
			// check if a poing from poly1 is in poly2
			for (int i = 0; i < poly1.Length; i++)
			{
				if (PolyIsPointIn(poly2, poly1[i]))
					return true;
			}
			
			// check if a poing from poly2 is in poly1
			for (int i = 0; i < poly2.Length; i++)
			{
				if (PolyIsPointIn(poly1, poly2[i]))
					return true;
			}
			
			// if no point from both polys is not inside the other poly, the polygons have no intersection
			return false;
		}
        
        
        //----------------------------------------------------------------------------------
        public static Vec3 ClipRay(Vec3 rayOrigin, Vec3 rayDir, float rayLength, Vec2 rectMin, Vec2 rectMax)
        {
            var endPnt = rayOrigin + rayDir * rayLength;
            ClipRay(rayOrigin, ref endPnt, rectMin, rectMax);
            return endPnt;
        }
        
        
        //----------------------------------------------------------------------------------
        public static void ClipRay(Vec3 begPnt, ref Vec3 endPnt, Vec2 rectMin, Vec2 rectMax)
        {
            //Assert.IsTrue(Math.IsPointInsideRect(rectMin, rectMax, begPnt.Vec2), "Ray start point should be inside rect!");
            
            if (Math.IsPointInsideRect(rectMin, rectMax, endPnt.Vec2))
                return;
            
            Vec2 iPnt, iNrm;

            Vec2 rayO = endPnt.Vec2;
            Vec2 rayD = (begPnt - endPnt).Vec2;
            
            Math.IntersectRayBox2D(rayO, rayD, rectMin, rectMax, out iPnt, out iNrm);
            float t  = Vec2.Dot(rayD, iPnt) / rayD.sqrMagnitude;
            endPnt   = iPnt.Vec3;
            endPnt.y = begPnt.y + (endPnt.y - begPnt.y) * t;
        }

        
        //----------------------------------------------------------------------------------
        public static bool ClipSegment(ref Vec3 begPnt, ref Vec3 endPnt, Vec2 rectMin, Vec2 rectMax)
        {
            Vec2  rayO, rayD;
            Vec2  iPnt, iNrm;
            float t;

            if (!IsPointInsideRect(rectMin, rectMax, begPnt.Vec2))
            {
                rayO = begPnt.Vec2;
                rayD = (endPnt - begPnt).Vec2;
                if (!IntersectRayBox2D(rayO, rayD, rectMin, rectMax, out iPnt, out iNrm))
                    return false; // Completely no intersection

                t = Vec2.Dot(rayD, iPnt - rayO) / rayD.sqrMagnitude;
                begPnt.x = iPnt.x;
                begPnt.y = begPnt.y + (endPnt.y - begPnt.y) * t;
                begPnt.z = iPnt.y;
            }

            if (IsPointInsideRect(rectMin, rectMax, endPnt.Vec2))
                return true;

            rayO = endPnt.Vec2;
            rayD = (begPnt - endPnt).Vec2;
            IntersectRayBox2D(rayO, rayD, rectMin, rectMax, out iPnt, out iNrm);

            t = Vec2.Dot(rayD, iPnt - rayO) / rayD.sqrMagnitude;
            endPnt.x = iPnt.x;
            endPnt.y = endPnt.y + (begPnt.y - endPnt.y) * t;
            endPnt.z = iPnt.y;

            return true;
        }


        //----------------------------------------------------------------------------------
        public static bool ClipSegment(ref Vec2 begPnt, ref Vec2 endPnt, Vec2 rectMin, Vec2 rectMax)
        {
            Vec2 iNrm;

            if (!IsPointInsideRect(rectMin, rectMax, begPnt))
            {
                if (!IntersectRayBox2D(begPnt, endPnt - begPnt, rectMin, rectMax, out begPnt, out iNrm))
                    return false; // Completely no intersection
            }

            if (IsPointInsideRect(rectMin, rectMax, endPnt))
                return true;

            IntersectRayBox2D(endPnt, begPnt - endPnt, rectMin, rectMax, out endPnt, out iNrm);
            return true;
        }


        //----------------------------------------------------------------------------------
        public static bool IntersectRaySphere(Vec3 rayOrigin, Vec3 rayDir, Vec3 sphereOrigin, float sphereRadius, out Vec3 hitPoint)
        {
            hitPoint = Vec3.zero;

            var v = rayOrigin - sphereOrigin;
            var b = 2f * Vec3.Dot(rayDir, v);
            var c = Vec3.Dot(v, v) - sphereRadius * sphereRadius;
            var d = b * b - 4f * c;

            if (d < 0f)
                return false;

            d = (float)System.Math.Sqrt(d);

            var t0 = (-b + d) / 2f;
            var t1 = (-b - d) / 2f;

            if (t0 < 0f && t1 < 0f)
                 return false;

            if (t0 >= 0f && t1 < 0f)
                hitPoint = rayOrigin + rayDir * t0;
            else if (t0 < 0f && t1 >= 0f)
                hitPoint = rayOrigin + rayDir * t1;
            else
                hitPoint = rayOrigin + rayDir * System.Math.Min(t0, t1);

            return true;
        }
    }
}

