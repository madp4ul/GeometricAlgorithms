using GeometricAlgorithms.Domain;
using GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.Nodes;
using GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.PointPartitioning;
using GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.RefinementTrees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree
{
    public static class RefinementTreeFactory
    {
        public static IRefinementTree CreateWithWithMostPointsFirst(IImplicitSurface implicitSurface, Mesh mesh, float containerScale = 1.3f)
        {
            OctreePartitioning partitioning = new OctreePartitioning(mesh, containerScale);

            return new RefinementTree<int>(implicitSurface, partitioning, GetVertexCount);
        }

        /// <summary>
        /// This will only benefit the selection with many regular triangulations
        /// </summary>
        /// <param name="implicitSurface"></param>
        /// <param name="mesh"></param>
        /// <param name="containerScale"></param>
        /// <returns></returns>
        public static IRefinementTree CreateWithFurthestPointsFirst(IImplicitSurface implicitSurface, Mesh mesh, float containerScale = 1.3f)
        {
            OctreePartitioning partitioning = new OctreePartitioning(mesh, containerScale);

            return new TriangulationBasedPriorityTree<float>(implicitSurface, partitioning, GetMaximumVertexToSurfaceDistance);
        }

        static int GetVertexCount(RefinementTreeNode node) => -node.OctreeNode.Vertices.Length;

        static float GetMaximumVertexToSurfaceDistance(RefinementTreeNode node)
        {
            if (node.LastTriangulation == null)
            {
                //Add penalty to vertex count method so that any node with 
                //calculated triangulation will be preffered.
                return 1000f + GetVertexCount(node);
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
