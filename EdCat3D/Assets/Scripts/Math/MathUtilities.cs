using System;
namespace BO.Common
{
	public class Spline1D : Spline
	{
		private CubicCoeffs[] c;		
		
		public Spline1D(float[] x)
		{
			c = Math.CalcNaturalCubic(x);
			numPoints = c.Length;
			
			CalculateDistances();
		}
		
		
		public float GetValue(int i, float p)
		{
			return c[i].GetValue(p);
		}
		
		
		public float GetValue(float m)
		{
			if (m < 0.0f)
				return GetValue(0, 0.0f);
			
			if (m > totalLength - Math.EPSILON)
				return GetValue(numPoints - 1, 1.0f);
			
			FindHint(m);
				
			return GetValue(hint, GetT(m));
		}		
		
		
		private void CalculateDistances()
		{
			segment = new float[numPoints][];
			fullD = new float[numPoints + 1];
			fullD[0] = 0.0f;
			
			for (int i = 0; i < numPoints; i++)
			{
				float s = 0.0f;
				segment[i] = new float[numSegments];
				for (int j = 0; j < numSegments; j++)
				{
					float segmentLength = System.Math.Abs(c[i].GetValue((float)(j + 1) / numSegments) - c[i].GetValue((float)j / numSegments));
					segment[i][j] = segmentLength;
					s += segmentLength;
				}
				
				fullD[i + 1] = fullD[i] + s;
			}
			
			totalLength = fullD[numPoints];
		}
	}
	
	
	public partial class Spline2D : Spline
	{
		private CubicCoeffs[] cx;
		private CubicCoeffs[] cz;
		
		public Spline2D(Vec2[] v)
		{
			float[] x = new float[v.Length];
			float[] z = new float[v.Length];
			for (int i = 0; i < v.Length; i++)
			{
				x[i] = v[i].x;
				z[i] = v[i].y;
			}
			
			cx = Math.CalcNaturalCubic(x);
			cz = Math.CalcNaturalCubic(z);
			numPoints = cx.Length;
			
			CalculateDistances();
		}
		
		
		public Spline2D(float[]x, float[] z)
		{
			cx = Math.CalcNaturalCubic(x);
			cz = Math.CalcNaturalCubic(z);
			numPoints = cx.Length;
			
			CalculateDistances();
		}
		
		
		public Vec2 GetValue(int i, float p)
		{
			if (i < 0 || i >= cx.Length )
				return Vec2.zero;
			
			Vec2 v = new Vec2(cx[i].GetValue(p), cz[i].GetValue(p));
			return v;
		}
		
		
		public Vec2 GetValue(float m)
		{
			if (m < 0.0f)
				return GetValue(0, 0.0f);
			
			if (m > totalLength - Math.EPSILON)
				return GetValue(numPoints - 1, 1.0f);
			
			FindHint(m);
				
			return GetValue(hint, GetT(m));
		}		
		
		
		private void CalculateDistances()
		{
			segment = new float[numPoints][];
			fullD = new float[numPoints + 1];
			fullD[0] = 0.0f;
			
			for (int i = 0; i < numPoints; i++)
			{
				float s = 0.0f;
				segment[i] = new float[numSegments];
				for (int j = 0; j < numSegments; j++)
				{
					Vec2 v1 = GetValue(i, (float)j / numSegments);
					Vec2 v2 = GetValue(i, (float)(j + 1) / numSegments);
					float segmentLength = (v2 - v1).magnitude;
					segment[i][j] = segmentLength;
					s += segmentLength;
				}
				
				fullD[i + 1] = fullD[i] + s;
			}
			
			totalLength = fullD[numPoints];
		}
	}
	
	
	public partial class Spline3D : Spline
	{
		private CubicCoeffs[] cx;
		private CubicCoeffs[] cy;
		private CubicCoeffs[] cz;
		
		public Spline3D(Vec3[] v)
		{
			float[] x = new float[v.Length];
			float[] y = new float[v.Length];
			float[] z = new float[v.Length];
			for (int i = 0; i < v.Length; i++)
			{
				x[i] = v[i].x;
				y[i] = v[i].y;
				z[i] = v[i].z;
			}
			
			cx = Math.CalcNaturalCubic(x);
			cy = Math.CalcNaturalCubic(y);
			cz = Math.CalcNaturalCubic(z);
			numPoints = cx.Length;
			
			CalculateDistances();
		}
		
		
		public Spline3D(float[]x, float[]y, float[] z)
		{
			cx = Math.CalcNaturalCubic(x);
			cy = Math.CalcNaturalCubic(y);
			cz = Math.CalcNaturalCubic(z);
			numPoints = cx.Length;
			
			CalculateDistances();
		}
		
		
		public Vec3 GetValue(int i, float p)
		{
			Vec3 v = new Vec3(cx[i].GetValue(p), cy[i].GetValue(p), cz[i].GetValue(p));
			return v;
		}
		
		
		public Vec3 GetValue(float m)
		{
			if (m < 0.0f)
				return GetValue(0, 0.0f);
			
			if (m > totalLength - Math.EPSILON)
				return GetValue(numPoints - 1, 1.0f);
			
			FindHint(m);
				
			return GetValue(hint, GetT(m));
		}		
		
		
		private void CalculateDistances()
		{
			segment = new float[numPoints][];
			fullD = new float[numPoints + 1];
			fullD[0] = 0.0f;
			
			for (int i = 0; i < numPoints; i++)
			{
				float s = 0.0f;
				segment[i] = new float[numSegments];
				for (int j = 0; j < numSegments; j++)
				{
					Vec3 v1 = GetValue(i, (float)j / numSegments);
					Vec3 v2 = GetValue(i, (float)(j + 1) / numSegments);
					float segmentLength = (v2 - v1).magnitude;
					segment[i][j] = segmentLength;
					s += segmentLength;
				}

				fullD[i + 1] = fullD[i] + s;
			}
			
			totalLength = fullD[numPoints];
		}
	}
	
	
	public abstract class Spline
	{
		public class CubicCoeffs
		{
			float a;
			float b;
			float c;
			float d;        
		
			public CubicCoeffs(float a, float b, float c, float d)
			{
		    	this.a = a;
		    	this.b = b;
		    	this.c = c;
		    	this.d = d;
		  	}
		  
		  	public float GetValue(float u) 
		  	{
		    	return (((d * u) + c) * u + b) * u + a;
		 	}	
		}
		
		protected int numPoints;
		protected float[][] segment;				// interval-segments lengths
		protected float[] fullD;				// lengths from the beginning
		protected int hint = 0;
		protected float totalLength = 0.0f;
		protected readonly int numSegments = 100;

		
		protected void FindHint(float m)
		{
			if ((m < fullD[hint]) || (m > fullD[hint + 1]))
			{
				for (int i = 1; i <= numPoints; i++)
				{
					if (m < fullD[i])
					{
						hint = i - 1;
						break;
					}
				}
			}
		}
		
		
		// return position of interpolation in a spline 
		public float GetInterpolationPosition(float m)
		{
			if (m < 0.0f)
				return 0.0f;
			
			if (m > totalLength - Math.EPSILON)
				return (numPoints - 1 + 0.999f);
			
			FindHint(m);
			float interval = (float)hint + GetT(m);
			return interval;
		}
		
		
		// return an interval that is in m distance along the spline from its start 
		public int GetInterpolationInterval(float m)
		{
			if (m < 0.0f)
				return 0;
			
			if (m > totalLength - Math.EPSILON)
				return (numPoints - 1);
			
			FindHint(m);
			return hint;
		}
		
		
		// return totalLength of a spline
		public float GetLength()
		{
			return totalLength;
		}
		
		
		// return length along a spline to point number i
		public float GetLength(int i)
		{
			if (i > numPoints)
				return totalLength;
			else
				if (i < 0)
					return 0.0f;
			else
				return fullD[i];
		}
		
		
		// returns interpolation coefficient
		protected float GetT(float m)
		{
 			float dist = fullD[hint];
			float t = 0.0f;
			for (int i = 0; i < numSegments; i++)
			{
				dist += segment[hint][i];
				
				if (dist >= m)
				{
					t = (i + (m - dist + segment[hint][i]) / segment[hint][i]) / numSegments;
					break;
				}
			}

			return t;
		}
	}
	
	
    public partial class Math
    {
		public static Spline.CubicCoeffs[] CalcNaturalCubic(float[] x) 
		{
			int n = x.Length - 1;
			
			float[] gamma = new float[n+1];
			float[] delta = new float[n+1];
			float[] D = new float[n+1];
		
			gamma[0] = 0.5f;
			
			for (int i = 1; i < n; i++) 
			{
				gamma[i] = 1.0f / (4.0f - gamma[i - 1]);
			}
			
			gamma[n] = 1.0f / (2.0f - gamma[n - 1]);
			
			delta[0] = 3.0f * (x[1] - x[0]) * gamma[0];
			
			for (int i = 1; i < n; i++) 
			{
				delta[i] = (3.0f * (x[i + 1] - x[i - 1]) - delta[i - 1]) * gamma[i];
			}
			
			delta[n] = (3.0f * (x[n] - x[n - 1]) - delta[n - 1]) * gamma[n];
			
			D[n] = delta[n];
			
			for (int i = n - 1; i >= 0; i--) 
			{
				D[i] = delta[i] - gamma[i] * D[i + 1];
			}
			
			Spline.CubicCoeffs[] C = new Spline.CubicCoeffs[n];
			
			for (int i = 0; i < n; i++)
			{
				C[i] = new Spline.CubicCoeffs(
				             (float)x[i],
				             D[i],
				             3.0f * (x[i + 1] - x[i]) - 2.0f * D[i] - D[i + 1],
					   		 2.0f * (x[i] - x[i + 1]) + D[i] + D[i + 1]);
			}
				
			return C;
		}
		
		public static bool FindInterpolationCoeffs(Vec2[] points, Vec2 p, out float x, out float y, float tol)
		{
			/*
			 * http://stackoverflow.com/questions/808441/inverse-bilinear-interpolation
			*/
			float A = Vec2.Cross(points[0] - p, points[0] - points[3]);
			float B = 0.5f * (Vec2.Cross(points[0] - p, points[1] - points[2]) +
			                  Vec2.Cross(points[1] - p, points[0] - points[3]));
			float C = Vec2.Cross(points[1] - p, points[1] - points[2]);
	
			float E = A - 2.0f * B + C;

			if (System.Math.Abs(E) < Math.EPSILON)
			{
				if (System.Math.Abs(A - C) < Math.EPSILON)
				{
					x = 0.5f;
					if (CalcYInterpCoeff(points, p, x, out y, tol))
					    return true;
				}
	
				x = A / (A - C);
				if ((x >= -tol) && (x <= 1.0f + tol))
					if (CalcYInterpCoeff(points, p, x, out y, tol))
					    return true;
			}
			else
			{
				float D = (float)System.Math.Sqrt(B * B - A * C);
				
				x = (A - B + D) / E;
				if ((x >= -tol) && (x <= 1.0f + tol))
					if (CalcYInterpCoeff(points, p, x, out y, tol))
					    return true;
				
				x = (A - B - D) / E;
				if ((x >= -tol) && (x <= 1.0f + tol))
					if (CalcYInterpCoeff(points, p, x, out y, tol))
					    return true;
			}
			
			x = 0.0f;
			y = 0.0f;
			return false;
		}
		
	
		private static bool CalcYInterpCoeff(Vec2[] points, Vec2 p, float x, out float y, float tol)
		{
			y = 0.0f;
			
			float dx = (1.0f - x) * (points[0].x - points[3].x) + x * (points[1].x - points[2].x);
			float dy = (1.0f - x) * (points[0].y - points[3].y) + x * (points[1].y - points[2].y);
			if ((System.Math.Abs(dx) < Math.EPSILON) && (System.Math.Abs(dy) < Math.EPSILON))
				return false;
			
			if (System.Math.Abs(dx) > System.Math.Abs(dy))
				y = ((1.0f - x) * (points[0].x - p.x) + x * (points[1].x - p.x) ) / dx;
			else
				y = ((1.0f - x) * (points[0].y - p.y) + x * (points[1].y - p.y) ) / dy;
	
			if ((y >= -tol) && (y <= 1.0f + tol))
				return true;
		
			return false;
		}
    }
}

