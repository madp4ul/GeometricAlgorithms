using GeometricAlgorithms.ImplicitSurfaces.MarchingOctree2.Triangulation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree2
{
    class EdgeSurfaceIntersections
    {
        public readonly int? IntersectionIndex;
        public readonly IReadOnlyCollection<TriangleLineSegment> EdgeLines;

        public EdgeSurfaceIntersections(bool isEdgeMinValueInside, int[] edgeIntersectionIndices)
        {
            int edgeLinesStart = 0;
            int edgeLinesEnd = edgeIntersectionIndices.Length;

            if (edgeIntersectionIndices.Length % 2 != 0)
            {
                if (isEdgeMinValueInside)
                {
                    IntersectionIndex = edgeIntersectionIndices[0];
                    edgeLinesStart = 1;
                }
                else
                {
                    IntersectionIndex = edgeIntersectionIndices[edgeIntersectionIndices.Length - 1];
                    edgeLinesEnd = edgeIntersectionIndices.Length - 1;
                }
            }

            var lineSegments = new TriangleLineSegment[(edgeLinesEnd - edgeLinesStart) / 2];

            int lineIndex = 0;
            for (int i = edgeLinesStart; i < edgeLinesEnd; i += 2)
            {
                var startNode = new TriangleLineSegmentNode(edgeIntersectionIndices[i]);
                var endNode = new TriangleLineSegmentNode(edgeIntersectionIndices[i + 1]);

                TriangleLineSegmentNode.Connect(startNode, endNode);

                lineSegments[lineIndex] = new TriangleLineSegment(startNode, endNode);

                lineIndex++;
            }

            EdgeLines = new ReadOnlyCollection<TriangleLineSegment>(lineSegments);
        }
    }
}
