using System;
using System.Text;
using System.Linq;
using System.Drawing;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AIECS
{
    class Vector4
    {
        private float[] m_data;

        public Vector4() : this(0, 0, 0, 0) { }
        public Vector4(float n) : this(n, n, n, n) { }
        public Vector4(Vector4 v) : this(v.x, v.y, v.z, v.w) { }
        public Vector4(float[] n) : this(n[0], n[1], n[2], n[3]) { }
        public Vector4(float a, Vector3 b) : this(a, b.x, b.y, b.z) { }
        public Vector4(Vector3 a, float b) : this(a.x, a.y, a.z, b) { }
        public Vector4(float x, float y, float z, float w) { m_data = new float[4] { x, y, z, w }; }

        public float x { get { return m_data[0]; } set { m_data[0] = value; } }
        public float y { get { return m_data[1]; } set { m_data[1] = value; } }
        public float z { get { return m_data[2]; } set { m_data[2] = value; } }
        public float w { get { return m_data[3]; } set { m_data[3] = value; } }

        public float Dot() { return x * x + y * y + z * z + w * w; }

        public Vector3 xyz { get { return new Vector3(m_data[0], m_data[1], m_data[2]); } }
        public float[] data { get { return m_data; } set { m_data = value; } }

        //public static bool operator ==(Vector4 a, Vector4 b) { return (object.ReferenceEquals(a, null) ? object.ReferenceEquals(b, null) : a.x == b.x && a.y == b.y && a.z == b.z && a.w == b.w); }
        //public static bool operator !=(Vector4 a, Vector4 b) { return !(object.ReferenceEquals(a, null) ? object.ReferenceEquals(b, null) : a.x == b.x && a.y == b.y && a.z == b.z && a.w == b.w); }
    }
}