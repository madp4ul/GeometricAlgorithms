﻿using GeometricAlgorithms.Domain;
using GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.PointPartitioning;
using GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.Triangulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree
{
    class EdgeTreeNode
    {
        public readonly CubeOutsides Sides;
        public readonly OctreeNode OctreeNode;

        public readonly int Depth;

        public EdgeTreeNode[,,] Children { get; private set; }
        public bool HasChildren => Children != null;

        public IList<PositionTriangle> LastTriangulation { get; private set; }

        /// <summary>
        /// Private constructor only for creation of root node
        /// </summary>
        /// <param name="octreeNode"></param>
        /// <param name="implicitSurface"></param>
        /// <param name="boundingBox"></param>
        private EdgeTreeNode(OctreeNode octreeNode, ImplicitSurfaceProvider implicitSurface, int depth)
            : this(octreeNode, CubeOutsides.ForRoot(implicitSurface, octreeNode.BoundingBox), depth)
        { }

        private EdgeTreeNode(OctreeNode octreeNode, CubeOutsides outsides, int depth)
        {
            OctreeNode = octreeNode;
            Sides = outsides;
            Depth = depth;
        }

        public void CreateChildren()
        {
            if (HasChildren)
            {
                throw new InvalidOperationException();
            }

            OctreeNode.CreateChildren();
            Sides.CreateChildren();

            var children = new EdgeTreeNode[2, 2, 2];

            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < 2; y++)
                {
                    for (int z = 0; z < 2; z++)
                    {
                        children[x, y, z] = new EdgeTreeNode(
                            octreeNode: OctreeNode.Children[x, y, z],
                            outsides: Sides.Children[x, y, z],
                            depth: Depth + 1);
                    }
                }
            }

            Children = children;
        }
        public void AddTriangulation(SurfaceApproximation approximation)
        {
            var sideLineSegments = new List<TriangleLineSegment>();

            foreach (var side in Sides)
            {
                //TODO if any side already contains circles, use a different triangulation method for 
                //polynoms in a plane

                sideLineSegments.AddRange(side.Value.GetLineSegments(approximation, side.Key.IsMax));
            }

            var mergedSegments = TriangleLineSegment.Merge(sideLineSegments);

            if (mergedSegments.Any(s => !s.IsFirstSameAsLast))
            {
                throw new ApplicationException("Should not happen.");
            }

            var triangulation = new List<PositionTriangle>();

            foreach (var circle in mergedSegments.Cast<MergedTriangleLineSegment>())
            {
                var triangles = circle.TriangulateCircle();

                triangulation.AddRange(triangles);
            }

            LastTriangulation = triangulation;

            approximation.AddFaces(triangulation);
        }

        public static EdgeTreeNode CreateRoot(OctreeNode octreeNode, ImplicitSurfaceProvider implicitSurface)
        {
            return new EdgeTreeNode(octreeNode, implicitSurface, depth: 0);
        }
    }
}
