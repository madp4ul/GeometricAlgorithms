using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree2
{
    class Edge
    {
        private readonly ImplicitSurfaceProvider ImplicitSurface;

        public readonly Dimension[] DirectionAxisFromCubeCenter;

        public readonly FunctionValue MinValue;
        public readonly FunctionValue MaxValue;

        public EdgeChildren Children { get; private set; }
        public bool HasChildren => Children != null;

        private SurfaceApproximation LastUsedSurfaceApproximation = null;
        private int[] LastComputedSurfaceApproximationIndices;

        public Edge(ImplicitSurfaceProvider implicitSurface, EdgeOrientation orientation, FunctionValue minValue, FunctionValue maxValue)
            : this(implicitSurface, orientation.GetAxis(), minValue, maxValue)
        { }

        public Edge(ImplicitSurfaceProvider implicitSurface, Dimension[] directionAxisFromCubeCenter, FunctionValue minValue, FunctionValue maxValue)
        {
            ImplicitSurface = implicitSurface;
            DirectionAxisFromCubeCenter = directionAxisFromCubeCenter;

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

            Children = new EdgeChildren(
                minEdge: new Edge(ImplicitSurface, DirectionAxisFromCubeCenter, MinValue, middleValue),
                maxEdge: new Edge(ImplicitSurface, DirectionAxisFromCubeCenter, middleValue, MaxValue));
        }

        public IReadOnlyCollection<int> GetInterpolatedPositionIndices(SurfaceApproximation surfaceApproximation)
        {
            int[] writeableIndices = GetWriteableInterpolatedPositionIndices(surfaceApproximation);
            return new ReadOnlyCollection<int>(writeableIndices);
        }

        private int[] GetWriteableInterpolatedPositionIndices(SurfaceApproximation surfaceApproximation)
        {
            if (surfaceApproximation == LastUsedSurfaceApproximation)
            {
                return LastComputedSurfaceApproximationIndices;
            }

            LastUsedSurfaceApproximation = surfaceApproximation;
            LastComputedSurfaceApproximationIndices = ComputeInterpolatedPositionIndices(surfaceApproximation);

            return LastComputedSurfaceApproximationIndices;
        }

        private int[] ComputeInterpolatedPositionIndices(SurfaceApproximation surfaceApproximation)
        {
            if (HasChildren)
            {
                return GetInterpolatedPositionIndicesFromChildren(surfaceApproximation);
            }

            if (MinValue.IsInside == MaxValue.IsInside)
            {
                return new int[0];
            }

            float surfaceZeroInterpolationFactor = Math.Abs(MinValue.Value / (MinValue.Value - MaxValue.Value));

            Vector3 interpolationPosition = Vector3.Interpolate(MinValue.Position, MaxValue.Position, surfaceZeroInterpolationFactor);

            return new int[1]
            {
                surfaceApproximation.AddPosition(interpolationPosition)
            };
        }

        private int[] GetInterpolatedPositionIndicesFromChildren(SurfaceApproximation surfaceApproximation)
        {
            var indicesFromMinChild = Children[0].GetWriteableInterpolatedPositionIndices(surfaceApproximation);
            var indicesFromMaxChild = Children[1].GetWriteableInterpolatedPositionIndices(surfaceApproximation);

            if (indicesFromMinChild.Length == 0)
            {
                return indicesFromMaxChild;
            }
            if (indicesFromMaxChild.Length == 0)
            {
                return indicesFromMinChild;
            }

            int[] combinedIndices = new int[indicesFromMinChild.Length + indicesFromMaxChild.Length];

            Array.Copy(indicesFromMinChild, 0, combinedIndices, 0, indicesFromMinChild.Length);
            Array.Copy(indicesFromMaxChild, 0, combinedIndices, indicesFromMinChild.Length, indicesFromMaxChild.Length);

            return combinedIndices;
        }
    }
}
