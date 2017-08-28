using System;
using System.Text;
using System.Linq;
using System.Drawing;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AIECS
{
    class OBJMesh
    {
        // position data
        public List<Vector3> normals;
        public List<Vector3> vertices;
        public List<Vector2> uvcoords;
        // connections data
        public OBJTransform transform;
        public List<OBJLink3> triangles;
        // extra drawing data
        public Pen pen;

        public OBJMesh()
        {
            Random rnd = new Random();
            pen = new Pen(Color.FromArgb(rnd.Next() % 200 + 55, rnd.Next() % 200 + 55, rnd.Next() % 200 + 55), 1);
            normals = new List<Vector3>();
            vertices = new List<Vector3>();
            uvcoords = new List<Vector2>();
            transform = new OBJTransform();
            triangles = new List<OBJLink3>();
        }

        public void Clear()
        {
            normals.Clear();
            vertices.Clear();
            uvcoords.Clear();
            triangles.Clear();
        }
        public void OptimizeData()
        {
            float count = 0;
            float magnitude = 0;
            Vector3 displacement = null;

            foreach (Vector3 v in vertices)
            {
                float m = v.x * v.x + v.y * v.y + v.z * v.z;

                if (m > magnitude) magnitude = m;
                displacement = displacement == null ? new Vector3(v) : new Vector3(displacement + v);
                count++;
            }

            magnitude = (float)Math.Sqrt(magnitude);
            if (count != 0) displacement.x = displacement.x / count;
            if (count != 0) displacement.y = displacement.y / count;
            if (count != 0) displacement.z = displacement.z / count;
            Matrix4 scale = new Matrix4(1f / magnitude);
            Matrix4 translate = Matrix4.Translate(-displacement.x, -displacement.y, -displacement.z);

            for (int i = 0; i < vertices.Count; i++)
            {
                vertices[i] = (scale * translate * new Vector4(vertices[i], 1)).xyz;
            }
        }

        public void ReCalculateNormals()
        {
            for (int i = 0; i < triangles.Count; i++)
            {
                int i0 = triangles[i].i1;
                int i1 = triangles[i].i2;
                int i2 = triangles[i].i3;

                Vector3 v1 = vertices[i1] - vertices[i0];
                Vector3 v2 = vertices[i2] - vertices[i0];

                Vector3 normal = v1.cross(v2).normalize();

                normals[i0] += normal;
                normals[i1] += normal;
                normals[i2] += normal;
            }
        }
    }
}