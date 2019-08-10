using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometricAlgorithms.Domain;
using GeometricAlgorithms.MeshQuerying;

namespace GeometricAlgorithms.ImplicitSurfaces
{
    class ScalarProductSurface : IImplicitSurface
    {
        public readonly KdTree KdTree;
        public readonly int UsedNearestPointCount;

        public ScalarProductSurface(KdTree kdTree, int usedNearestPointCount)
        {
            KdTree = kdTree ?? throw new ArgumentNullException(nameof(kdTree));
            UsedNearestPointCount = usedNearestPointCount;
        }

        public float GetApproximateSurfaceDistance(Vector3 position)
        {
            //All the neighbours are assumed to be on the surface
            var nearestPositions = KdTree.FindNearestVertices(position, UsedNearestPointCount);

            float averageFunctionValue = 0;

            foreach (var neighbour in nearestPositions)
            {
                Vector3 normalizedNeighbourToPosition = (position - neighbour.Value.Position) / neighbour.Key;

                //dot product of neighbour normal and vector from neighbour to position to 
                //get a value that represents how much the position is infront or behind the surface         
                float side = Vector3.Dot(KdTree.Mesh.FileUnitNormals[neighbour.Value.OriginalIndex], normalizedNeighbourToPosition);

                //multiply side with distance because of the distance is high the neighbour must be further away
                averageFunctionValue += side * neighbour.Key;

            }
            averageFunctionValue /= UsedNearestPointCount;

            return averageFunctionValue;
        }
    }
}
