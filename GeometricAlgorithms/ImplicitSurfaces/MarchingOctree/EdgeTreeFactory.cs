using GeometricAlgorithms.Domain;
using GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.PointPartitioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree
{
    public static class EdgeTreeFactory
    {
        public static IEdgeTree CreateWithWithMostPointsFirst(IImplicitSurface implicitSurface, Mesh mesh, float containerScale = 1.3f)
        {
            OctreePartitioning partitioning = new OctreePartitioning(mesh, containerScale);

            return new EdgeTree<int>(implicitSurface, partitioning, GetVertexCount);
        }

        /// <summary>
        /// This will 
        /// </summary>
        /// <param name="implicitSurface"></param>
        /// <param name="mesh"></param>
        /// <param name="containerScale"></param>
        /// <returns></returns>
        public static IEdgeTree CreateWithFurthestPointsFirst(IImplicitSurface implicitSurface, Mesh mesh, float containerScale = 1.3f)
        {
            OctreePartitioning partitioning = new OctreePartitioning(mesh, containerScale);

            return new EdgeTree<float>(implicitSurface, partitioning, GetMaximumVertexToSurfaceDistance);
        }

        static int GetVertexCount(EdgeTreeNode node) => -node.OctreeNode.Vertices.Length;

        static float GetMaximumVertexToSurfaceDistance(EdgeTreeNode node)
        {
            if (node.LastTriangulation == null)
            {
                return GetVertexCount(node);
            }

            float furthestDistance = float.NegativeInfinity;

            foreach (var vertex in node.OctreeNode.Vertices)
            {
                float closestTriangleDistance = float.PositiveInfinity;

                foreach (var triangle in node.LastTriangulation)
                {
                    float triangleDistance = triangle.GetDistance(vertex.Position);

                    if (triangleDistance < closestTriangleDistance)
                    {
                        closestTriangleDistance = triangleDistance;
                    }
                }

                if (closestTriangleDistance > furthestDistance)
                {
                    furthestDistance = closestTriangleDistance;
                }
            }

            return -furthestDistance;
        }
    }
}
