using System;
using System.Globalization;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace BO.Common
{
    [System.Serializable, JsonObject(MemberSerialization.OptIn)]
    public struct Vec3
    {
        [JsonProperty(PropertyName = "X")]
        public float x;
        [JsonProperty(PropertyName = "Y")]
        public float y;
        [JsonProperty(PropertyName = "Z")]
        public float z;

        public static readonly Vec3 zero = new Vec3(0, 0, 0);
        public static readonly Vec3 one = new Vec3(1, 1, 1);
        public static readonly Vec3 forward = new Vec3(0, 0, 1);
        public static readonly Vec3 up = new Vec3(0, 1, 0);
        public static readonly Vec3 down = new Vec3(0, -1, 0);
        public static readonly Vec3 right = new Vec3(1, 0, 0);
        public static readonly Vec3 left = new Vec3(-1, 0, 0);

        public const float EPSILON = 0.0001f;


        //----------------------------------------------------------------------------------
        public Vec3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        //----------------------------------------------------------------------------------
        public void Set(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        //----------------------------------------------------------------------------------
        public void Set(float angle)
        {
            float angleRad = Math.Deg2Rad * angle;
            this.x = (float)System.Math.Sin(angleRad);
            this.y = 0.0f;
            this.z = (float)System.Math.Cos(angleRad);
        }
        
        //----------------------------------------------------------------------------------
        public static bool operator ==(Vec3 a, Vec3 b)
        {
            return (a.x == b.x && a.y == b.y && a.z == b.z);
        }
        //----------------------------------------------------------------------------------
        public static bool operator !=(Vec3 a, Vec3 b)
        {
            return (a.x != b.x || a.y != b.y || a.z != b.z);
        }
        //----------------------------------------------------------------------------------
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Vec3 && Equals((Vec3) obj);
        }
        //----------------------------------------------------------------------------------
        public bool Equals(Vec3 other)
        {
            return x.Equals(other.x) && y.Equals(other.y) && z.Equals(other.z);
        }
        //----------------------------------------------------------------------------------
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = x.GetHashCode();
                hashCode = (hashCode*397) ^ y.GetHashCode();
                hashCode = (hashCode*397) ^ z.GetHashCode();
                return hashCode;
            }
        }
        //----------------------------------------------------------------------------------
        public static Vec3 operator +(Vec3 a, Vec3 b)
        {
            Vec3 result;
            result.x = a.x + b.x;
            result.y = a.y + b.y;
            result.z = a.z + b.z;
            return result;
        }
        //----------------------------------------------------------------------------------
        public static Vec3 operator -(Vec3 a, Vec3 b)
        {
            Vec3 result;
            result.x = a.x - b.x;
            result.y = a.y - b.y;
            result.z = a.z - b.z;
            return result;
        }


        //----------------------------------------------------------------------------------
        public static Vec3 operator -(Vec3 v)
        {
            return new Vec3(-v.x, -v.y, -v.z);
        }
        

        //----------------------------------------------------------------------------------
        public static Vec3 operator *(Vec3 a, float d)
        {
            Vec3 result;
            result.x = a.x * d;
            result.y = a.y * d;
            result.z = a.z * d;
            return result;
        }


        //----------------------------------------------------------------------------------
        public static Vec3 operator *(float d, Vec3 a)
        {
            Vec3 result;
            result.x = a.x * d;
            result.y = a.y * d;
            result.z = a.z * d;
            return result;
        }


        //----------------------------------------------------------------------------------
        public static Vec3 operator /(Vec3 a, float d)
        {
            Vec3 result;
            result.x = a.x / d;
            result.y = a.y / d;
            result.z = a.z / d;
            return result;
        }


        //----------------------------------------------------------------------------------
        public void Normalize()
        {
            double m = CalculateMagnitude();
            if (m > 0)
            {
                x = (float)(x / m);
                y = (float)(y / m);
                z = (float)(z / m);
            }
        }


        //----------------------------------------------------------------------------------
        public Vec3 normalized
        {
            get
            {
                Vec3 norm;
                norm.x = x;
                norm.y = y;
                norm.z = z;
                norm.Normalize();
                return norm;
            }
        }

        //----------------------------------------------------------------------------------
        public Vec2 Vec2 { get { return new Vec2(x, z); } }

        //----------------------------------------------------------------------------------
        private double CalculateMagnitude()
        {
            return System.Math.Sqrt((x * x) + (y * y) + (z * z));
        }


        //----------------------------------------------------------------------------------
        public float magnitude
        {
            get
            {
                return (float)CalculateMagnitude(); // Need double precision in internal calculations
            }
        }


        //----------------------------------------------------------------------------------
        public float sqrMagnitude
        {
            get
            {
                return (x * x) + (y * y) + (z * z);
            }
        }


        //----------------------------------------------------------------------------------
        public static float Dot(Vec3 a, Vec3 b)
        {
            return (a.x * b.x) + (a.y * b.y) + (a.z * b.z);
        }


        //----------------------------------------------------------------------------------
        public static Vec3 Cross(Vec3 a, Vec3 b)
        {
            Vec3 result;
            result.x = (a.y * b.z) - (a.z * b.y);
            result.y = (a.z * b.x) - (a.x * b.z);
            result.z = (a.x * b.y) - (a.y * b.x);
            return result;
        }


        //----------------------------------------------------------------------------------
        public static Vec3 Lerp(Vec3 from, Vec3 to, float t)
        {
            Vec3 result;
            result.x = (t * to.x) + ((1 - t) * from.x);
            result.y = (t * to.y) + ((1 - t) * from.y);
            result.z = (t * to.z) + ((1 - t) * from.z);
            return result;
        }


        public static Vec3 Slerp(Vec3 from, Vec3 to, float t)
        {
            double angle = Angle(from, to) * Math.Deg2Rad;
            if (angle == 0f)
            {
                return from;
            }
            double sinAngle = System.Math.Sin(angle);
            return (float)(System.Math.Sin((1 - t) * angle) / sinAngle) * from + (float)(System.Math.Sin(t * angle) / sinAngle) * to;
        }

        // this is Antoshka's version of Slerp that interpolates only in XZ
        public static Vec3 SlerpXZ(Vec3 from, Vec3 to, float t)
        {
            float angleXZ = Math.SignedAngleXZ(from, to);

            if (System.Math.Abs(angleXZ) < EPSILON)
            {
                return from;
            }

            float angleRotation = angleXZ * t;

            return RotateXZ(from, angleRotation);
        }


        //----------------------------------------------------------------------------------
        public static float Angle(Vec3 from, Vec3 to)
        {
            float cosA = Dot(from.normalized, to.normalized);
            return Math.Acos(cosA) * Math.Rad2Deg;
        }


        //----------------------------------------------------------------------------------
        public static float Distance(Vec3 a, Vec3 b)
        {
            return (a - b).magnitude;
        }


        //----------------------------------------------------------------------------------
        public static float SqrDistance(Vec3 a, Vec3 b)
        {
            return (a - b).sqrMagnitude;
        }


        //----------------------------------------------------------------------------------
        public override String ToString()
        {
            String str = "";
            str += x.ToString("r", CultureInfo.InvariantCulture) + ", " +
                   y.ToString("r", CultureInfo.InvariantCulture) + ", " +
                   z.ToString("r", CultureInfo.InvariantCulture);
            return str;
        }


        // this function rotates CLOCKWISE
        public static Vec3 RotateXZ(Vec3 v, float angle)
        {
            angle *= Math.Deg2Rad;

            double sinAngle = System.Math.Sin(angle);
            double cosAngle = System.Math.Cos(angle);

            float x = (float)(v.x * cosAngle + v.z * sinAngle);
            float z = (float)(-v.x * sinAngle + v.z * cosAngle);

            return new Vec3(x, v.y, z);
        }


        // this function rotates CLOCKWISE
        public static Vec3 RotateYZ(Vec3 v, float angle)
        {
            angle *= Math.Deg2Rad;

            double sinAngle = System.Math.Sin(angle);
            double cosAngle = System.Math.Cos(angle);

            float y = (float)(v.y * cosAngle - v.z * sinAngle);
            float z = (float)(v.y * sinAngle + v.z * cosAngle);

            return new Vec3(v.x, y, z);
        }


        public static float Angle360(Vec3 a, Vec3 b)
        {
            float angle = Math.SignedAngleXZ(a, b);

            if (angle > 0)
            {
                return angle;
            }
            else if (angle < 0)
            {
                return 360 + angle;
            }
            else
            {
                return 0f;
            }
        }

        //----------------------------------------------------------------------------------
        public Vec2 ToVec2()
        {
            Vec2 result;
            result.x = x;
            result.y = z;
            return result;
        }

        // Because SessionServer2ClientInterface is shared between client and server
        public Vec3 ToVec3()
        {
            return this;
        }

        //----------------------------------------------------------------------------------
        public Vec3 DirectionTowards(Vec3 pos) { return (pos - this).normalized; }
        public bool InRange(Vec3 origin, float radius) { return Vec3.SqrDistance(this, origin) <= radius * radius; }

#if !SERVER
        public static implicit operator UnityEngine.Vector3(Vec3 vector)
        {
            return vector.ToUnityVector3();
        }
#endif
    }
}