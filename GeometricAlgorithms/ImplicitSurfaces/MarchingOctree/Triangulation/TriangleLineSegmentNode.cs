using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.Triangulation
{
    class TriangleLineSegmentNode
    {
        public TriangleLineSegmentNode Previous;
        public TriangleLineSegmentNode Next;
        public readonly PositionIndex Vertex;

        public TriangleLineSegmentNode(PositionIndex vertex)
        {
            Vertex = vertex;
        }

        public void Detach()
        {
            if (Previous.Next == this)
            {
                Previous.Next = null;
            }
            Previous = null;

            if (Next.Previous == this)
            {
                Next.Previous = null;
            }
            Next = null;
        }

        public override string ToString()
        {
            string next = Next != null ? Next.Vertex.ToString() : "_";
            string prev = Previous != null ? Previous.Vertex.ToString() : "_";

            return $"{{node: {prev}<-(({Vertex}))->{next} }}";
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
