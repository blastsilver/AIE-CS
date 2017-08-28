using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIECS
{
    class OBJTransform
    {
        public Vector3 scale;
        public Matrix4 matrix;
        public Vector3 position;
        public Vector3 rotation;

        public OBJTransform()
        {
            scale = new Vector3(1);
            matrix = new Matrix4();
            position = new Vector3();
            rotation = new Vector3();
        }

        public void CalculateMatrix()
        {
            // calculate transform matrix
            matrix = Matrix4.Translate(position) * Matrix4.Rotate(rotation) * Matrix4.Scale(scale);
        }

    }
}