using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GraphicsUtility
{
    public class CellMesh
    {
        public int Count;
        private readonly int sizeCollection;
        private readonly List<Vector3> vertex;
        private readonly Dictionary<uint, Dictionary<uint, int>> cells;

        public CellMesh(int size)
        {
            Count = 0;
            sizeCollection = size;
            vertex = new List<Vector3>(sizeCollection);
            cells = new Dictionary<uint, Dictionary<uint, int>>(sizeCollection);
        }

        public Vector3 this[uint x, uint z]
        {
            get { return vertex[cells[x][z]]; }
            set
            {
                if (cells.ContainsKey(x) && cells[x].ContainsKey(z))
                {
                    vertex[cells[x][z]] = value;
                }
                else
                {
                    if (!cells.ContainsKey(x))
                    {
                        vertex.Add(value);
                        cells.Add(x, new Dictionary<uint, int>(sizeCollection) { { z, Count } });
                        Count++;
                    }
                    else
                    {
                        vertex.Add(value);
                        cells[x].Add(z, Count);
                        Count++;
                    }
                }
            }
        }

        public int GetIndex(uint x, uint z)
        {
            return cells[x][z];
        }
        public Vector3[] GetVertex()
        {
            return vertex.ToArray();
        }
    }
    [System.Serializable]
    public class Spline
    {
        private List<Vector3> points = new List<Vector3>();
        private List<Vector3> spline = new List<Vector3>();

        private int addsPoint = 10;

        public int Quality
        {
            get { return addsPoint; }
            set
            {
                addsPoint = Mathf.Max(value, 1);
                addsPoint = Mathf.Min(value, 1000);
                if (points.Count >= 2)
                {
                    this.spline = CalcSpline(points);
                }
            }
        }
        public Spline()
        {
            this.addsPoint = 10;
        }
        public Spline(Vector3[] Points)
        {
            this.points = Points.ToList();
            this.addsPoint = 10;
            pointChange();
        }
        public Spline(Vector3 Point)
        {
            this.points = new List<Vector3> { Point};
            this.addsPoint = 10;
            pointChange();
        }
        public Spline(Vector3[] Points, int quality)
        {
            this.points = Points.ToList();
            this.addsPoint = quality;
            pointChange();
        }
        public Spline(List<Vector3> Points, int quality)
        {
            this.points = Points;
            this.addsPoint = quality;
            pointChange();
        }
        public Vector3 this[uint i]
        {
            get { return spline[(int)i]; }
        }

        public List<Vector3> GetPoints()
        {
            return points;
        }
        public void SetPoints(List<Vector3> points)
        {
            this.points = points;
            pointChange();
        }
        public int Count
        {
            get { return spline.Count; }
        }
        public void Add(Vector3 newPoint)
        {
            points.Add(newPoint);
            pointChange();
        }
        private void pointChange()
        {
            if (points.Count >= 2)
                this.spline = CalcSpline(points);
            else
                this.spline = points;
        }
        private List<Vector3> CalcSpline(List<Vector3> point)
        {
            Vector3[] _points = new Vector3[point.Count + 2];
            _points[0] = point[0] + point[0] - point[1];
            point.CopyTo(_points, 1);
            _points[point.Count + 1] = point[point.Count - 1] + point[point.Count - 1] - point[point.Count - 2];

            float step = 1.0f/(addsPoint + 1.0f);
            Vector3[] toPoint4 = new Vector3[4];
            List<Vector3> spline = new List<Vector3>();
            for (int i = 1; i < _points.Length - 2; i++)
            {
                float lenght = step;
                spline.Add(_points[i]);
                while (lenght < 1.0f - Mathf.Epsilon)
                {
                    System.Array.Copy(_points, i - 1, toPoint4, 0, 4);
                    spline.Add(Interpolator.GetPoint(new Interpolator.Point4<Vector3>(toPoint4), lenght));
                    lenght += step;
                }
            }
            spline.Add(_points[_points.Length - 2]);

            return spline;
        }

        public static implicit operator Vector3[](Spline sp)
        {
            return sp.spline.ToArray();
        }
        public static implicit operator List<Vector3>(Spline sp)
        {
            return sp.spline;
        }
        public List<Vector3> GetPointsInSpline(float stepLength)
        {
            if (spline.Count > 0)
            {
                List<Vector3> points = new List<Vector3>();
                float currentLegth = 0;
                float pointStep = 0;
                Vector3 PointToPoint = Vector3.zero;
                int i = 0;
                //Vector3 currentPoint;
                points.Add(spline[i]);
                while (i < spline.Count - 2)
                {
                    //currentPoint = spline[i];
                    currentLegth += stepLength;
                    while (currentLegth>=0 && i < spline.Count-2)
                    {
                        PointToPoint = spline[i] - spline[i + 1];
                        pointStep = PointToPoint.magnitude;
                        currentLegth -= pointStep;
                        i++;
                    }
                    points.Add(spline[i] + PointToPoint * (-currentLegth / pointStep));
                }
                return points;
            }
            else
                return null;
        }
    }

    public static class Interpolator
    {
        private static float Cubic(Point4<float> p, float x)
        {
            //P0 * t^3 + P1 * 3*(1-t)*t^2 + P2 * 3*(1-t)^2*t + P3 * (1-t)^3
            return calc(p, x);
        }

        private static Vector2 BiCubic(Point4<Vector2> p, float x, float y)
        {
            Point4<float> px = new Point4<float>(p.a.x, p.b.x, p.c.x, p.d.x);
            Point4<float> py = new Point4<float>(p.a.y, p.b.y, p.c.y, p.d.y);

            float X = calc(px, x);
            float Y = calc(py, y);

            return new Vector2(X, Y);
        }

        private static Vector3 TriCubic(Point4<Vector3> p, float x, float y, float z)
        {
            Point4<float> px = new Point4<float>(p.a.x, p.b.x, p.c.x, p.d.x);
            Point4<float> py = new Point4<float>(p.a.y, p.b.y, p.c.y, p.d.y);
            Point4<float> pz = new Point4<float>(p.a.z, p.b.z, p.c.z, p.d.z);

            float X = calc(px, x);
            float Y = calc(py, y);
            float Z = calc(pz, z);

            return new Vector3(X, Y, Z);
        }

        private static float calc(Point4<float> p, float x)
        {
            /*float A = (p.c- p.b) - (p.a - p.b);
            float B = (p.a-p.b) - A;
            float C = p.c - p.a;
            float D = p.b;

            Debug.Log( A * Mathf.Pow(x, 3) + B * Mathf.Pow(x, 2) + C * x + D);
            Debug.Log(p.b + 0.5f * x * (p.c - p.a + x * (2.0f * p.a - 5.0f * p.b + 4.0f * p.c - p.d + x * (3.0f * (p.b - p.c) + p.d - p.a))));*/
            return p.b + 0.5f*x*(p.c - p.a + x*(2.0f*p.a - 5.0f*p.b + 4.0f*p.c - p.d + x*(3.0f*(p.b - p.c) + p.d - p.a)));
        }

        /*public static float Bicubic(float[][] p, float x, float y)
        {
            float[] arr = new float[4];
            arr[0] = Cubic(p[0], y);
            arr[1] = Cubic(p[1], y);
            arr[2] = Cubic(p[2], y);
            arr[3] = Cubic(p[3], y);
            return Cubic(arr, x);
        }
        public static float Tricubic(float[][][] p, float x, float y, float z)
        {
            float[] arr = new float[4];
            arr[0] = Bicubic(p[0], y, z);
            arr[1] = Bicubic(p[1], y, z);
            arr[2] = Bicubic(p[2], y, z);
            arr[3] = Bicubic(p[3], y, z);
            return Cubic(arr, x);
        }*/

        /// <summary>
        /// Возвращает точку сплайна на расстоянии 0-1 между 2 и 3 точкой
        /// </summary>
        /// <param name="points">4 точки</param>
        /// <param name="x">Расстояние по x</param>
        /// <param name="y">Расстояние по y</param>
        /// <param name="z">Расстояние по z</param>
        /// <returns>Координаты точки</returns>
        public static Vector3 GetPoint(Point4<Vector3> points, float x, float y, float z)
        {
            //Point4<Vector3> p = new Point4<Vector3>(points[0], points[1], points[2], points[3]);
            //Vector3 point = Vector3.Scale((points.c - points.b), TriCubic(points, x, y, z));
            return TriCubic(points, x, y, z);
        }

        public static Vector3 GetPoint(Point4<Vector3> points, Vector3 vec)
        {
            //Vector3 point = Vector3.Scale((points.c - points.b), TriCubic(points, vec.x, vec.y, vec.z));
            return GetPoint(points, vec.x, vec.y, vec.z);
        }

        public static Vector3 GetPoint(Point4<Vector3> points, float f)
        {
            //Vector3 point = Vector3.Scale((points.c - points.b), TriCubic(points, f, f, f));
            return GetPoint(points, f, f, f);
        }

        /*//private static Vector3Vector3 operator Vector3
        public static Vector3 FindPoint(List<Vector3> points, Vector3 vec)
        {
            Point4<Vector3> p = new Point4<Vector3>(points[0], points[1], points[2], points[3]);
            return TriCubic(p, vec.x, vec.y, vec.z);
        }

        public static Vector3 FindPoint(Vector3[] points, Vector3 vec)
        {
            Point4<Vector3> p = new Point4<Vector3>(points[0], points[1], points[2], points[3]);
            return TriCubic(p, vec.x, vec.y, vec.z);
        }*/

        public struct Point4<T>
        {
            public T a;
            public T b;
            public T c;
            public T d;

            public Point4(List<T> lst)
            {
                a = lst[0];
                b = lst[1];
                c = lst[2];
                d = lst[3];
            }
            public Point4(T[] lst)
            {
                a = lst[0];
                b = lst[1];
                c = lst[2];
                d = lst[3];
            }
            public Point4(T a, T b, T c, T d)
            {
                this.a = a;
                this.b = b;
                this.c = c;
                this.d = d;
            }

            /*public static Point4<Vector3> operator *(Point4<Vector3> p1, Point4<Vector3> p2)
            {
                return new Point4<Vector3>(Vector3.Scale(p1.a, p2.a), Vector3.Scale(p1.b , p2.b), Vector3.Scale(p1.c, p2.c), Vector3.Scale(p1.d, p2.d));
            }
            public static Point4<Vector3> operator -(Point4<Vector3> p1, Point4<Vector3> p2)
            {
                return new Point4<Vector3>(p1.a - p2.a, p1.b - p2.b, p1.c - p2.c, p1.d- p2.d);
            }*/
        }
    }
}