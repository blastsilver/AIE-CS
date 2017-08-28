using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIECS
{
    class OBJVertex
    {
        public int indexV;
        public int indexVN;
        public int indexVT;
        public OBJVertex(int index1, int index2, int index3)
        {
            indexV = index1;
            indexVN = index2;
            indexVT = index3;
        }
    }
}
