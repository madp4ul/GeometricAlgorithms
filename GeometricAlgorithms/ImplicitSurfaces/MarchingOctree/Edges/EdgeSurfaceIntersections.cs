using GeometricAlgorithms.Domain;
using GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.Triangulation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.Edges
{
    class EdgeSurfaceIntersections
    {
        private readonly PositionIndex[] EdgeIntersectionIndices;

        private readonly int EdgeLinesStart;
        private readonly int EdgeLinesEnd;

        public readonly PositionIndex? IntersectionIndex;

        public EdgeSurfaceIntersections(bool isEdgeMinValueInside, PositionIndex[] edgeIntersectionIndices)
        {
            EdgeIntersectionIndices = edgeIntersectionIndices;

            EdgeLinesStart = 0;
            EdgeLinesEnd = edgeIntersectionIndices.Length;

            if (edgeIntersectionIndices.Length % 2 != 0)
            {
                if (isEdgeMinValueInside)
                {
                    IntersectionIndex = edgeIntersectionIndices[0];
                    EdgeLinesStart = 1;
                }
                else
                {
                    IntersectionIndex = edgeIntersectionIndices[edgeIntersectionIndices.Length - 1];
                    EdgeLinesEnd = edgeIntersectionIndices.Length - 1;
                }
            }
        }

        public TriangleLineSegment[] GetEdgeLines()
        {
            var lineSegments = new TriangleLineSegment[(EdgeLinesEnd - EdgeLinesStart) / 2];

            int lineIndex = 0;
            for (int i = EdgeLinesStart; i < EdgeLinesEnd; i += 2)
            {
                var startNode = new TriangleLineSegmentNode(EdgeIntersectionIndices[i]);
                var endNode = new TriangleLineSegmentNode(EdgeIntersectionIndices[i + 1]);

                TriangleLineSegmentNode.Connect(startNode, endNode);

                lineSegments[lineIndex] = new TriangleLineSegment(startNode, endNode);

                lineIndex++;
            }

            return lineSegments;
        }
    }
}
