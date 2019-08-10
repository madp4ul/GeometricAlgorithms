using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingCubes
{
    public class CubeMarcher
    {
        public BoundingBox BoundingBox { get; set; }
        public int StepsAlongSide { get; set; }

        public IImplicitSurface Surface { get; set; }

        public FunctionValueGrid FunctionValueGrid { get; private set; }

        public int TotalValues => StepsAlongSide * StepsAlongSide * StepsAlongSide;

        public CubeMarcher(BoundingBox boundingBox, IImplicitSurface surface, int stepsAlongSide = 16)
        {
            BoundingBox = boundingBox ?? throw new ArgumentNullException(nameof(boundingBox));
            StepsAlongSide = stepsAlongSide;
            Surface = surface ?? throw new ArgumentNullException(nameof(surface));
        }

        public Mesh MarchCubes()
        {
            FunctionValueGrid = GetFunctionValues();
            var edgeValues = new EdgeValueGrid(FunctionValueGrid);

            var triangles = edgeValues.Compute();

            Mesh result = new Mesh(edgeValues.Vertices.ToArray(), triangles.ToArray());

            return result;
        }

        private FunctionValueGrid GetFunctionValues()
        {
            var result = new FunctionValueGrid(
                StepsAlongSide,
                BoundingBox.Minimum,
                BoundingBox.Diagonal / StepsAlongSide);

            result.Compute(Surface);

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
