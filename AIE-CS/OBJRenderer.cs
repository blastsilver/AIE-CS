using System;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AIECS
{
    class OBJRenderer
    {
        // variables
        private static float m_sizeW = 0;
        private static float m_sizeH = 0;
        private static Matrix4 m_PVMMatrix = new Matrix4();
        private static Matrix4 m_ViewMatrix = new Matrix4();
        private static Matrix4 m_ModelMatrix = new Matrix4();
        private static Matrix4 m_ProjectionMatrix = new Matrix4();

        public static void Init(float w, float h)
        {
            // set size
            m_sizeW = w;
            m_sizeH = h;
            // set perspective matrix
            SetProjectionMatrix(Matrix4.Perspective(70, m_sizeW / m_sizeH, 0.1f, 1000.0f));
        }

        public static void Render(OBJMesh mesh, Graphics graphics)
        {
            // recalculate PVW matrix
            RecalculatePVMMatrix();
            // iterate through mesh faces
            foreach (OBJLink3 link3 in mesh.triangles)
            {
                // render triangle
                Render(
                    m_PVMMatrix * new Vector4(mesh.vertices[link3.i1], 1),
                    m_PVMMatrix * new Vector4(mesh.vertices[link3.i2], 1),
                    m_PVMMatrix * new Vector4(mesh.vertices[link3.i3], 1),
                    graphics,
                    mesh.pen
                );
            }
        }

        public static void Render(Vector4 v1, Vector4 v2, Vector4 v3, Graphics graphics, Pen pen)
        {
            // depth test
            if (v1.z < 0.1f) return;
            if (v2.z < 0.1f) return;
            if (v3.z < 0.1f) return;
            // render converted points
            Render(ConvertWORLDtoSCREEN(v1), ConvertWORLDtoSCREEN(v2), ConvertWORLDtoSCREEN(v3), graphics, pen);
        }

        public static void Render(Point p1, Point p2, Point p3, Graphics graphics, Pen pen)
        {
            // back-face culling - http://www.geeksforgeeks.org/orientation-3-ordered-points/
            if ((p2.Y - p1.Y) * (p3.X - p2.X) - (p2.X - p1.X) * (p3.Y - p2.Y) > 0) return;
            // render points on screen
            graphics.DrawPolygon(pen, new Point[3] { p1, p2, p3 });
        }
        
        public static void SetViewMatrix(Matrix4 matrix)
        {
            // set view matrix
            m_ViewMatrix = matrix;
        }

        public static void SetModelMatrix(Matrix4 matrix)
        {
            // set model matrix
            m_ModelMatrix = matrix;
        }

        public static void SetProjectionMatrix(Matrix4 matrix)
        {
            // set projection matrix
            m_ProjectionMatrix = matrix;
        }

        private static void RecalculatePVMMatrix()
        {
            // recalculate PVM matrix
            m_PVMMatrix = m_ProjectionMatrix * m_ViewMatrix;
        }

        public static Matrix4 GetViewMatrix()
        {
            // return view matrix
            return m_ViewMatrix;
        }

        private static Point ConvertWORLDtoSCREEN(Vector4 vector)
        {
            // convert world to screen coordinates
            return new Point(
                (int)(((vector.x / vector.z) + 1.0f) * (m_sizeW / 2.0f)),
                (int)(((-vector.y / vector.z) + 1.0f) * (m_sizeH / 2.0f))
            );
        }

    }
}