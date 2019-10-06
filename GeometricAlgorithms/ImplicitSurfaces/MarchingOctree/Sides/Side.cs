using GeometricAlgorithms.Domain;
using GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.Approximation;
using GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.Edges;
using GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.Triangulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.Sides
{
    class Side
    {
        private readonly ImplicitSurfaceProvider ImplicitSurface;
        private readonly RefiningApproximation Approximation;

        public readonly SideDimensions Dimensions;

        public readonly SideOutsideEdges Edges;

        public SideChildren Children { get; private set; }
        public bool HasChildren => Children != null;

        public Side(RefiningApproximation approximation, ImplicitSurfaceProvider implicitSurface, SideOutsideEdges edges)
        {
            if (edges.IsEdgeMissing)
            {
                throw new ArgumentException("Side can only be constructed from 4 edges.");
            }

            Approximation = approximation;
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

            var insideEdges = new SideInsideEdges(Approximation, ImplicitSurface, Edges);

            Side[,] childSides = new Side[2, 2];

            {
                SideOutsideEdges childEdges = new SideOutsideEdges(Dimensions.DirectionAxisFromCubeCenter);
                childEdges[0, 0] = Edges[0, 0].Children[0];
                childEdges[0, 1] = insideEdges[1, 0];
                childEdges[1, 0] = Edges[1, 0].Children[0];
                childEdges[1, 1] = insideEdges[0, 0];
                childSides[0, 0] = new Side(Approximation, ImplicitSurface, childEdges);
            }

            {
                SideOutsideEdges childEdges = new SideOutsideEdges(Dimensions.DirectionAxisFromCubeCenter);
                childEdges[0, 0] = Edges[0, 0].Children[1];
                childEdges[0, 1] = insideEdges[1, 1];
                childEdges[1, 0] = insideEdges[0, 0];
                childEdges[1, 1] = Edges[1, 1].Children[0];
                childSides[0, 1] = new Side(Approximation, ImplicitSurface, childEdges);
            }

            {
                SideOutsideEdges childEdges = new SideOutsideEdges(Dimensions.DirectionAxisFromCubeCenter);
                childEdges[0, 0] = insideEdges[1, 0];
                childEdges[0, 1] = Edges[0, 1].Children[0];
                childEdges[1, 0] = Edges[1, 0].Children[1];
                childEdges[1, 1] = insideEdges[0, 1];
                childSides[1, 0] = new Side(Approximation, ImplicitSurface, childEdges);
            }

            {
                SideOutsideEdges childEdges = new SideOutsideEdges(Dimensions.DirectionAxisFromCubeCenter);
                childEdges[0, 0] = insideEdges[1, 1];
                childEdges[0, 1] = Edges[0, 1].Children[1];
                childEdges[1, 0] = insideEdges[0, 1];
                childEdges[1, 1] = Edges[1, 1].Children[1];
                childSides[1, 1] = new Side(Approximation, ImplicitSurface, childEdges);
            }

            Children = new SideChildren(childSides[0, 0], childSides[0, 1], childSides[1, 0], childSides[1, 1]);
        }

        public List<TriangleLineSegment> GetLineSegments(bool positiveOfCube)
        {
            if (HasChildren)
            {
                return GetMergedChildLineSegments(positiveOfCube);
            }
            else
            {
                return GetLineSegmentsFromTriagulationTable(positiveOfCube);
            }
        }

        private List<TriangleLineSegment> GetMergedChildLineSegments(bool positiveOfCube)
        {
            var childSegments = new List<TriangleLineSegment>();
            childSegments.AddRange(Children[0, 0].GetLineSegments(positiveOfCube));
            childSegments.AddRange(Children[0, 1].GetLineSegments(positiveOfCube));
            childSegments.AddRange(Children[1, 0].GetLineSegments(positiveOfCube));
            childSegments.AddRange(Children[1, 1].GetLineSegments(positiveOfCube));

            return TriangleLineSegment.Merge(childSegments);
        }

        private List<TriangleLineSegment> GetLineSegmentsFromTriagulationTable(bool positiveOfCube)
        {
            Edge minEdge = Edges[0, 0];
            Edge maxEdge = Edges[0, 1];

            var lineSegmentDefinition = SideTriangulationTable.GetDefinitionByFunctionValue(
                is00Inside: minEdge.MinValue.IsInside,
                is01Inside: minEdge.MaxValue.IsInside,
                is10Inside: maxEdge.MinValue.IsInside,
                is11Inside: maxEdge.MaxValue.IsInside);

            var result = new List<TriangleLineSegment>();

            var intersections = GetEdgeIntersections();

            for (int i = 0; i < lineSegmentDefinition.Length; i++)
            {
                ref LineSegmentDefinition current = ref lineSegmentDefinition[i];

                var startEdgeIntersection = intersections[current.LineStart.DimensionIndex, current.LineStart.DirectionIndex];
                var startNode = new TriangleLineSegmentNode(startEdgeIntersection.IntersectionIndex);

                var endEdgeIntersection = intersections[current.LineEnd.DimensionIndex, current.LineEnd.DirectionIndex];
                var endNode = new TriangleLineSegmentNode(endEdgeIntersection.IntersectionIndex);

                TriangleLineSegment resultSegment;
                if (positiveOfCube != Dimensions.SideOnlookedFromMin)
                {
                    TriangleLineSegmentNode.Connect(startNode, endNode);
                    resultSegment = new TriangleLineSegment(startNode, endNode);
                }
                else
                {
                    TriangleLineSegmentNode.Connect(endNode, startNode);
                    resultSegment = new TriangleLineSegment(endNode, startNode);
                }

                result.Add(resultSegment);
            }

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    var edgeLines = intersections[i, j].GetEdgeLines();

                    result.AddRange(edgeLines);
                }
            }

            return result;
        }

        private EdgeSurfaceIntersections[,] GetEdgeIntersections()
        {
            EdgeSurfaceIntersections[,] intersections = new EdgeSurfaceIntersections[2, 2];
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    intersections[i, j] = Edges[i, j].GetSurfaceIntersections();
                }
            }

            return intersections;
        }

        public override string ToString()
        {
            string hasChildren = HasChildren ? "has children" : "no children";

            return $"{{side: {Dimensions.DirectionAxisFromCubeCenter.ToString()}, {hasChildren}}}";
        }
    }
}
