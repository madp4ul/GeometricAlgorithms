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
        public readonly int VertexIndex;

        public TriangleLineSegmentNode(int vertexIndex)
        {
            VertexIndex = vertexIndex;
        }

        public override string ToString()
        {
            string hasNext = Next != null ? "has next" : "";
            string hasPrev = Previous != null ? "has previous" : "";

            return $"{{node: {VertexIndex}, {hasPrev}, {hasNext} }}";
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
