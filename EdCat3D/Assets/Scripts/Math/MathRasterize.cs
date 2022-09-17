using System;

namespace BO.Common
{
    public partial class Math
    {
        public delegate void OnPoint(int x, int y);

        //----------------------------------------------------------------------------------
        public static bool PolyIsPointIn(Vec2[] points, Vec2 p)
        {
            int j = points.Length - 1;
            for (int i = 0; i < points.Length; j = i, i++)
            {
				if ((points[i] - points[j]).magnitude < Math.EPSILON)
				{
					return ((p - points[i]).magnitude < Math.EPSILON);
				}
				else
                	if (Vec2.Cross(points[i] - points[j], p - points[j]) > 0.0f)
                    	return false;
            }
            return true;
        }

        //----------------------------------------------------------------------------------
        public static void PolyMinMax(Vec2[] points, out Vec2 min, out Vec2 max)
        {
            // Calculate bounds
            min = points[0];
            max = points[0];
            for (int i = 1; i < points.Length; i++)
            {
                min.x = System.Math.Min(min.x, points[i].x);
                min.y = System.Math.Min(min.y, points[i].y);
                max.x = System.Math.Max(max.x, points[i].x);
                max.y = System.Math.Max(max.y, points[i].y);
            }
        }

        //----------------------------------------------------------------------------------
        // Simple & straightforward version
        public static void RasterizePoly(Vec2[] points, Vec2 cellSize, OnPoint processor)
        {
            // Calculate bounds
            Vec2 min, max;
            PolyMinMax(points, out min, out max);

            Vec2 invCellSize;
            invCellSize.x = 1.0f / cellSize.x;
            invCellSize.y = 1.0f / cellSize.y;

            int xMin = (int)System.Math.Floor(min.x * invCellSize.x);
            int yMin = (int)System.Math.Floor(min.y * invCellSize.y);
            int xMax = (int)System.Math.Ceiling(max.x * invCellSize.x);
            int yMax = (int)System.Math.Ceiling(max.y * invCellSize.y);

            Vec2 pos;
            pos.y = (yMin + 0.5f) * cellSize.y;
            for (int y = yMin; y <= yMax; y++, pos.y += cellSize.y)
            {
                pos.x = (xMin + 0.5f) * cellSize.x;
                for (int x = xMin; x <= xMax; x++, pos.x += cellSize.x)
                {
                    if (PolyIsPointIn(points, pos))
                        processor(x, y);
                }
            }
        }

        //----------------------------------------------------------------------------------
        public static void RasterizePoly(Vec2[] points, OnPoint processor)
        {
            RasterizePoly(points, Vec2.one, processor);
        }

        //----------------------------------------------------------------------------------
        // Simple & straightforward version
        public static void RasterizeCircle(Vec2 center, float radius, Vec2 cellSize, OnPoint processor)
        {
            Vec2 invCellSize;
            invCellSize.x = 1.0f / cellSize.x;
            invCellSize.y = 1.0f / cellSize.y;

            int xMin = (int)System.Math.Floor((center.x - radius) * invCellSize.x);
            int yMin = (int)System.Math.Floor((center.y - radius) * invCellSize.y);
            int xMax = (int)System.Math.Ceiling((center.x + radius) * invCellSize.x);
            int yMax = (int)System.Math.Ceiling((center.y + radius) * invCellSize.y);

            float sqrRad = radius * radius;

            Vec2 pos;
            pos.y = (yMin + 0.5f) * cellSize.y;
            for (int y = yMin; y <= yMax; y++, pos.y += cellSize.y)
            {
                pos.x = (xMin + 0.5f) * cellSize.x;
                for (int x = xMin; x <= xMax; x++, pos.x += cellSize.x)
                {
                    if (Vec2.SqrDistance(pos, center) < sqrRad)
                        processor(x, y);
                }
            }
        }

        //----------------------------------------------------------------------------------
        public static void RasterizeCircle(Vec2 center, float radius, OnPoint processor)
        {
            RasterizeCircle(center, radius, Vec2.one, processor);
        }

        private static void SetPoint(ref int[,] bounds, int x, int y)
        {
            bounds[y, 0] = System.Math.Min(x, bounds[y, 0]);
            bounds[y, 1] = System.Math.Max(x, bounds[y, 1]);
        }

        //----------------------------------------------------------------------------------
        // Alternative Rasterization method

        static int[,] bounds = new int[16, 2];
        public static void RasterizePoly2(Vec2[] sourcepoints, Vec2 cellSize, OnPoint processor)
        {
			int numPoints = sourcepoints.Length;
			if (numPoints < 3)
				return;
			
			Vec2[] points = new Vec2[sourcepoints.Length];
			
            // normalize points to the grid
            Vec2 invCellSize;
            invCellSize.x = 1.0f / cellSize.x;
            invCellSize.y = 1.0f / cellSize.y;
            for (int i = 0; i < numPoints; i++)
            {
                points[i].x = sourcepoints[i].x * invCellSize.x;
                points[i].y = sourcepoints[i].y * invCellSize.y;
            }

            // Calculate bounds
            Vec2 min, max;
            PolyMinMax(points, out min, out max);

            int minY = (int)System.Math.Floor(min.y);
            int maxY = (int)System.Math.Floor(max.y);
            int sizeY = maxY - minY + 1;

            // increase bouns array size if necessary
            int boundsSize = bounds.Length / 2;
            while (boundsSize < sizeY)
                boundsSize *= 2;
            if (boundsSize > bounds.Length / 2)
                bounds = new int[boundsSize, 2];

            // initialize the array
            for (int i = 0; i < sizeY; i++)
            {
                bounds[i, 0] = int.MaxValue;
                bounds[i, 1] = int.MinValue;
            }

            // check edges
            for (int i = 0; i < numPoints; i++)
            {
                Vec2 v1 = points[i];
                Vec2 v2 = points[(i + 1) % numPoints];

                int signX = (v2.x > v1.x) ? 1 : -1;
                int signY = (v2.y > v1.y) ? 1 : -1;

                int startX = (int)System.Math.Floor(v1.x);
                int startY = (int)System.Math.Floor(v1.y);
                int endX = (int)System.Math.Floor(v2.x);
                int endY = (int)System.Math.Floor(v2.y);

                int curX = startX;
                int curY = startY;

                float dx = 0.0f;
                float dy = 0.0f;
                if ((startX != endX) && (startY != endY))
                {
                    dx = 1.0f / (v2.x - v1.x);
                    dy = 1.0f / (v2.y - v1.y);
                }

                SetPoint(ref bounds, startX, startY - minY);	// fill starting point

                while (!((curX == endX) && (curY == endY)))
                {
                    if (curX == endX)
                        curY += signY;
                    else
                    {
                        if (curY == endY)
                            curX += signX;
                        else
                        {
                            int newX = curX + ((signX > 0) ? 1 : 0);
                            int newY = curY + ((signY > 0) ? 1 : 0);
                            float tx = (newX - v1.x) * dx;
                            float ty = (newY - v1.y) * dy;

                            if (System.Math.Abs(tx - ty) < Math.EPSILON)
                            {
                                curX += signX;
                                curY += signY;
                            }
                            else
                            {
                                if (tx < ty)
                                    curX += signX;      // X-crossing
                                else
                                    curY += signY;      // Y-crossing
                            }
                        }
                    }
                    SetPoint(ref bounds, curX, curY - minY);	// fill the point					
                }
            }

            // filling
            for (int y = minY; y <= maxY; y++)
            {
                for (int x = bounds[y - minY, 0]; x <= bounds[y - minY, 1]; x++)
                    processor(x, y);
            }
        }

        // Rasterize line segment in 3D
        public delegate bool RasterizerCallback(int x, int y, int t);
        public static bool RasterizeLSeg(Vec3 from, Vec3 to, Vec3 cellSize, RasterizerCallback callback)
        {
            // normalize points to the grid
            Vec3 invCellSize;
            invCellSize.x = 1.0f / cellSize.x;
            invCellSize.y = 1.0f / cellSize.y;
            invCellSize.z = 1.0f / cellSize.z;
            from.x *= invCellSize.x;
            from.y *= invCellSize.y;
            from.z *= invCellSize.z;
            to.x *= invCellSize.x;
            to.y *= invCellSize.y;
            to.z *= invCellSize.z;

            int signX = (to.x > from.x) ? 1 : -1;
            int signY = (to.y > from.y) ? 1 : -1;
            int signZ = (to.z > from.z) ? 1 : -1;

            int startX = (int)System.Math.Floor(from.x);
            int startY = (int)System.Math.Floor(from.y);
            int startZ = (int)System.Math.Floor(from.z);
            int endX = (int)System.Math.Floor(to.x);
            int endY = (int)System.Math.Floor(to.y);
            int endZ = (int)System.Math.Floor(to.z);

            int curX = startX;
            int curY = startY;
            int curZ = startZ;

            float dx = 0.0f;
            float dy = 0.0f;
            float dz = 0.0f;
            if (startX != endX)
                dx = 1.0f / (to.x - from.x);
            if (startY != endY)
                dy = 1.0f / (to.y - from.y);
            if (startZ != endZ)
                dz = 1.0f / (to.z - from.z);

            if (!callback(curX, curY, curZ))                // fill starting point
                return false;

            while (!((curX == endX) && (curY == endY) && (curZ == endZ)))
            {
                if ((curX == endX) && (curY == endY))
                    curZ += signZ;
                else if ((curX == endX) && (curZ == endZ))
                    curY += signY;
                else if ((curY == endY) && (curZ == endZ))
                    curX += signX;
                else
                {
                    int newX = curX + ((signX > 0) ? 1 : 0);
                    int newY = curY + ((signY > 0) ? 1 : 0);
                    int newZ = curZ + ((signZ > 0) ? 1 : 0);
                    float tx = (newX - from.x) * dx;
                    float ty = (newY - from.y) * dy;
                    float tz = (newZ - from.z) * dz;

                    if (curX == endX)
                        tx = float.MaxValue;
                    if (curY == endY)
                        ty = float.MaxValue;
                    if (curZ == endZ)
                        tz = float.MaxValue;

                    // diagonal movement cases
                    if ((System.Math.Abs(tx - ty) < Math.EPSILON) &&
                            (System.Math.Abs(tz - ty) < Math.EPSILON)) // XYZ
                    {
                        curX += signX;
                        curY += signY;
                        curZ += signZ;
                    }
                    else
                    {
                        if (System.Math.Abs(tx - ty) < Math.EPSILON)        // XY
                        {
                            curX += signX;
                            curY += signY;
                        }
                        else if (System.Math.Abs(tx - tz) < Math.EPSILON)   // XZ
                        {
                            curX += signX;
                            curZ += signZ;
                        }
                        else if (System.Math.Abs(ty - tz) < Math.EPSILON)   // YZ
                        {
                            curY += signY;
                            curZ += signZ;
                        }
                        else // usual case
                        {
                            if ((tx < ty) && (tx < tz))
                                curX += signX;                  // X-crossing
                            else if ((ty < tx) && (ty < tz))
                                curY += signY;                  // Y-crossing
                            else
                                curZ += signZ;                   // Z-crossing
                        }
                    }
                }

                if (!callback(curX, curY, curZ))
                    return false;
            }

            return true;
        }
    }
}
