using UnityEngine;

namespace BO.Common
{
	public static class MathExtensions
	{
        //----------------------------------------------------------------------------------
		public static Vector3 ToUnityVector3(this Vec3 vec)
		{
		    Vector3 result;
		    result.x = vec.x;
		    result.y = vec.y;
		    result.z = vec.z;
		    return result;
		}
        
        //----------------------------------------------------------------------------------
		public static Vector2 ToUnityVector2(this Vec2 vec)
		{
		   Vector2 result;
		    result.x = vec.x;
		    result.y = vec.y;
		    return result;
		}
        

        //----------------------------------------------------------------------------------
		public static Vector3 ToUnityVector3(this Vec2 vec)
		{
		    UnityEngine.Vector3 result;
		    result.x = vec.x;
		    result.y = 0f;
		    result.z = vec.y;
		    return result;
		}
		
        //----------------------------------------------------------------------------------
		public static Vec3 ToVec3(this Vector3 v)
		{
			Vec3 result;
		    result.x = v.x;
		    result.y = v.y;
		    result.z = v.z;
		    return result;
		}

    	//----------------------------------------------------------------------------------
		public static Vec2 ToVec2(this Vector3 v)
		{
			Vec2 result;
		    result.x = v.x;
		    result.y = v.z;
		    return result;
		}
		
    	//----------------------------------------------------------------------------------
		public static Vec2 ToVec2(this Vector2 v)
		{
			Vec2 result;
		    result.x = v.x;
		    result.y = v.y;
		    return result;
		}
		
		//----------------------------------------------------------------------------------
		public static Vector2 ToUnityVector2(this Vector3 v)
		{
			Vector2 result;
		    result.x = v.x;
		    result.y = v.z;
		    return result;
		}		
	}
}
