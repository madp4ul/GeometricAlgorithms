using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree
{
    class TriangleEdge
    {
        public readonly Edge Edge1;
        public readonly Edge Edge2;

        public TriangleEdge(Edge edge1, Edge edge2)
        {
            Edge1 = edge1;
            Edge2 = edge2;
        }
    }
}
