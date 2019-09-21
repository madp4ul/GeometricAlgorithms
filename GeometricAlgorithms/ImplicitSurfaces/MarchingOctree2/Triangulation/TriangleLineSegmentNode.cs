using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree2.Triangulation
{
    class TriangleLineSegmentNode
    {
        public TriangleLineSegmentNode Previous;
        public TriangleLineSegmentNode Next;
        public int VertexIndex;

        public TriangleLineSegmentNode(int vertexIndex)
        {
            VertexIndex = vertexIndex;
        }

        public static void Connect(TriangleLineSegmentNode first, TriangleLineSegmentNode second)
        {
            if (first != null)
            {
                first.Next = second;
            }

            if (second != null)
            {
                second.Previous = first;
            }
        }
    }
}
