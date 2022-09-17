namespace BO.Common
{
    public partial class Math
    {
        public const float EPSILON = 0.001f;
        public const float ANGLE_EPSILON = 0.1f;
        public const float PI = (float)System.Math.PI;
        public const float PI_SNGL = 3.1415926f;
        public const float PI2_SNGL = 6.2831853f;
        public const double PI_DBL = 3.14159265358979;
        public const double PI2_DBL = 6.28318530717958;
        public const float Deg2Rad = PI / 180;
        public const float Rad2Deg = 180 / PI;
		
        //----------------------------------------------------------------------------------
        // t is in [a, b]
        //----------------------------------------------------------------------------------
        public static float LerpClamped(float a, float b, float fa, float fb, float t)
        {
            if (t <= a)
            {
                return fa;
            }

            if (t >= b)
            {
                return fb;
            }

            float x = (t - a) / (b - a);
            return Lerp(fa, fb, x);
        }


        //----------------------------------------------------------------------------------
        // t is in [0.0 - 1.0]
        //----------------------------------------------------------------------------------
        public static float Lerp(float from, float to, float t)
        {
            return (from + ((to - from) * t));
        }


        //----------------------------------------------------------------------------------
        // Interpolates angle by shortest way
        // t is in [0.0 - 1.0]
        //----------------------------------------------------------------------------------
        public static float LerpAngle(float a, float b, float t)
        {
            float delta = System.Math.Abs(b - a);

            if (a > b && delta > 180f)
            {
                float tmp = (360f - a + b);
                float result = a + Math.Lerp(0f, tmp, t);

                if (result > 360f)
                {
                    result -= 360f;
                }

                return result;
            }

            if (a < b && delta > 180f)
            {
                float tmp = (360f - b + a);
                float result = a - Math.Lerp(0f, tmp, t);

                if (result < 0f)
                {
                    result += 360f;
                }

                return result;
            }

            return Math.Lerp(a, b, t);
        }


        public static float NormalizeAngle(float a)
        {
            var nrm = (double)a / PI2_DBL;
            nrm -= System.Math.Floor(nrm);
            return (float)(nrm * PI2_DBL);
        }


        public static float AngularDistance(float a, float b)
        {
            var na = NormalizeAngle(a);
            var nb = NormalizeAngle(b);

            var delta = na - nb;

            if (delta > PI_SNGL)
                return PI2_SNGL - delta;
            
            if (delta < -PI_SNGL)
                return PI2_SNGL + delta;

            return delta;
        }


        //----------------------------------------------------------------------------------
        public static float SignedAngleXZ(Vec3 a, Vec3 b)
        {
            if (Vec3.Dot(a, b) == -1f)
            {
                return 180f;
            }

            float angle = Vec3.Angle(a, b);
            if (Vec3.Cross(a, b).y < 0)
            {
                return -angle;
            }

            return angle;
        }


        //----------------------------------------------------------------------------------
        public static float SignedAngleYZ(Vec3 a, Vec3 b)
        {
            if (Vec3.Dot(a, b) == -1f)
            {
                return 180f;
            }

            float angle = Vec3.Angle(a, b);
            if (Vec3.Cross(a, b).x < 0)
            {
                return -angle;
            }

            return angle;
        }


        //----------------------------------------------------------------------------------
        public static float AngleXZ(Vec3 a, Vec3 b)
        {
            a.y = b.y = 0f;

            if (Vec3.Dot(a, b) == -1f)
            {
                return 180f;
            }

            float angle = Vec3.Angle(a, b);
            if (Vec3.Cross(a, b).y < 0)
            {
                return 360 - angle;
            }

            return angle;
        }


        //----------------------------------------------------------------------------------
        public static float Acos(float cosA)
        {
            // Protection from round-up error
            if (cosA > 0.999999f)
            {
                cosA = 1f;
            }
            else if (cosA < -0.999999f)
            {
                cosA = -1f;
            }

            return (float)System.Math.Acos(cosA);
        }


        //----------------------------------------------------------------------------------
        public static float DirectionToAlpha(Vec2 direction)
        {
            return Vec2.Angle360(Vec2.forward, direction);
        }


        //----------------------------------------------------------------------------------
        public static Vec2 AlphaToDirection(float alpha)
        {
            var vec = Vec2.zero;
            vec.Set(alpha);
            return vec;
        }


        //----------------------------------------------------------------------------------
        public static Vec2 AlphaRadToDirection(float alpha)
        {
            var vec = Vec2.zero;
            vec.SetRad(alpha);
            return vec;
        }

        
        //----------------------------------------------------------------------------------
        // Generates CW rotated poly

        // x,y - pos
        // sizeAlong - size of box along given direction
        // sizeOrto - size of box ortogonal given direction
        public static void MakeRotated2DBox(Vec2[] poly, float x, float y, float sizeAlong, float sizeOrto, float dx, float dy)
        {
            sizeAlong *= 0.5f;
            sizeOrto *= 0.5f;

            poly[0].x = x + sizeAlong * dx - sizeOrto * dy;
            poly[0].y = y + sizeAlong * dy + sizeOrto * dx;

            poly[1].x = x - sizeAlong * dx - sizeOrto * dy;
            poly[1].y = y - sizeAlong * dy + sizeOrto * dx;

            poly[2].x = x - sizeAlong * dx + sizeOrto * dy;
            poly[2].y = y - sizeAlong * dy - sizeOrto * dx;

            poly[3].x = x + sizeAlong * dx + sizeOrto * dy;
            poly[3].y = y + sizeAlong * dy - sizeOrto * dx;

        }

        
        public static Vec2[] MakeRotated2DBox(float x, float y, float sizeAlong, float sizeOrto, float dx, float dy)
        {
            var poly = new Vec2[4];
            MakeRotated2DBox(poly, x, y, sizeAlong, sizeOrto, dx, dy);
            return poly;
        }

        // x,y - pos
        // sizeAlong - size of box along given direction
        // sizeOrto  - size of box ortogonal given direction
        // angle     - zero angle results in (1,0) vector
        public static void MakeRotated2DBox(Vec2[] poly, float x, float y, float sizeAlong, float sizeOrto, float angle)
        {
            angle *= Deg2Rad;
            MakeRotated2DBox(poly, x, y, sizeAlong, sizeOrto, (float) System.Math.Cos(angle),
                             -(float) System.Math.Sin(angle));
        }


        public static Vec2[] MakeRotated2DBox(float x, float y, float sizeAlong, float sizeOrto, float angle)
        {
            var poly = new Vec2[4];
            MakeRotated2DBox(poly, x, y, sizeAlong, sizeOrto, angle);
            return poly;
        }

        
        //----------------------------------------------------------------------------------
        public static bool IsZero(float v)
        {
            return IsZero(v, EPSILON);
        }


        public static float Square(float x)
        {
            return x * x;
        }

        public static double Square(double x)
        {
            return x * x;
        }

        //----------------------------------------------------------------------------------
        public static bool IsZero(float v, float eps)
        {
            if (v > 0f)
            {
                return v < eps;
            }
            return v > -eps;
        }

        
        //----------------------------------------------------------------------------------
        public static float Clamp(float min, float max, float value)
        {
            if (value < min)
                return min;

            if (value > max)
                return max;

            return value;
        }


        //----------------------------------------------------------------------------------
        public static int Clamp(int min, int max, int value)
        {
            if (value < min)
                return min;

            if (value > max)
                return max;

            return value;
        }

        
        //----------------------------------------------------------------------------------
        public static bool IsOvershoot(Vec2 direction, Vec2 goalDirection, Vec2 newDirection)
        {
            var dxg = Vec2.Cross(direction, goalDirection);
            var nxg = Vec2.Cross(newDirection, goalDirection);
            return dxg * nxg <= 0.0f;
        }


        //----------------------------------------------------------------------------------
        public static Vec2 CalculateRandomPointInCircle(Vec2 origin, float radius, Random random)
        {
            var rndRadius = random.NextFloat(0f, radius);
            var rndAngle  = random.NextFloat(0f, 2f * Math.PI);
            
            return origin + Vec2.RotateRad(Vec2.forward, rndAngle) * rndRadius;
            
        }
    }
}

