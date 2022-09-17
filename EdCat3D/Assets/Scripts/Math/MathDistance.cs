namespace BO.Common
{
    public partial class Math
    {
        private static float Distance(float x1, float y1, float x2, float y2)
        {
            float dx = x2 - x1;
            float dy = y2 - y1;
            return (float)System.Math.Sqrt(dx * dx + dy * dy);
        }

        public static float Distance2Box2D(float x, float y, float minX, float minY, float maxX, float maxY)
        {
            if (x < minX)
            {
                if (y < minY)
                    return Distance(x, y, minX, minY);

                if (y < maxY)
                    return minX - x;

                return Distance(x, y, minX, maxY);
            }

            if (x < maxX)
            {
                if (y < minY)
                    return minY - y;

                if (y < maxY)
                    return 0.0f;

                return y - maxY;
            }

            if (y < minY)
                return Distance(x, y, maxX, minY);

            if (y < maxY)
                return x - maxX;

            return Distance(x, y, maxX, maxY);
        }

        public static float Distance2Box2D(Vec2 pnt, Vec2 min, Vec2 max)
        {
            return Distance2Box2D(pnt.x, pnt.y, min.x, min.y, max.x, max.y);
        }

        public static float Distance2Segment(Vec2 pnt, Vec2 segmentStart, Vec2 segmentEnd)
        {
            return ((segmentStart.y - segmentEnd.y) * pnt.x + (segmentEnd.x - segmentStart.x) * pnt.y + (segmentStart.x * segmentEnd.y - segmentEnd.x * segmentStart.y)) / 
                Distance(segmentStart.x, segmentStart.y, segmentEnd.x, segmentEnd.y);
        }
    }
}

