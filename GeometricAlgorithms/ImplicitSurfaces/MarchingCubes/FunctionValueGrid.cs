using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingCubes
{

    public class FunctionValueGrid
    {
        public readonly FunctionValue[] FunctionValues;

        public readonly int Steps;
        public readonly Vector3 MinCorner;
        public readonly Vector3 StepSize;

        public FunctionValueGrid(int steps, Vector3 minCorner, Vector3 stepSize)
        {
            Steps = steps;
            MinCorner = minCorner;
            StepSize = stepSize;
            FunctionValues = new FunctionValue[steps * steps * steps];

        }

        public void Compute(IImplicitSurface implicitSurface, OperationProgressUpdater progressUpdater)
        {
            int valueIndex = 0;

            for (int x = 0; x < Steps; x++)
            {
                for (int y = 0; y < Steps; y++)
                {
                    for (int z = 0; z < Steps; z++)
                    {
                        Vector3 probePosition =
                            MinCorner
                            + new Vector3(
                                StepSize.X * x,
                                StepSize.Y * y,
                                StepSize.Z * z);

                        float approximateDistance = implicitSurface.GetApproximateSurfaceDistance(probePosition);

                        FunctionValues[valueIndex] = new FunctionValue(probePosition, approximateDistance);

                        valueIndex++;
                    }

                    progressUpdater.UpdateAddOperation(operationCount: Steps);
                }
            }
        }

        public FunctionValue GetValue(int x, int y, int z)
        {
            return FunctionValues[x * Steps * Steps + y * Steps + z];
        }



        //1: add function values
        //2: compute vertices on edges
        //3: collect triangles
    }
}
