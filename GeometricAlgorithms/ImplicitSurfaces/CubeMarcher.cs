using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces
{
    public class CubeMarcher
    {
        public BoundingBox BoundingBox { get; set; }
        public int StepsAlongSide { get; set; }

        public IImplicitSurface Surface { get; set; }

        public int TotalValues => StepsAlongSide * StepsAlongSide * StepsAlongSide;

        public CubeMarcher(BoundingBox boundingBox, IImplicitSurface surface, int stepsAlongSide = 16)
        {
            BoundingBox = boundingBox ?? throw new ArgumentNullException(nameof(boundingBox));
            StepsAlongSide = stepsAlongSide;
            Surface = surface ?? throw new ArgumentNullException(nameof(surface));
        }

        public MarchingCubeResult GetFunctionValues()
        {
            var values = new KeyValuePair<Vector3, float>[TotalValues];

            Vector3 probeAreaSize = BoundingBox.Diagonal;
            int valueIndex = 0;

            for (int x = 0; x < StepsAlongSide; x++)
            {
                for (int y = 0; y < StepsAlongSide; y++)
                {
                    for (int z = 0; z < StepsAlongSide; z++)
                    {
                        Vector3 probePosition =
                            BoundingBox.Minimum
                            + new Vector3(
                            probeAreaSize.X * (x / (float)StepsAlongSide),
                            probeAreaSize.Y * (y / (float)StepsAlongSide),
                            probeAreaSize.Z * (z / (float)StepsAlongSide));

                        float distance = Surface.GetApproximateSurfaceDistance(probePosition);

                        values[valueIndex] = new KeyValuePair<Vector3, float>(probePosition, distance);

                        valueIndex++;
                    }
                }
            }

            var result = new MarchingCubeResult
            {
                Result = values,
            };

            return result;
        }

        public static CubeMarcher AroundBox(
            BoundingBox boundingBox,
            float distanceAroundBox,
            IImplicitSurface surface,
            int stepsAlongSide = 16)
        {
            Vector3 distance3 = new Vector3(distanceAroundBox);

            var surroundingBox = new BoundingBox(boundingBox.Minimum - distance3, boundingBox.Maximum + distance3);

            return new CubeMarcher(surroundingBox, surface, stepsAlongSide);
        }
    }
}
