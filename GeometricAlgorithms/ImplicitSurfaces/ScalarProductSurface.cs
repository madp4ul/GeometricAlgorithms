using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometricAlgorithms.Domain;
using GeometricAlgorithms.MeshQuerying;

namespace GeometricAlgorithms.ImplicitSurfaces
{
    public class ScalarProductSurface : IFiniteImplicitSurface
    {
        public readonly ATree Tree;
        public int UsedNearestPointCount;

        public ScalarProductSurface(ATree tree, int usedNearestPointCount)
        {
            Tree = tree ?? throw new ArgumentNullException(nameof(tree));
            UsedNearestPointCount = usedNearestPointCount;

            DefinedArea = Tree.MeshContainer;
        }

        public BoundingBox DefinedArea { get; private set; }

        public float GetApproximateSurfaceDistance(Vector3 position)
        {
            //All the neighbours are assumed to be on the surface
            var nearestPositionQueue = Tree.FindNearestVertices(position, UsedNearestPointCount);
            var unorderedPositions = nearestPositionQueue.GetMinHeap();

            //If a vertex with distance 0 exists, the position is on the surface.
            //Also a few calculations below dont work in that case but we already know that the distance 
            //must be 0, so return.
            if (unorderedPositions.Any(pid => pid.Distance == 0))
            {
                return 0;
            }

            float furthestDistance = nearestPositionQueue.Peek().Distance;

            float weightedAverageFunctionValue = 0;
            float sumOfWeights = 0;

            foreach (var neighbour in unorderedPositions)
            {
                Vector3 normalizedNeighbourToPosition = (position - neighbour.PositionIndex.Position) / neighbour.Distance;

                //dot product of neighbour normal and vector from neighbour to position to 
                //get a value that represents how much the position is infront or behind the surface         
                float side = Vector3.Dot(Tree.Mesh.UnitNormals[neighbour.PositionIndex.Index], normalizedNeighbourToPosition);

                float weight = GetWeight(furthestDistance, neighbour.Distance);
                sumOfWeights += weight;

                //multiply side with distance because of the distance is high the neighbour must be further away.
                float addedFunctionValue = side * neighbour.Distance * weight;
                weightedAverageFunctionValue += addedFunctionValue;
            }

            sumOfWeights /= UsedNearestPointCount;
            weightedAverageFunctionValue /= sumOfWeights;

            if (float.IsNaN(weightedAverageFunctionValue))
            {
                throw new ApplicationException();
            }

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
