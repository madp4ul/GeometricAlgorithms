using GeometricAlgorithms.Domain;
using GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.Approximation;
using GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.Cubes;
using GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.PointPartitioning;
using GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.Triangulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.Nodes
{
    class RefinementTreeNode
    {
        private readonly RefiningApproximation Approximation;

        public readonly CubeOutsides Sides;
        public readonly OctreeNode OctreeNode;

        public readonly int Depth;

        public RefinementTreeNode[,,] Children { get; private set; }
        public bool HasChildren => Children != null;

        public NodeTriangulation Triangulation { get; private set; }

        /// <summary>
        /// Private constructor only for creation of root node
        /// </summary>
        /// <param name="octreeNode"></param>
        /// <param name="implicitSurface"></param>
        /// <param name="boundingBox"></param>
        private RefinementTreeNode(OctreeNode octreeNode, RefiningApproximation approximation, ImplicitSurfaceProvider implicitSurface, int depth)
            : this(octreeNode, approximation, CubeOutsides.ForRoot(approximation, implicitSurface, octreeNode.BoundingBox), depth)
        { }

        private RefinementTreeNode(OctreeNode octreeNode, RefiningApproximation approximation, CubeOutsides outsides, int depth)
        {
            OctreeNode = octreeNode;
            Approximation = approximation;
            Sides = outsides;
            Depth = depth;

            AddSelfToSides();
            CreateTriangulation();
        }

        private void AddSelfToSides()
        {
            foreach (var orientedSide in Sides)
            {
                orientedSide.Side.UsingNodes[orientedSide.Orientation] = this;

                //add self to edges of the side
                for (int axisIndex = 0; axisIndex < 2; axisIndex++)
                {
                    for (int axisDirection = 0; axisDirection < 2; axisDirection++)
                    {
                        var edgeOrientation = orientedSide.Orientation.GetEdgeOrientation(axisIndex, axisDirection);
                        var edgeAtOrientation = orientedSide.Side.Edges[axisIndex, axisDirection];

                        edgeAtOrientation.UsingNodes[edgeOrientation] = this;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Influenced neighbouring nodes</returns>
        public List<RefinementTreeNode> CreateChildren()
        {
            if (HasChildren)
            {
                throw new InvalidOperationException();
            }

            OctreeNode.CreateChildren();
            Sides.CreateChildren();

            var children = new RefinementTreeNode[2, 2, 2];

            var lessRefinedNeighbours = new List<RefinementTreeNode>();

            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < 2; y++)
                {
                    for (int z = 0; z < 2; z++)
                    {
                        children[x, y, z] = new RefinementTreeNode(
                            octreeNode: OctreeNode.Children[x, y, z],
                            approximation: Approximation,
                            outsides: Sides.Children[x, y, z],
                            depth: Depth + 1);

                        var retriangulatedNeighbours = children[x, y, z].GetLessRefinedNeighbours();
                        lessRefinedNeighbours.AddRange(retriangulatedNeighbours);
                    }
                }
            }

            lessRefinedNeighbours = lessRefinedNeighbours.Distinct().ToList();
            RetriangulateLessRefinedNeighbours(lessRefinedNeighbours);

            Children = children;

            if (Triangulation != null)
            {
                Triangulation.Dispose();
                Triangulation = null;
            }

            return lessRefinedNeighbours;
        }

        private static int count = 0;
        public void CreateTriangulation()
        {
            count++;

            if (HasChildren)
            {
                throw new InvalidOperationException();
            }

            if (Triangulation != null)
            {
                Triangulation.Dispose();
                Triangulation = null;
            }

            var sideLineSegments = new List<TriangleLineSegment>();

            foreach (var orientedSide in Sides)
            {
                //TODO if any side already contains circles, use a different triangulation method for 
                //polynoms in a plane

                sideLineSegments.AddRange(orientedSide.Side.GetLineSegments(orientedSide.Orientation.IsMax));
            }

            var mergedSegments = TriangleLineSegment.Merge(sideLineSegments);

            if (mergedSegments.Any(s => !s.IsFirstSameAsLast))
            {
                throw new ApplicationException("Should not happen.");
            }

            var triangulation = new List<EditableIndexTriangle>();

            foreach (var circle in mergedSegments.Cast<MergedTriangleLineSegment>())
            {
                var triangles = circle.TriangulateCircle();

                triangulation.AddRange(triangles);
            }

            if (triangulation.Count > 0)
            {
                Triangulation = Approximation.AddTriangulation(triangulation);
            }
        }

        private void RetriangulateLessRefinedNeighbours(List<RefinementTreeNode> lessRefinedNeighbours)
        {
            foreach (var neighbour in lessRefinedNeighbours)
            {
                neighbour.CreateTriangulation();
            }
        }

        /// <summary>
        /// Get unique neighbours, whoose depth is less than depth of current node
        /// </summary>
        /// <returns></returns>
        private List<RefinementTreeNode> GetLessRefinedNeighbours()
        {
            var distinctEdges = Sides
                .Select(s => s.Side)
                .SelectMany(s => s.Edges)
                .Distinct()
                .ToList();

            var edgeSharingNodes = distinctEdges.Select(e => e.UsingNodes.GetLessRefinedLeafAtMirroredPosition(this));
            var sideSharingNodes = Sides.Select(s => s.Side.UsingNodes.GetLessRefinedNodeAtMirroredPosition(this));

            var neighbours = edgeSharingNodes.Concat(sideSharingNodes)
                 .Where(n => n != null)
                 .ToList();

            return neighbours;
        }

        public static RefinementTreeNode CreateRoot(OctreeNode octreeNode, RefiningApproximation approximation, ImplicitSurfaceProvider implicitSurface)
        {
            return new RefinementTreeNode(octreeNode, approximation, implicitSurface, depth: 0);
        }
    }
}
