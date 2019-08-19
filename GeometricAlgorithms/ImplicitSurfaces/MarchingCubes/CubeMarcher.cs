using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Tasks;
using GeometricAlgorithms.ImplicitSurfaces.MarchingCubes;
using GeometricAlgorithms.MeshQuerying;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingCubes
{
    public class CubeMarcher
    {
        public BoundingBox BoundingBox { get; private set; }
        public Point Steps { get; private set; }
        public int StepsAlongLongestSide { get; private set; }

        public IImplicitSurface Surface { get; private set; }

        public int TotalValues => Steps.X * Steps.Y * Steps.Z;

        public CubeMarcher(BoundingBox boundingBox, IImplicitSurface surface, int stepsAlongLongestSide = 16)
        {
            BoundingBox = boundingBox ?? throw new ArgumentNullException(nameof(boundingBox));
            Surface = surface ?? throw new ArgumentNullException(nameof(surface));
            SetSteps(stepsAlongLongestSide);
        }

        public void SetSteps(int stepsAlongLongestSide)
        {
            StepsAlongLongestSide = stepsAlongLongestSide;

            Vector3 boundingBoxDiagonal = BoundingBox.Diagonal;
            float longestLength = boundingBoxDiagonal.MaximumComponent();
            Steps = new Point(
               (int)Math.Ceiling((boundingBoxDiagonal.X / longestLength) * stepsAlongLongestSide),
               (int)Math.Ceiling((boundingBoxDiagonal.Y / longestLength) * stepsAlongLongestSide),
               (int)Math.Ceiling((boundingBoxDiagonal.Z / longestLength) * stepsAlongLongestSide)
            );
        }

        public Mesh GetSurface(FunctionValueGrid functionValueGrid, IProgressUpdater progressUpdater = null)
        {
            var edgeValues = new EdgeValueGrid(functionValueGrid);

            var operationUpdater = new OperationProgressUpdater(progressUpdater, edgeValues.TotalCubes, "Marching cubes");

            edgeValues.Compute(operationUpdater);

            operationUpdater.IsCompleted();

            return edgeValues.ComputedSurface;
        }

        public FunctionValueGrid GetFunctionValues(IProgressUpdater progressUpdater = null)
        {
            var operationUpdater = new OperationProgressUpdater(progressUpdater, TotalValues, "Computing implicit function samples");

            float longestLength = BoundingBox.Diagonal.MaximumComponent();
            var result = new FunctionValueGrid(
              steps: Steps,
              minCorner: BoundingBox.Minimum,
              stepSize: longestLength / (StepsAlongLongestSide - 1)); //-1 because the first step stays at zero.
            //So there is one step less to get from min-corner to max-corner

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

        public static CubeMarcher CreateDummy()
        {
            return new CubeMarcher(new BoundingBox(Vector3.Zero, Vector3.Zero), new EmptySurface(), 2);
        }
    }
}
