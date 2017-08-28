using System;
using System.Text;
using System.Linq;
using System.Drawing;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AIECS
{
    //https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/explicit
    //https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/implicit
    class Vector3
    {
        private float[] m_data;

        public Vector3() : this(0, 0, 0) {}
        public Vector3(float n) : this(n, n, n) {}
        public Vector3(Vector3 n) : this(n.x, n.y, n.z) { }
        public Vector3(float[] n) : this(n[0], n[1], n[2]) { }
        public Vector3(float x, float y, float z) { m_data = new float[3] { x, y, z }; }

        public float x { get { return m_data[0]; } set { m_data[0] = value; } }
        public float y { get { return m_data[1]; } set { m_data[1] = value; } }
        public float z { get { return m_data[2]; } set { m_data[2] = value; } }

        public float length() { return (float)Math.Sqrt(x * x + y * y + z * z); }
        public Vector3 normalize() { return this / length(); }
        public Vector3 cross(Vector3 v)
        {
            float cx = y * v.z - z * v.y;
            float cy = z * v.x - x * v.z;
            float cz = x * v.y - y * v.x;

            return new Vector3(cx, cy, cz);
        }

        public static Vector3 up { get { return new Vector3(0, 1, 0); } }
        public static Vector3 right { get { return new Vector3(1, 0, 0); } }
        public static Vector3 forward { get { return new Vector3(0, 0, 1); } }
        public static Vector3 operator -(Vector3 b) { return new Vector3(-b.x, -b.y, -b.z); }
        public static Vector3 operator -(float a, Vector3 b) { return new Vector3(a - b.x, a - b.y, a - b.z); }
        public static Vector3 operator +(float a, Vector3 b) { return new Vector3(a + b.x, a + b.y, a + b.z); }
        public static Vector3 operator *(float a, Vector3 b) { return new Vector3(a * b.x, a * b.y, a * b.z); }
        public static Vector3 operator /(float a, Vector3 b) { return new Vector3(a / b.x, a / b.y, a / b.z); }
        public static Vector3 operator -(Vector3 a, float b) { return new Vector3(a.x - b, a.y - b, a.z - b); }
        public static Vector3 operator +(Vector3 a, float b) { return new Vector3(a.x + b, a.y + b, a.z + b); }
        public static Vector3 operator *(Vector3 a, float b) { return new Vector3(a.x * b, a.y * b, a.z * b); }
        public static Vector3 operator /(Vector3 a, float b) { return new Vector3(a.x / b, a.y / b, a.z / b); }
        public static Vector3 operator -(Vector3 a, Vector3 b) { return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z); }
        public static Vector3 operator +(Vector3 a, Vector3 b) { return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z); }
        public static Vector3 operator *(Vector3 a, Vector3 b) { return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z); }
        public static Vector3 operator /(Vector3 a, Vector3 b) { return new Vector3(a.x / b.x, a.y / b.y, a.z / b.z); }
    }
}