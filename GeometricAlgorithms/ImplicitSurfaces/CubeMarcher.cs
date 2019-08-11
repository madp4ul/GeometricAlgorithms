using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Tasks;
using GeometricAlgorithms.ImplicitSurfaces.MarchingCubes;
using GeometricAlgorithms.MeshQuerying;
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

        public Mesh GetSurface(FunctionValueGrid functionValueGrid, IProgressUpdater progressUpdater = null)
        {
            var edgeValues = new EdgeValueGrid(functionValueGrid);

            var operationUpdater = new OperationProgressUpdater(progressUpdater, edgeValues.CubesTotal, "Marching cubes");

            edgeValues.Compute(operationUpdater);

            operationUpdater.IsCompleted();

            return edgeValues.ComputedSurface;
        }

        public FunctionValueGrid GetFunctionValues(IProgressUpdater progressUpdater = null)
        {
            var operationUpdater = new OperationProgressUpdater(progressUpdater, TotalValues, "Computing implicit function samples");

            var result = new FunctionValueGrid(
                StepsAlongSide,
                BoundingBox.Minimum,
                BoundingBox.Diagonal / StepsAlongSide);

            result.Compute(Surface, operationUpdater);

            operationUpdater.IsCompleted();

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
