using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIECS
{
    class OBJFile
    {
        private class _typeF2
        {
            public string line;
            public Vector2 vec;
            public _typeF2(string a, Vector2 b)
            {
                vec = b;
                line = a;
            }
        }

        private class _typeF3
        {
            public string line;
            public Vector3 vec;
            public _typeF3(string a, Vector3 b)
            {
                vec = b;
                line = a;
            }
        }

        private class _typeI3
        {
            public int e1;
            public int e2;
            public int e3;
            public _typeI3(int index1, int index2, int index3)
            {
                e1 = index1;
                e2 = index2;
                e3 = index3;
            }
        }
        
        private static List<_typeF3> m_listV = new List<_typeF3>();
        private static List<_typeF3> m_listN = new List<_typeF3>();
        private static List<_typeF2> m_listU = new List<_typeF2>();
        private static Dictionary<string, int> m_link3 = new Dictionary<string, int>();

        public static void Save(string filename, List<OBJMesh> meshes)
        {
            // create new file
            System.IO.FileInfo file = new System.IO.FileInfo(filename);
            System.IO.StreamWriter writer = file.AppendText();
            // iterate through meshes
            int n = 1;
            int count = 0;
            foreach (OBJMesh mesh in meshes)
            {
                // write current mesh
                writer.WriteLine("# Object_" + count++ + " - Using 'OBJ Tool Kit' by Francisco Romano");
                // write all mesh vertices
                foreach (Vector3 v in mesh.vertices) writer.WriteLine("v " + v.x + " " + v.y + " " + v.z);
                // write all mesh normals
                foreach (Vector3 vn in mesh.normals) writer.WriteLine("vn " + vn.x + " " + vn.y + " " + vn.z);
                // write all mesh uvs
                foreach (Vector2 vt in mesh.uvcoords) writer.WriteLine("vt " + vt.x + " " + vt.y);
                // write all mesh triangles
                foreach (OBJLink3 f in mesh.triangles)
                {
                    int i1 = f.i1 + n;
                    int i2 = f.i2 + n;
                    int i3 = f.i3 + n;
                    writer.WriteLine("f " + i1 + "/" + i1 + "/" + i1 + " " + i2 + "/" + i2 + "/" + i2 + " " + i3 + "/" + i3 + "/" + i3);
                }
                count++;
                n += mesh.vertices.Count;
            }
            // close file
            writer.Close();
        }

        public static OBJMesh Open(string filename)
        {
            FreeMemory();
            OBJMesh mesh = new OBJMesh();
            // get current file
            System.IO.StreamReader file = new System.IO.StreamReader(filename);
            // check if complete
            string line;
            while ((line = file.ReadLine()) != null)
            {
                line = line.Replace("  ", " ");
                // decrypt face
                if (line.StartsWith("f ")) DecodeFaceToMesh(line, mesh);
                // decrypt vertex
                else if (line.StartsWith("v ")) m_listV.Add(DecodeTypeF3(line));
                // decrypt normal
                else if (line.StartsWith("vn ")) m_listN.Add(DecodeTypeF3(line));
                // decrypt uv coord
                else if (line.StartsWith("vt ")) m_listU.Add(DecodeTypeF2(line));
            }
            // close the file
            file.Close();
            FreeMemory();
            // return mesh
            return mesh;
        }

        private static void FreeMemory()
        {
            // clear data
            m_link3.Clear();
            m_listV.Clear();
            m_listN.Clear();
            m_listU.Clear();
        }

        private static void DecodeFaceToMesh(string line, OBJMesh mesh)
        {
            string[] data = line.Split(' ');
            if (data.Length > 4 && data[4] != "") DecodeFace4ToMesh(data, mesh);
            else DecodeFace3ToMesh(data, mesh);
        }

        private static void DecodeFace4ToMesh(string[] lines, OBJMesh mesh)
        {
            int[] index = new int[4];
            // iterate through data
            for (int i = 0; i < 4; i++)
            {
                // decode face
                _typeI3 face = DecodeTypeI3(lines[i + 1]);
                // final check
                if (face.e3 == -1 && m_listU.Count > 0)
                {
                    // swap U-N (list may have Vertices and UVCoords ONLY)
                    face.e3 = face.e2;
                    face.e2 = -1;
                }
                // check if exists
                if (!m_link3.ContainsKey(lines[i + 1]))
                {
                    // fetch data
                    _typeF3 v = FetchListV(face.e1);
                    _typeF3 n = FetchListN(face.e2);
                    _typeF2 u = FetchListU(face.e3);
                    // add data to mesh
                    mesh.vertices.Add(v.vec);
                    mesh.normals.Add(n.vec);
                    mesh.uvcoords.Add(u.vec);
                    // add data to links
                    m_link3.Add(lines[i + 1], mesh.vertices.Count - 1);
                }
                // update index
                index[i] = m_link3[lines[i + 1]];
            }
            // add triangle to mesh
            mesh.triangles.Add(new OBJLink3(index[0], index[1], index[2]));
            mesh.triangles.Add(new OBJLink3(index[2], index[3], index[0]));
        }

        private static void DecodeFace3ToMesh(string[] lines, OBJMesh mesh)
        {
            int[] index = new int[3];
            // iterate through data
            for (int i = 0; i < 3; i++)
            {
                // decode face
                _typeI3 face = DecodeTypeI3(lines[i + 1]);
                // final check
                if (face.e3 == -1 && m_listU.Count > 0)
                {
                    // swap U-N (list may have Vertices and UVCoords ONLY)
                    face.e3 = face.e2;
                    face.e2 = -1;
                }
                // check if exists
                if (!m_link3.ContainsKey(lines[i + 1]))
                {
                    // fetch data
                    _typeF3 v = FetchListV(face.e1);
                    _typeF3 n = FetchListN(face.e2);
                    _typeF2 u = FetchListU(face.e3);
                    // add data to mesh
                    mesh.vertices.Add(v.vec);
                    mesh.normals.Add(n.vec);
                    mesh.uvcoords.Add(u.vec);
                    // add data to links
                    m_link3.Add(lines[i + 1], mesh.vertices.Count - 1);
                }
                // update index
                index[i] = m_link3[lines[i + 1]];
            }
            // add triangle to mesh
            mesh.triangles.Add(new OBJLink3(index[0], index[1], index[2]));
        }

        private static _typeF3 FetchListV(int index)
        {
            // fetch vertex
            return index < m_listV.Count && index >= 0 ? m_listV[index] : new _typeF3("@@@ V_ERROR @@@", new Vector3());
        }

        private static _typeF3 FetchListN(int index)
        {
            // fetch normal
            return index < m_listN.Count && index >= 0 ? m_listN[index] : new _typeF3("@@@ N_ERROR @@@", new Vector3());
        }

        private static _typeF2 FetchListU(int index)
        {
            // fetch uvcoord
            return index < m_listU.Count && index >= 0 ? m_listU[index] : new _typeF2("@@@ U_ERROR @@@", new Vector2());
        }

        private static _typeF2 DecodeTypeF2(string line)
        {
            string[] data = line.Split(' ');
            // decode format
            return new _typeF2(line, new Vector2(float.Parse(data[1]), float.Parse(data[2])));
        }

        private static _typeF3 DecodeTypeF3(string line)
        {
            string[] data = line.Split(' ');
            // decode format
            return new _typeF3(line, new Vector3(float.Parse(data[1]), float.Parse(data[2]), float.Parse(data[3])));
        }

        private static _typeI3 DecodeTypeI3(string line)
        {
            string[] data = line.Split('/');
            // decode format
            return new _typeI3(
                data.Length > 0 && data[0].Length > 0 ? int.Parse(data[0]) - 1 : -1,
                data.Length > 1 && data[1].Length > 0 ? int.Parse(data[1]) - 1 : -1,
                data.Length > 2 && data[2].Length > 0 ? int.Parse(data[2]) - 1 : -1
            );
        }

    }
}

//    public static void Read(string filename, OBJMesh mesh)
//    {
//        string line = "";
//        // clear all data
//        ClearAll();
//        // get current file
//        System.IO.StreamReader file = new System.IO.StreamReader(filename);
//        // check if complete
//        while ((line = file.ReadLine()) != null)
//        {
//            // decrypt face
//            if (line.StartsWith("f ")) DecryptFace(line.Replace("f ", ""));
//            // decrypt vertex
//            else if (line.StartsWith("v ")) m_listV.Add(new type_float3( line, DecryptVector3(line.Replace("v ", ""))));
//            // decrypt normal
//            else if (line.StartsWith("vn ")) m_listN.Add(DecryptVector3(line.Replace("vn ", "")));
//            // decrypt uv coord
//            else if (line.StartsWith("vt ")) m_listU.Add(DecryptVector2(line.Replace("vt ", "")));
//        }
//        // copy data to current mesh
//        CopyToMesh(mesh);
//    }

//    public static void Write(string filename, OBJMesh mesh)
//    {

//    }

//    private static void ClearAll()
//    {
//        // clear lists
//        m_listN.Clear();
//        m_listU.Clear();
//        m_listV.Clear();
//        triangles.Clear();
//    }
//    private static void CopyToMesh(OBJMesh mesh)
//    {
//        // clear data
//        mesh.Clear();
//        // simple copy
//        mesh.normals = m_listN;
//        mesh.uvcoords = m_listU;
//        mesh.vertices = m_listV;
//        mesh.triangles = triangles;
//        // complex copy
//        //foreach (OBJLink3 link3 in triangles)
//        //{
//        //    // get links
//        //    OBJLink2 l1 = new OBJLink2(link3.i1, link3.i2);
//        //    OBJLink2 l2 = new OBJLink2(link3.i2, link3.i3);
//        //    OBJLink2 l3 = new OBJLink2(link3.i3, link3.i1);
//        //    // iterate through lines
//        //    foreach (OBJLink2 link2 in mesh.lines)
//        //    {
//        //        // check if null
//        //        if (link2 == null) continue;
//        //        // update current lines
//        //        if (l1 != null && l1 == link2) l1 = null;
//        //        if (l2 != null && l2 == link2) l2 = null;
//        //        if (l3 != null && l3 == link2) l3 = null;
//        //    }
//        //    // add lines to list
//        //    if (l1 != null) mesh.lines.Add(l1);
//        //    if (l2 != null) mesh.lines.Add(l2);
//        //    if (l3 != null) mesh.lines.Add(l3);
//        //}
//    }
//    private static void DecryptFace(string line)
//    {
//        OBJVertex[] data = DecryptOBJVertexArray(line);
//        foreach (OBJVertex i in data)
//        {

//        }

//        //for (int i = 0; i < 3; i++)
//        //{
//        //    // fetch values
//        //    Vector3 v1 = FetchVertex(data[i]);
//        //    Vector3 n1 = FetchNormal(data[i]);
//        //    Vector2 u1 = FetchUVCoord(data[i]);
//        //    if (v1 == null || n1 == null || u1 == null) continue;
//        //    // iterate through triangles
//        //    foreach (OBJLink3 link3 in triangles)
//        //    {
//        //        //Vector3 v2 = FetchVertex(data[i]);
//        //        //Vector3 n2 = FetchNormal(data[i]);
//        //        //Vector2 u2 = FetchUVCoord(data[i]);
//        //        //// validate data [v]
//        //        //if (v1 == v2)
//        //        //{
//        //        //    // validate data [vn]
//        //        //    if (n1 == n2)
//        //        //    {
//        //        //        // validate data [vt]
//        //        //        if (u1 == u2)
//        //        //        {
//        //        //            data[i].indexV = link3.i1;
//        //        //            data[i].indexVN = link3.i1;
//        //        //            data[i].indexVT = link3.i1;
//        //        //            break;
//        //        //        }
//        //        //    }
//        //        //}
//        //    }
//        //}

//        OBJLink3 link = new OBJLink3(data[0].indexV, data[1].indexV, data[2].indexV);
//        triangles.Add(link);

//    }

//    private static _typeF3 FetchVertex(OBJVertex data)
//    {
//        return data.indexV == -1 || m_listV.Count <= data.indexV ? null : m_listV[data.indexV];
//    }
//    private static _typeF3 FetchNormal(OBJVertex data)
//    {
//        return data.indexVN == -1 || m_listN.Count <= data.indexVN ? null : m_listN[data.indexVN];
//    }
//    private static _typeF2 FetchUVCoord(OBJVertex data)
//    {
//        return data.indexVT == -1 || m_listU.Count <= data.indexVT ? null : m_listU[data.indexVT];
//    }
//    private static Vector2 DecryptVector2(string line)
//    {
//        string[] data = line.Split(' ');
//        return new Vector2
//        (
//            float.Parse(data[0]),
//            float.Parse(data[1])
//        );
//    }
//    private static Vector3 DecryptVector3(string line)
//    {
//        string[] data = line.Split(
//    private static OBJVertex DecryptOBJVertex(string line)
//    {
//        string[] data = line.Split('/');
//        return new OBJVertex
//        (
//            data.Length > 0 ? int.Parse(data[0]) - 1 : -1,
//            data.Length > 1 ? int.Parse(data[1]) - 1 : -1,
//            data.Length > 2 ? int.Parse(data[2]) - 1 : -1
//        );
//    }
//    private static OBJVertex[] DecryptOBJVertexArray(string line)
//    {
//        string[] data = line.Split(' ');
//        return new OBJVertex[3]
//        {
//            DecryptOBJVertex(data[0]),
//            DecryptOBJVertex(data[1]),
//            DecryptOBJVertex(data[2])
//        };
//    }' ');
//        return new Vector3
//        (
//            float.Parse(data[0]),
//            float.Parse(data[1]),
//            float.Parse(data[2])
//        );
//    }
//}