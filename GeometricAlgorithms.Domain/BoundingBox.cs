using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Domain
{
    public class BoundingBox : ICloneable
    {
        public Vector3 Minimum { get; private set; }
        public Vector3 Maximum { get; private set; }

        public Vector3 Diagonal => Maximum - Minimum;

        public float Volume
        {
            get
            {
                Vector3 diff = (Maximum - Minimum);
                return diff.X * diff.Y * diff.Z;
            }
        }

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

        public float GetDistanceAlongDimension(Vector3 position, Func<Vector3, float> dimensionSelector)
        {
            float positionAlongDimension = dimensionSelector(position);

            float distance = GetDistanceAboveDimension(positionAlongDimension, dimensionSelector);
            if (distance > 0)
            {
                return distance;
            }

            return GetDistanceBelowDimension(positionAlongDimension, dimensionSelector);
        }

        public float GetDistanceAboveDimension(float positionAlongDimension, Func<Vector3, float> dimensionSelector)
        {
            float maximumAlongDimension = dimensionSelector(Maximum);

            if (positionAlongDimension > maximumAlongDimension)
            {
                return positionAlongDimension - maximumAlongDimension;
            }
            else
            {
                return 0;
            }
        }

        public float GetDistanceBelowDimension(float positionAlongDimension, Func<Vector3, float> dimensionSelector)
        {
            float minimumAlongDimension = dimensionSelector(Minimum);

            if (positionAlongDimension < minimumAlongDimension)
            {
                return minimumAlongDimension - positionAlongDimension;
            }
            else
            {
                return 0;
            }
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        public BoundingBox Clone()
        {
            return new BoundingBox(Minimum, Maximum);
        }

        public BoundingBox GetMinHalfAlongDimension(Dimension dimension, float upperBound)
        {
            return new BoundingBox(Minimum,
                 new Vector3(
                    dimension == Dimension.X ? upperBound : Maximum.X,
                    dimension == Dimension.Y ? upperBound : Maximum.Y,
                    dimension == Dimension.Z ? upperBound : Maximum.Z));
        }

        public BoundingBox GetMaxHalfAlongDimension(Dimension dimension, float lowerBound)
        {
            return new BoundingBox(
                new Vector3(
                    dimension == Dimension.X ? lowerBound : Minimum.X,
                    dimension == Dimension.Y ? lowerBound : Minimum.Y,
                    dimension == Dimension.Z ? lowerBound : Minimum.Z),
                 Maximum);
        }

        public static BoundingBox CreateContainer<TVertex>(IEnumerable<TVertex> vertices) where TVertex : IVertex
        {
            float minX = float.MaxValue;
            float minY = float.MaxValue;
            float minZ = float.MaxValue;

            float maxX = float.MinValue;
            float maxY = float.MinValue;
            float maxZ = float.MinValue;

            foreach (TVertex vertex in vertices)
            {
                if (vertex.Position.X < minX)
                {
                    minX = vertex.Position.X;
                }
                if (vertex.Position.Y < minY)
                {
                    minY = vertex.Position.Y;
                }
                if (vertex.Position.Z < minZ)
                {
                    minZ = vertex.Position.Z;
                }

                if (vertex.Position.X > maxX)
                {
                    maxX = vertex.Position.X;
                }
                if (vertex.Position.Y > maxY)
                {
                    maxY = vertex.Position.Y;
                }
                if (vertex.Position.Z > maxZ)
                {
                    maxZ = vertex.Position.Z;
                }
            }

            return new BoundingBox(new Vector3(minX, minY, minZ), new Vector3(maxX, maxY, maxZ));
        }
    }
}
