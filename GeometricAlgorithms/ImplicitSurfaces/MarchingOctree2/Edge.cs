using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree2
{
    class Edge
    {
        private readonly ImplicitSurfaceProvider ImplicitSurface;

        public readonly Dimension[] Dimensions;

        public readonly FunctionValue MinValue;
        public readonly FunctionValue MaxValue;

        public Edge[] Children { get; private set; }
        public bool HasChildren => Children != null;

        private SurfaceApproximation LastUsedSurfaceApproximation = null;
        private int? LastComputedSurfaceApproximationIndex;

        public Edge(ImplicitSurfaceProvider implicitSurface, EdgeOrientation orientation, FunctionValue minValue, FunctionValue maxValue)
            : this(implicitSurface, orientation.GetAxis(), minValue, maxValue)
        { }

        private Edge(ImplicitSurfaceProvider implicitSurface, Dimension[] dimensions, FunctionValue minValue, FunctionValue maxValue)
        {
            ImplicitSurface = implicitSurface;
            Dimensions = dimensions;

            MinValue = minValue;
            MaxValue = maxValue;
        }

        public void CreateChildren()
        {
            if (HasChildren)
            {
                throw new InvalidOperationException("Cant compute children twice.");
            }

            Vector3 middlePosition = Vector3.Interpolate(MinValue.Position, MaxValue.Position, 0.5f);
            FunctionValue middleValue = ImplicitSurface.CreateFunctionValue(middlePosition);

            Children = new Edge[2]
            {
                new Edge(ImplicitSurface, Dimensions, MinValue, middleValue),
                new Edge(ImplicitSurface, Dimensions, middleValue, MaxValue)
            };
        }

        public int? GetInterpolatedPositionIndex(SurfaceApproximation surfaceApproximation)
        {
            if (surfaceApproximation == LastUsedSurfaceApproximation)
            {
                return LastComputedSurfaceApproximationIndex;
            }

            LastUsedSurfaceApproximation = surfaceApproximation;
            LastComputedSurfaceApproximationIndex = ComputeInterpolatedPositionIndex(surfaceApproximation);

            return LastComputedSurfaceApproximationIndex;
        }

        private int? ComputeInterpolatedPositionIndex(SurfaceApproximation surfaceApproximation)
        {
            if (HasChildren)
            {
                throw new InvalidOperationException("Compute interpolation of children instead");
            }

            if (MinValue.IsInside == MaxValue.IsInside)
            {
                return null;
            }

            float surfaceZeroInterpolationFactor = Math.Abs(MinValue.Value / (MinValue.Value - MaxValue.Value));

            Vector3 interpolationPosition = Vector3.Interpolate(MinValue.Position, MaxValue.Position, surfaceZeroInterpolationFactor);

            return surfaceApproximation.AddPosition(interpolationPosition);
        }
    }
}
