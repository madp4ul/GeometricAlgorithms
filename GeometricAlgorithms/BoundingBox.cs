using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms
{
    public class BoundingBox
    {
        public Vector3 Minimum { get; private set; }
        public Vector3 Maximum { get; private set; }

        public BoundingBox(Vector3 minimum, Vector3 maximum)
        {
            Minimum = minimum;
            Maximum = maximum;
        }

        /// <summary>
        /// Returns a distance smaller or equal to the actual distance of the position to the box.
        /// Its trying to find the best approximation to the actual distance with minimal computation.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public float GetMinimumDistance(Vector3 position)
        {
            float distanceX = GetDistanceAlongDimension(position, p => p.X);
            float distanceY = GetDistanceAlongDimension(position, p => p.Y);
            float distanceZ = GetDistanceAlongDimension(position, p => p.Z);

            return Math.Max(Math.Max(distanceX, distanceY), distanceZ);
        }

        private float GetDistanceAlongDimension(Vector3 position, Func<Vector3, float> dimensionSelector)
        {
            float positionAlongDimension = dimensionSelector(position);
            float minimumAlongDimension = dimensionSelector(Minimum);
            float maximumAlongDimension = dimensionSelector(Maximum);

            if (positionAlongDimension > maximumAlongDimension)
            {
                return positionAlongDimension - maximumAlongDimension;
            }
            else if (positionAlongDimension < minimumAlongDimension)
            {
                return minimumAlongDimension - positionAlongDimension;
            }
            else
            {
                return 0;
            }
        }
    }
}
