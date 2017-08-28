using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIECS
{
    class Vector2
    {
        private float[] m_data;

        public Vector2() : this(0, 0) { }
        public Vector2(float n) : this(n, n) { }
        public Vector2(float x, float y) { m_data = new float[2] { x, y }; }

        public float x { get { return m_data[0]; } set { m_data[0] = value; } }
        public float y { get { return m_data[1]; } set { m_data[1] = value; } }

        //public static bool operator ==(Vector2 a, Vector2 b) { return (object.ReferenceEquals(a, null) ? object.ReferenceEquals(b, null) : a.x == b.x && a.y == b.y); }
        //public static bool operator !=(Vector2 a, Vector2 b) { return !(object.ReferenceEquals(a, null) ? object.ReferenceEquals(b, null) : a.x == b.x && a.y == b.y); }
    }
}
