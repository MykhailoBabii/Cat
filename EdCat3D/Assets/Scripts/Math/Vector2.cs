using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace BO.Common
{
    [System.Serializable, JsonObject(MemberSerialization.OptIn)]
    public struct Vec2
    {
        [JsonProperty(PropertyName = "X")]
        public float x;
        [JsonProperty(PropertyName = "Y")]
        public float y;

        public static readonly Vec2 zero = new Vec2(0, 0);
        public static readonly Vec2 one = new Vec2(1, 1);
        public static readonly Vec2 forward = new Vec2(0, 1);
        public static readonly Vec2 back = new Vec2(0, -1);
        public static readonly Vec2 right = new Vec2(1, 0);
        public static readonly Vec2 left = new Vec2(-1, 0);

        public const float EPSILON = 0.0001f;

        [JsonIgnoreAttribute]
        public float this[int index] {
            get {
                if (index == 0) 
                {
                    return x;
                } else
                {
                    return y;
                }
            }

            set {
                if (index == 0) 
                {
                    x = value;
                } else 
                {
                    y = value;
                }
            }
        }


        //----------------------------------------------------------------------------------
        public Vec2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }


        //----------------------------------------------------------------------------------
        public void Set(float x, float y)
        {
            this.x = x;
            this.y = y;
        }


        //----------------------------------------------------------------------------------
        public void Set(float angle)
        {
            var angleRad = Math.Deg2Rad * angle;
            x = (float)System.Math.Sin(angleRad);
            y = (float)System.Math.Cos(angleRad);
        }


        //----------------------------------------------------------------------------------
        public void SetRad(float angle)
        {
            x = (float)System.Math.Sin(angle);
            y = (float)System.Math.Cos(angle);
        }

        
        //----------------------------------------------------------------------------------
        public static Vec2 operator +(Vec2 a, Vec2 b)
        {
            Vec2 result;
            result.x = a.x + b.x;
            result.y = a.y + b.y;
            return result;
        }


        //----------------------------------------------------------------------------------
        public static Vec2 operator -(Vec2 a, Vec2 b)
        {
            Vec2 result;
            result.x = a.x - b.x;
            result.y = a.y - b.y;
            return result;
        }


        //----------------------------------------------------------------------------------
        public static Vec2 operator -(Vec2 a)
        {
            Vec2 result;
            result.x = -a.x;
            result.y = -a.y;
            return result;
        }

        //----------------------------------------------------------------------------------
        public static Vec2 operator *(Vec2 a, float d)
        {
            Vec2 result;
            result.x = a.x * d;
            result.y = a.y * d;
            return result;
        }


        //----------------------------------------------------------------------------------
        public static Vec2 operator *(float d, Vec2 a)
        {
            Vec2 result;
            result.x = a.x * d;
            result.y = a.y * d;
            return result;
        }


        //----------------------------------------------------------------------------------
        public static Vec2 operator /(Vec2 a, float d)
        {
            Vec2 result;
            result.x = a.x / d;
            result.y = a.y / d;
            return result;
        }


        //----------------------------------------------------------------------------------
        public void Normalize()
        {
            float m = magnitude;
            x = x / m;
            y = y / m;
        }


        //----------------------------------------------------------------------------------
        public Vec2 normalized
        {
            get
            {
                Vec2 norm;
                norm.x = x;
                norm.y = y;
                norm.Normalize();
                return norm;
            }
        }

        //----------------------------------------------------------------------------------
        public Vec3 Vec3 { get { return new Vec3(x, 0.0f, y); } }

        //----------------------------------------------------------------------------------
        public float magnitude
        {
            get { return (float)System.Math.Sqrt((x * x) + (y * y)); }
        }


        //----------------------------------------------------------------------------------
        public float sqrMagnitude
        {
            get { return (x * x) + (y * y); }
        }


        //----------------------------------------------------------------------------------
        public static float Dot(Vec2 a, Vec2 b)
        {
            return (a.x * b.x) + (a.y * b.y);
        }


        //----------------------------------------------------------------------------------
        public static float Cross(Vec2 a, Vec2 b)
        {
            return (a.y * b.x) - (a.x * b.y);
        }


        //----------------------------------------------------------------------------------
        public static Vec2 Lerp(Vec2 from, Vec2 to, float t)
        {
            Vec2 result;
            result.x = (t * to.x) + ((1 - t) * from.x);
            result.y = (t * to.y) + ((1 - t) * from.y);
            return result;
        }


        //----------------------------------------------------------------------------------
        public static Vec2 Slerp(Vec2 from, Vec2 to, float t)
        {
            float angle = SignedAngle(from, to);
            if (System.Math.Abs(angle) < EPSILON)
            {
                return from;
            }

            return RotateDeg(from, angle * t);
        }


        //----------------------------------------------------------------------------------
        public static float Angle(Vec2 from, Vec2 to)
        {
            float cosA = Dot(from.normalized, to.normalized);
            return Math.Acos(cosA) * Math.Rad2Deg;
        }


        //----------------------------------------------------------------------------------
        public static float SignedAngle(Vec2 from, Vec2 to)
        {
            if (Vec2.Dot(from, to) == -1f)
            {
                return 180f;
            }

            float angle = Vec2.Angle(from, to);
            if (Vec2.Cross(from, to) < 0)
            {
                return -angle;
            }

            return angle;
        }


        //----------------------------------------------------------------------------------
        public static float Angle360(Vec2 from, Vec2 to)
        {
            if (Vec2.Dot(from, to) == -1f)
            {
                return 180f;
            }

            float angle = Vec2.Angle(from, to);
            if (Vec2.Cross(from, to) < 0)
            {
                return 360 - angle;
            }

            return angle;
        }


        //----------------------------------------------------------------------------------
        public static float Distance(Vec2 a, Vec2 b)
        {
            return (a - b).magnitude;
        }


        //----------------------------------------------------------------------------------
        public static float SqrDistance(Vec2 a, Vec2 b)
        {
            return (a - b).sqrMagnitude;
        }

        // this function rotates CLOCKWISE and angle starts from Y axis
        public static Vec2 RotateRad(Vec2 v, float angle)
        {
            double sinAngle = System.Math.Sin(angle);
            double cosAngle = System.Math.Cos(angle);

            Vec2 res;
            res.x = (float)(v.x * cosAngle + v.y * sinAngle);
            res.y = (float)(-v.x * sinAngle + v.y * cosAngle);
            return res;
        }


        public static Vec2 RotateDeg(Vec2 v, float angle)
        {
            return RotateRad(v, angle * Math.Deg2Rad);
        }


        public static Vec2 RotateAroundRad(Vec2 v, Vec2 origin, float angle)
        {
            return RotateRad(v - origin, angle) + origin;
        }


        public static Vec2 RotateAroundDeg(Vec2 v, Vec2 origin, float angle)
        {
            return RotateAroundRad(v, origin, angle * Math.Deg2Rad);
        }


        //----------------------------------------------------------------------------------
        public static Vec2 MeasureOut(Vec2 start, Vec2 end, float dist)
        {
            var angle = System.Math.Atan((end.y - start.y) / (end.x - start.x));
            var dx = dist * (float)System.Math.Cos(angle);
            var dy = dist * (float)System.Math.Sin(angle);

            return new Vec2(start.x - dx, start.y - dy);
        }


        //----------------------------------------------------------------------------------
        public override String ToString()
        {
            var str = "";
            str += x.ToString("r", CultureInfo.InvariantCulture) + ", " + y.ToString("r", CultureInfo.InvariantCulture);
            return str;
        }

        //----------------------------------------------------------------------------------
		public Vec3 ToVec3()
		{
		    Vec3 result;
		    result.x = x;
		    result.y = 0f;
		    result.z = y;
		    return result;
		}

        public Vec2 TransformToLocal(Vec2 center, float alpha) { return RotateDeg(this - center, -alpha); }
        public Vec2 TransformToWorld(Vec2 center, float alpha) { return RotateDeg(this, alpha) + center; }
        public bool InRange(Vec2 origin, float radius) { return Vec2.SqrDistance(this, origin) <= radius * radius; }


        //----------------------------------------------------------------------------------
        public Vec2 DirectionTowards(Vec2 pos)
        {
            if (pos.x == x && pos.y == y)
                return forward;

            return (pos - this).normalized;
        }

        
        //----------------------------------------------------------------------------------
        public bool InRange(Vec2 origin, float minRadius, float maxRadius)
        {
            var dist = SqrDistance(this, origin);

            if (dist > maxRadius * maxRadius)
                return false;

            if (dist < minRadius * minRadius)
                return false;

            return true;
        }


        //----------------------------------------------------------------------------------
        public bool Equals(Vec2 other)
        {
            return x.Equals(other.x) && y.Equals(other.y);
        }


        //----------------------------------------------------------------------------------
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Vec2 && Equals((Vec2) obj);
        }


        //----------------------------------------------------------------------------------
        public override int GetHashCode()
        {
            unchecked
            {
                return (x.GetHashCode()*397) ^ y.GetHashCode();
            }
        }


        //----------------------------------------------------------------------------------
        public static bool operator ==(Vec2 a, Vec2 b)
        {
            return (a.x == b.x && a.y == b.y);
        }


        //----------------------------------------------------------------------------------
        public static bool operator !=(Vec2 a, Vec2 b)
        {
            return (a.x != b.x || a.y != b.y);
        }
    }
}