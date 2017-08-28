using System;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AIECS
{
    class Matrix4
    {
        private float[] m_data;

        public Matrix4() : this(1) { }
        public Matrix4(float n) { m_data = new float[16] { n, 0, 0, 0, 0, n, 0, 0, 0, 0, n, 0, 0, 0, 0, n }; }

        public _VAL4F col1 { get { return new _COL4F(m_data, 0); } set { _COL4F.Copy(m_data, value, 0); } }
        public _VAL4F col2 { get { return new _COL4F(m_data, 1); } set { _COL4F.Copy(m_data, value, 1); } }
        public _VAL4F col3 { get { return new _COL4F(m_data, 2); } set { _COL4F.Copy(m_data, value, 2); } }
        public _VAL4F col4 { get { return new _COL4F(m_data, 3); } set { _COL4F.Copy(m_data, value, 3); } }
        public _VAL4F row1 { get { return new _ROW4F(m_data, 0); } set { _ROW4F.Copy(m_data, value, 0); } }
        public _VAL4F row2 { get { return new _ROW4F(m_data, 1); } set { _ROW4F.Copy(m_data, value, 1); } }
        public _VAL4F row3 { get { return new _ROW4F(m_data, 2); } set { _ROW4F.Copy(m_data, value, 2); } }
        public _VAL4F row4 { get { return new _ROW4F(m_data, 3); } set { _ROW4F.Copy(m_data, value, 3); } }

        public float Get(int x, int y)
        {
            // x = col, y = row
            return m_data[y * 4 + x];
        }

        public float Set(int x, int y, float value)
        {
            // x = col, y = row
            return m_data[y * 4 + x] = value;
        }

        public static Vector4 operator *(Matrix4 a, Vector4 b)
        {
            // multiply row by collum
            return new Vector4(
                a.Get(0, 0) * b.x + a.Get(1, 0) * b.y + a.Get(2, 0) * b.z + a.Get(3, 0) * b.w,
                a.Get(0, 1) * b.x + a.Get(1, 1) * b.y + a.Get(2, 1) * b.z + a.Get(3, 1) * b.w,
                a.Get(0, 2) * b.x + a.Get(1, 2) * b.y + a.Get(2, 2) * b.z + a.Get(3, 2) * b.w,
                a.Get(0, 3) * b.x + a.Get(1, 3) * b.y + a.Get(2, 3) * b.z + a.Get(3, 3) * b.w
            );
        }

        public static Matrix4 operator *(Matrix4 a, Matrix4 b)
        {
            Matrix4 result = new Matrix4();
            // multiply row by collum
            result.row1 = new _VAL4F((a.row1 * b.col1).dot(), (a.row1 * b.col2).dot(), (a.row1 * b.col3).dot(), (a.row1 * b.col4).dot());
            result.row2 = new _VAL4F((a.row2 * b.col1).dot(), (a.row2 * b.col2).dot(), (a.row2 * b.col3).dot(), (a.row2 * b.col4).dot());
            result.row3 = new _VAL4F((a.row3 * b.col1).dot(), (a.row3 * b.col2).dot(), (a.row3 * b.col3).dot(), (a.row3 * b.col4).dot());
            result.row4 = new _VAL4F((a.row4 * b.col1).dot(), (a.row4 * b.col2).dot(), (a.row4 * b.col3).dot(), (a.row4 * b.col4).dot());
            // return result
            return result;
        }

        public static Matrix4 Scale(Vector3 v)
        {
            // set { x, y, z } scale
            return Scale(v.x, v.y, v.z);
        }

        public static Matrix4 Scale(float x, float y, float z)
        {
            Matrix4 result = new Matrix4();
            // set { x, y, z } scale
            result.Set(0, 0, x);
            result.Set(1, 1, y);
            result.Set(2, 2, z);
            // return result
            return result;
        }

        public static Matrix4 Rotate(Vector3 v)
        {
            // return { x y z } rotation
            return Rotate(v.x, v.y, v.z);
        }

        public static Matrix4 Rotate(float x, float y, float z)
        {
            // return { x y z } rotation
            return RotateX(x) * RotateY(y) * RotateZ(z);
        }
        
        public static Matrix4 Rotate(Vector3 axis, float angle)
        {
            axis = axis.normalize();
            float a = angle;
            float c = (float)Math.Cos(a);
            float s = (float)Math.Sin(a);
            Matrix4 result = new Matrix4(1);
            Vector3 temp = ((1 - c) * axis);
            // set x axis angle
            result.Set(0, 0, c + temp.x * axis.x);
            result.Set(0, 1, temp.x * axis.y + s * axis.z);
            result.Set(0, 2, temp.x * axis.z - s * axis.y);
            // set y axis angle
            result.Set(1, 0, temp.y * axis.x - s * axis.z);
            result.Set(1, 1, c + temp.y * axis.y);
            result.Set(1, 2, temp.y * axis.z + s * axis.x);
            // set z axis angle
            result.Set(2, 0, temp.z * axis.x + s * axis.y);
            result.Set(2, 1, temp.z * axis.y - s * axis.x);
            result.Set(2, 2, c + temp.z * axis.z);
            // return result;
            return result;
        }

        public static Matrix4 Rotate(Vector3 r, Vector3 u, Vector3 f)
        {
            Matrix4 result = new Matrix4();
            // set axis rotation
            result.row1 = new _ROW4F(new float[4] { r.x, r.y, r.z, 1 });
            result.row2 = new _ROW4F(new float[4] { u.x, u.y, u.z, 1 });
            result.row3 = new _ROW4F(new float[4] { f.x, f.y, f.z, 1 });
            // return result
            return result;
        }

        public static Matrix4 RotateX(float angle)
        {
            Matrix4 result = new Matrix4();
            // set x rotation
            result.Set(1, 1, (float)Math.Cos(angle));
            result.Set(1, 2,-(float)Math.Sin(angle));
            result.Set(2, 1, (float)Math.Sin(angle));
            result.Set(2, 2, (float)Math.Cos(angle));
            // return result
            return result;
        }

        public static Matrix4 RotateY(float angle)
        {
            Matrix4 result = new Matrix4();
            // set y rotation
            result.Set(0, 0, (float)Math.Cos(angle));
            result.Set(0, 2,-(float)Math.Sin(angle));
            result.Set(2, 0, (float)Math.Sin(angle));
            result.Set(2, 2, (float)Math.Cos(angle));
            // return result
            return result;
        }

        public static Matrix4 RotateZ(float angle)
        {
            Matrix4 result = new Matrix4();
            // set z rotation
            result.Set(0, 0, (float)Math.Cos(angle));
            result.Set(0, 1,-(float)Math.Sin(angle));
            result.Set(1, 0, (float)Math.Sin(angle));
            result.Set(1, 1, (float)Math.Cos(angle));
            // return result
            return result;
        }

        public static Matrix4 Translate(Vector3 v)
        {
            // return { x y z } position
            return Translate(v.x, v.y, v.z);
        }

        public static Matrix4 Translate(float x, float y, float z)
        {
            Matrix4 result = new Matrix4();
            // set { x, y, z } position
            result.Set(3, 0, x);
            result.Set(3, 1, y);
            result.Set(3, 2, z);
            // return result
            return result;
        }

        public static Matrix4 Perspective(float fov, float ar, float near, float far)
        {
            Matrix4 result = new Matrix4();

            float range = near - far;
            float tHFOV = (float)Math.Tan(fov / 2);

            result.Set(0, 0, 1.0f / (tHFOV * ar));
            result.Set(1, 1, 1.0f / tHFOV);
            result.Set(2, 2, (-near - far) / range);
            result.Set(2, 3, 2 * far * near / range);
            result.Set(3, 2, 0); // have a look at this later (1 was here before)

            return result;
        }

        public class _VAL4F
        {
            public float[] data;
            public _VAL4F(float e1, float e2, float e3, float e4) { data = new float[4] { e1, e2, e3, e4 }; }
            public float dot() { return (data[0]) + (data[1]) + (data[2]) + (data[3]); }
            public static _VAL4F operator +(_VAL4F a, _VAL4F b) { return new _VAL4F(a.data[0] + b.data[0], a.data[1] + b.data[1], a.data[2] + b.data[2], a.data[3] + b.data[3]); }
            public static _VAL4F operator -(_VAL4F a, _VAL4F b) { return new _VAL4F(a.data[0] - b.data[0], a.data[1] - b.data[1], a.data[2] - b.data[2], a.data[3] - b.data[3]); }
            public static _VAL4F operator *(_VAL4F a, _VAL4F b) { return new _VAL4F(a.data[0] * b.data[0], a.data[1] * b.data[1], a.data[2] * b.data[2], a.data[3] * b.data[3]); }
            public static _VAL4F operator /(_VAL4F a, _VAL4F b) { return new _VAL4F(a.data[0] / b.data[0], a.data[1] / b.data[1], a.data[2] / b.data[2], a.data[3] / b.data[3]); }
        }
        public class _COL4F : _VAL4F
        {
            public _COL4F(float[] n) : base(n[0], n[1], n[2], n[3]) { }
            public _COL4F(float[] n, int i) : base(n[0 + i], n[4 + i], n[8 + i], n[12 + i]) { }
            public static void Copy(float[] a, _VAL4F b, int i) { a[0 + i] = b.data[0]; a[4 + i] = b.data[1]; a[8 + i] = b.data[2]; a[12 + i] = b.data[3]; }
        }
        public class _ROW4F : _VAL4F
        {
            public _ROW4F(float[] n) : base(n[0], n[1], n[2], n[3]) { }
            public _ROW4F(float[] n, int i) : base(n[0 + i * 4], n[1 + i * 4], n[2 + i * 4], n[3 + i * 4]) { }
            public static void Copy(float[] a, _VAL4F b, int i) { a[0 + i * 4] = b.data[0]; a[1 + i * 4] = b.data[1]; a[2 + i * 4] = b.data[2]; a[3 + i * 4] = b.data[3]; }
        }
    }
}
/*
namespace AIECS
{
    class Matrix4
    {
        private float[] m_data;

        public Matrix4() : this(1) { }
        public Matrix4(float n) { m_data = new float[16] { n, 0, 0, 0, 0, n, 0, 0, 0, 0, n, 0, 0, 0, 0, n }; }

        public _VAL4F col1 { get { return new _COL4F(m_data, 0); } set { _COL4F.Copy(m_data, value, 0); } }
        public _VAL4F col2 { get { return new _COL4F(m_data, 1); } set { _COL4F.Copy(m_data, value, 1); } }
        public _VAL4F col3 { get { return new _COL4F(m_data, 2); } set { _COL4F.Copy(m_data, value, 2); } }
        public _VAL4F col4 { get { return new _COL4F(m_data, 3); } set { _COL4F.Copy(m_data, value, 3); } }
        public _VAL4F row1 { get { return new _ROW4F(m_data, 0); } set { _ROW4F.Copy(m_data, value, 0); } }
        public _VAL4F row2 { get { return new _ROW4F(m_data, 1); } set { _ROW4F.Copy(m_data, value, 1); } }
        public _VAL4F row3 { get { return new _ROW4F(m_data, 2); } set { _ROW4F.Copy(m_data, value, 2); } }
        public _VAL4F row4 { get { return new _ROW4F(m_data, 3); } set { _ROW4F.Copy(m_data, value, 3); } }

        public float Get(int x, int y)
        {
            // x = col, y = row
            return m_data[y * 4 + x];
        }
        
        public float Set(int x, int y, float value)
        {
            // x = col, y = row
            return m_data[y * 4 + x] = value;
        }
        
        public static Vector4 operator *(Matrix4 a, Vector4 b)
        {
            return new Vector4(
                (a.row1 * b).dot(),
                (a.row2 * b).dot(),
                (a.row3 * b).dot(),
                (a.row4 * b).dot()
            );
        }
        public static Matrix4 operator *(Matrix4 a, Matrix4 b)
        {
            Matrix4 result = new Matrix4();
            result.row1 = new _VAL4F((a.row1 * b.col1).dot(), (a.row1 * b.col2).dot(), (a.row1 * b.col3).dot(), (a.row1 * b.col4).dot());
            result.row2 = new _VAL4F((a.row2 * b.col1).dot(), (a.row2 * b.col2).dot(), (a.row2 * b.col3).dot(), (a.row2 * b.col4).dot());
            result.row3 = new _VAL4F((a.row3 * b.col1).dot(), (a.row3 * b.col2).dot(), (a.row3 * b.col3).dot(), (a.row3 * b.col4).dot());
            result.row4 = new _VAL4F((a.row4 * b.col1).dot(), (a.row4 * b.col2).dot(), (a.row4 * b.col3).dot(), (a.row4 * b.col4).dot());
            return result;
        }

        public static Matrix4 Scale(float scalar) { return new Matrix4(scalar); }
        public static Matrix4 Scale(Vector3 vector) { return Scale(vector.x, vector.y, vector.z); }
        public static Matrix4 Scale(float x, float y, float z)
        {
            Matrix4 result = new Matrix4();
            
            result.Set(0, 0, x);
            result.Set(1, 1, y);
            result.Set(2, 2, z);

            return result;
        }

        public static Matrix4 Rotate(Vector3 r, Vector3 u, Vector3 f)
        {
            Matrix4 result = new Matrix4();

            result.row1 = new _ROW4F(new float[4] { r.x, r.y, r.z, 1 });
            result.row2 = new _ROW4F(new float[4] { u.x, u.y, u.z, 1 });
            result.row3 = new _ROW4F(new float[4] { f.x, f.y, f.z, 1 });

            return result;
        }
        public static Matrix4 Rotate(Vector3 vector) { return Rotate(vector.x, vector.y, vector.z); }
        public static Matrix4 Rotate(float x, float y, float z)
        {
            Matrix4 rx = new Matrix4();
            Matrix4 ry = new Matrix4();
            Matrix4 rz = new Matrix4();

            rz.Set(0, 0, (float)Math.Cos(z));
            rz.Set(0, 1,-(float)Math.Sin(z));
            rz.Set(1, 0, (float)Math.Sin(z));
            rz.Set(1, 1, (float)Math.Cos(z));
            
            rx.Set(1, 1, (float)Math.Cos(x));
            rx.Set(1, 2,-(float)Math.Sin(x));
            rx.Set(2, 1, (float)Math.Sin(x));
            rx.Set(2, 2, (float)Math.Cos(x));

            ry.Set(0, 0,  (float)Math.Cos(y));
            ry.Set(0, 2, -(float)Math.Sin(y));
            ry.Set(2, 0,  (float)Math.Sin(y));
            ry.Set(2, 2,  (float)Math.Cos(y));

            return rx * ry * rz;
        }

        public static Matrix4 Translate(Vector3 vector) { return Translate(vector.x, vector.y, vector.z); }
        public static Matrix4 Translate(float x, float y, float z)
        {
            Matrix4 result = new Matrix4();

            result.col1 = new _COL4F(new float[4] { x, y, z, 1 });

            return result;
        }

        public static Matrix4 Perspective(float fov, float ar, float near, float far)
        {
            Matrix4 result = new Matrix4();

            float range = near - far;
            float tHFOV = (float)Math.Tan(fov / 2);

            result.Set(0, 0, 1.0f / (tHFOV * ar));
            result.Set(1, 1, 1.0f / tHFOV);
            result.Set(2, 2, (-near - far) / range);
            result.Set(2, 3, 2 * far * near / range);
            result.Set(3, 2, 1);

            return result;
        }

        public class _VAL4F
        {
            public float[] data;
            public _VAL4F(float e1, float e2, float e3, float e4) { data = new float[4] { e1, e2, e3, e4 }; }
            public float dot() { return (data[0]) + (data[1]) + (data[2]) + (data[3]); }
            public static _VAL4F operator +(_VAL4F a, _VAL4F b) { return new _VAL4F(a.data[0] + b.data[0], a.data[1] + b.data[1], a.data[2] + b.data[2], a.data[3] + b.data[3]); }
            public static _VAL4F operator -(_VAL4F a, _VAL4F b) { return new _VAL4F(a.data[0] - b.data[0], a.data[1] - b.data[1], a.data[2] - b.data[2], a.data[3] - b.data[3]); }
            public static _VAL4F operator *(_VAL4F a, _VAL4F b) { return new _VAL4F(a.data[0] * b.data[0], a.data[1] * b.data[1], a.data[2] * b.data[2], a.data[3] * b.data[3]); }
            public static _VAL4F operator /(_VAL4F a, _VAL4F b) { return new _VAL4F(a.data[0] / b.data[0], a.data[1] / b.data[1], a.data[2] / b.data[2], a.data[3] / b.data[3]); }
            public static _VAL4F operator +(_VAL4F a, Vector4 b) { return new _VAL4F(a.data[0] + b.data[0], a.data[1] + b.data[1], a.data[2] + b.data[2], a.data[3] + b.data[3]); }
            public static _VAL4F operator -(_VAL4F a, Vector4 b) { return new _VAL4F(a.data[0] - b.data[0], a.data[1] - b.data[1], a.data[2] - b.data[2], a.data[3] - b.data[3]); }
            public static _VAL4F operator *(_VAL4F a, Vector4 b) { return new _VAL4F(a.data[0] * b.data[0], a.data[1] * b.data[1], a.data[2] * b.data[2], a.data[3] * b.data[3]); }
            public static _VAL4F operator /(_VAL4F a, Vector4 b) { return new _VAL4F(a.data[0] / b.data[0], a.data[1] / b.data[1], a.data[2] / b.data[2], a.data[3] / b.data[3]); }
        }

        public class _COL4F : _VAL4F
        {
            public _COL4F(float[] n) : base(n[0], n[1], n[2], n[3]) { }
            public _COL4F(float[] n, int i) : base(n[0 + i], n[4 + i], n[8 + i], n[12 + i]) { }
            public static void Copy(float[] a, _VAL4F b, int i) { a[0 + i] = b.data[0]; a[4 + i] = b.data[1]; a[8 + i] = b.data[2]; a[12 + i] = b.data[3]; }
        }

        public class _ROW4F : _VAL4F
        {
            public _ROW4F(float[] n) : base(n[0], n[1], n[2], n[3]) { }
            public _ROW4F(float[] n, int i) : base(n[0 + i * 4], n[1 + i * 4], n[2 + i * 4], n[3 + i * 4]) { }
            public static void Copy(float[] a, _VAL4F b, int i) { a[0 + i * 4] = b.data[0]; a[1 + i * 4] = b.data[1]; a[2 + i * 4] = b.data[2]; a[3 + i * 4] = b.data[3]; }
        }
    }
}
*/
