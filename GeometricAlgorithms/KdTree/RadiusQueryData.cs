using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.KdTree
{
    class RadiusQueryData
    {
        public Vector3 Center { get; set; }

        public float[] Distances { get; set; }

        public float DistanceBelowMinimumX { get; set; }
        public float DistanceAboveMaximumX { get; set; }

        public float DistanceBelowMinimumY { get; set; }
        public float DistanceAboveMaximumY { get; set; }

        public float DistanceBelowMinimumZ { get; set; }
        public float DistanceAboveMaximumZ { get; set; }

        /// <summary>
        /// The currently highest value of the distances above.
        /// It also is the minimum distance to the bbox
        /// </summary>
        public float MaximumDistance { get; set; }
        private int MaxDistanceIndex;

        public RadiusQueryData()
        {
            Distances = new float[((int)Dimension.Count - 1) * 2];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dimension"></param>
        /// <param name="isAbove">If it shoudl update the distance of point greater than box</param>
        /// <param name="newDistance"></param>
        public void UpdateDistance(Dimension dimension, bool isAbove, float newDistance)
        {
            int index = (int)dimension + (isAbove ? 1 : 0);
            bool updateMaxDistance = index == MaxDistanceIndex;

            Distances[index] = newDistance;

            if (updateMaxDistance)
            {
                MaximumDistance = 0;
                for (int i = 0; i < Distances.Length; i++)
                {
                    if (Distances[i] > MaximumDistance)
                    {
                        MaximumDistance = Distances[i];
                        MaxDistanceIndex = i;
                    }
                }
            }
        }

        public float GetDistance(Dimension dimension, bool isAbove)
        {
            return Distances[(int)dimension + (isAbove ? 1 : 0)];
        }

        public static RadiusQueryData FromRootBoundingBox(BoundingBox box, Vector3 center)
        {

        }
    }
}
