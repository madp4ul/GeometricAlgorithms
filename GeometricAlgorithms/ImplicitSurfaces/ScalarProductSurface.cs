using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometricAlgorithms.Domain;
using GeometricAlgorithms.MeshQuerying;

namespace GeometricAlgorithms.ImplicitSurfaces
{
    public class ScalarProductSurface : IImplicitSurface
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

            float furthestDistance = nearestPositions.Keys[nearestPositions.Count - 1];

            float weightedAverageFunctionValue = 0;
            float sumOfWeights = 0;

            foreach (var neighbour in nearestPositions)
            {
                Vector3 normalizedNeighbourToPosition = (position - neighbour.Value.Position) / neighbour.Key;

                //dot product of neighbour normal and vector from neighbour to position to 
                //get a value that represents how much the position is infront or behind the surface         
                float side = Vector3.Dot(KdTree.Mesh.UnitNormals[neighbour.Value.OriginalIndex], normalizedNeighbourToPosition);

                float weight = GetWeight(furthestDistance, neighbour.Key);
                sumOfWeights += weight;

                //multiply side with distance because of the distance is high the neighbour must be further away.
                weightedAverageFunctionValue += side * neighbour.Key * weight;

            }

            sumOfWeights /= UsedNearestPointCount;
            weightedAverageFunctionValue /= sumOfWeights;

            return weightedAverageFunctionValue;
        }

        /// <summary>
        /// Get a weight by distance ranging from 0 to 1
        /// </summary>
        /// <param name="maxDistance"></param>
        /// <param name="currentDistance"></param>
        /// <returns></returns>
        private float GetWeight(float maxDistance, float currentDistance)
        {
            return WendlandWeight(maxDistance, currentDistance);
        }

        private float WendlandWeight(float maxDistance, float currentDistance)
        {
            float relativeDistance = currentDistance / maxDistance;

            float relativeCloseness = 1 - relativeDistance;
            float relativeCloseness2 = relativeCloseness * relativeCloseness;
            float relativeCloseness4 = relativeCloseness2 * relativeCloseness2;

            float b = 4 * relativeDistance + 1;

            return relativeCloseness4 * b;
        }
    }
}
