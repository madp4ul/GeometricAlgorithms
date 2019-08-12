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

        public readonly int TotalSteps;
        public readonly Point StepAmounts;
        public readonly Vector3 MinCorner;
        public readonly float StepSize;

        public FunctionValueGrid(Point steps, Vector3 minCorner, float stepSize)
        {
            StepAmounts = steps;
            TotalSteps = steps.X * steps.Y * steps.Z;

            MinCorner = minCorner;
            StepSize = stepSize;
            FunctionValues = new FunctionValue[steps.X * steps.Y * steps.Z];

        }

        public void Compute(IImplicitSurface implicitSurface, OperationProgressUpdater progressUpdater)
        {
            int valueIndex = 0;

            for (int x = 0; x < StepAmounts.X; x++)
            {
                for (int y = 0; y < StepAmounts.Y; y++)
                {
                    for (int z = 0; z < StepAmounts.Z; z++)
                    {
                        Vector3 probePosition = MinCorner
                            + new Vector3(
                                StepSize * x,
                                StepSize * y,
                                StepSize * z);

                        float approximateDistance = implicitSurface.GetApproximateSurfaceDistance(probePosition);

                        FunctionValues[valueIndex] = new FunctionValue(probePosition, approximateDistance);

                        valueIndex++;
                    }

                    progressUpdater.UpdateAddOperation(operationCount: StepAmounts.Z);
                }
            }
        }

        public FunctionValue GetValue(int x, int y, int z)
        {
            return FunctionValues[x * StepAmounts.Y * StepAmounts.Z + y * StepAmounts.Z + z];
        }



        //1: add function values
        //2: compute vertices on edges
        //3: collect triangles
    }
}
