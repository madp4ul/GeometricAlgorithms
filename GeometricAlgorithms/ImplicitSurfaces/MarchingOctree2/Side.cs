using GeometricAlgorithms.Domain;
using GeometricAlgorithms.ImplicitSurfaces.MarchingOctree2.Triangulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree2
{
    class Side
    {
        private readonly ImplicitSurfaceProvider ImplicitSurface;

        public readonly SideDimensions Dimensions;

        public readonly SideOutsideEdges Edges;

        public SideChildren Children { get; private set; }
        public bool HasChildren => Children != null;

        public Side(ImplicitSurfaceProvider implicitSurface, SideOutsideEdges edges)
        {
            if (edges.IsEdgeMissing)
            {
                throw new ArgumentException("Side can only be constructed from 4 edges.");
            }

            ImplicitSurface = implicitSurface;
            Edges = edges;
            Dimensions = edges.Dimensions;
        }

        public void CreateChildren()
        {
            if (HasChildren)
            {
                throw new InvalidOperationException("Cant compute children twice.");
            }

            var insideEdges = new SideInsideEdges(ImplicitSurface, Edges);

            Side[,] childSides = new Side[2, 2];

            {
                SideOutsideEdges childEdges = new SideOutsideEdges(Dimensions.DirectionAxisFromCubeCenter);
                childEdges[0, 0] = Edges[0, 0].Children[0];
                childEdges[0, 1] = insideEdges[1, 0];
                childEdges[1, 0] = Edges[1, 0].Children[0];
                childEdges[1, 1] = insideEdges[0, 0];
                childSides[0, 0] = new Side(ImplicitSurface, childEdges);
            }

            {
                SideOutsideEdges childEdges = new SideOutsideEdges(Dimensions.DirectionAxisFromCubeCenter);
                childEdges[0, 0] = Edges[0, 0].Children[1];
                childEdges[0, 1] = insideEdges[1, 1];
                childEdges[1, 0] = insideEdges[0, 0];
                childEdges[1, 1] = Edges[1, 1].Children[0];
                childSides[0, 1] = new Side(ImplicitSurface, childEdges);
            }

            {
                SideOutsideEdges childEdges = new SideOutsideEdges(Dimensions.DirectionAxisFromCubeCenter);
                childEdges[0, 0] = insideEdges[1, 0];
                childEdges[0, 1] = Edges[0, 1].Children[0];
                childEdges[1, 0] = Edges[1, 0].Children[1];
                childEdges[1, 1] = insideEdges[0, 1];
                childSides[1, 0] = new Side(ImplicitSurface, childEdges);
            }

            {
                SideOutsideEdges childEdges = new SideOutsideEdges(Dimensions.DirectionAxisFromCubeCenter);
                childEdges[0, 0] = insideEdges[1, 1];
                childEdges[0, 1] = Edges[0, 1].Children[1];
                childEdges[1, 0] = insideEdges[0, 1];
                childEdges[1, 1] = Edges[1, 1].Children[1];
                childSides[1, 1] = new Side(ImplicitSurface, childEdges);
            }

            Children = new SideChildren(childSides[0, 0], childSides[0, 1], childSides[1, 0], childSides[1, 1]);
        }

        public List<TriangleLineSegment> GetLineSegments(SurfaceApproximation approximation)
        {
            if (HasChildren)
            {
                return GetMergedChildLineSegments(approximation);
            }
            else
            {
                return GetLineSegmentsFromTriagulationTable(approximation);
            }
        }

        private List<TriangleLineSegment> GetMergedChildLineSegments(SurfaceApproximation approximation)
        {
            var childSegments = new List<TriangleLineSegment>();
            childSegments.AddRange(Children[0, 0].GetLineSegments(approximation));
            childSegments.AddRange(Children[0, 1].GetLineSegments(approximation));
            childSegments.AddRange(Children[1, 0].GetLineSegments(approximation));
            childSegments.AddRange(Children[1, 1].GetLineSegments(approximation));

            return TriangleLineSegment.Merge(childSegments);
        }

        private List<TriangleLineSegment> GetLineSegmentsFromTriagulationTable(SurfaceApproximation approximation)
        {
            Edge minEdge = Edges[0, 0];
            Edge maxEdge = Edges[0, 1];



            var lineSegmentDefinition = SideTriangulationTable.GetDefinitionByFunctionValue(
                is00Inside: minEdge.MinValue.IsInside,
                is01Inside: minEdge.MaxValue.IsInside,
                is10Inside: maxEdge.MinValue.IsInside,
                is11Inside: maxEdge.MaxValue.IsInside);

            var result = new List<TriangleLineSegment>();

            for (int i = 0; i < lineSegmentDefinition.Length; i++)
            {
                ref LineSegmentDefinition current = ref lineSegmentDefinition[i];

                var startEdge = Edges[current.LineStart.DimensionIndex, current.LineStart.DirectionIndex];
                var startEdgeIntersection = startEdge.GetSurfaceIntersectionPositionIndices(approximation);
                var startNode = new TriangleLineSegmentNode(startEdgeIntersection.IntersectionIndex.Value);

                var endEdge = Edges[current.LineEnd.DimensionIndex, current.LineEnd.DirectionIndex];
                var endEdgeIntersection = endEdge.GetSurfaceIntersectionPositionIndices(approximation);
                var endNode = new TriangleLineSegmentNode(endEdgeIntersection.IntersectionIndex.Value);

                startNode.Next = endNode;
                endNode.Previous = startNode;

                result.Add(new TriangleLineSegment(startNode, endNode));
            }

            foreach (var edge in Edges)
            {
                var edgeLines = edge.GetSurfaceIntersectionPositionIndices(approximation).EdgeLines;

                result.AddRange(edgeLines);
            }

            return result;
        }

        public override string ToString()
        {
            string hasChildren = HasChildren ? "has children" : "no children";

            return $"{{side: {Dimensions.DirectionAxisFromCubeCenter.ToString()}, {hasChildren}}}";
        }
    }
}
