using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree
{
    class Edge
    {
        private readonly ImplicitSurfaceProvider ImplicitSurface;

        public readonly Dimension[] DirectionAxisFromCubeCenter;

        public readonly FunctionValue MinValue;
        public readonly FunctionValue MaxValue;

        public EdgeChildren Children { get; private set; }
        public bool HasChildren => Children != null;

        private EdgeIntersectionCache IntersectionCache = new EdgeIntersectionCache();

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

        /// <summary>
        /// Get cached position idices of surface intersections 
        /// </summary>
        /// <param name="surfaceApproximation"></param>
        /// <returns></returns>
        public EdgeSurfaceIntersections GetSurfaceIntersectionPositionIndices(SurfaceApproximation surfaceApproximation)
        {
            if (IntersectionCache.TryGetIntersections(surfaceApproximation, out EdgeSurfaceIntersections intersections))
            {
                return intersections;
            }

            PositionIndex[] writeableIndices = GetWriteableSurfaceIntersectionPositionIndices(surfaceApproximation);

            intersections = new EdgeSurfaceIntersections(MinValue.IsInside, writeableIndices);
            IntersectionCache.SetIntersections(surfaceApproximation, intersections);

            return intersections;
        }

        private PositionIndex[] GetWriteableSurfaceIntersectionPositionIndices(SurfaceApproximation surfaceApproximation)
        {
            if (IntersectionCache.TryGetIntersectionIndices(surfaceApproximation, out PositionIndex[] intersectionIndices))
            {
                return intersectionIndices;
            }

            intersectionIndices = ComputeSurfaceIntersectionPositionIndices(surfaceApproximation);
            IntersectionCache.SetIntersectionIndices(surfaceApproximation, intersectionIndices);

            return intersectionIndices;
        }

        private PositionIndex[] ComputeSurfaceIntersectionPositionIndices(SurfaceApproximation surfaceApproximation)
        {
            if (HasChildren)
            {
                return GetSurfaceIntersectionPositionIndicesFromChildren(surfaceApproximation);
            }

            if (MinValue.IsInside == MaxValue.IsInside)
            {
                return new PositionIndex[0];
            }

            float surfaceZeroInterpolationFactor = Math.Abs(MinValue.Value / (MinValue.Value - MaxValue.Value));

            Vector3 interpolationPosition = Vector3.Interpolate(MinValue.Position, MaxValue.Position, surfaceZeroInterpolationFactor);

            return new PositionIndex[1]
            {
                surfaceApproximation.AddPosition(interpolationPosition)
            };
        }

        private PositionIndex[] GetSurfaceIntersectionPositionIndicesFromChildren(SurfaceApproximation surfaceApproximation)
        {
            var indicesFromMinChild = Children[0].GetWriteableSurfaceIntersectionPositionIndices(surfaceApproximation);
            var indicesFromMaxChild = Children[1].GetWriteableSurfaceIntersectionPositionIndices(surfaceApproximation);

            if (indicesFromMinChild.Length == 0)
            {
                return indicesFromMaxChild;
            }
            if (indicesFromMaxChild.Length == 0)
            {
                return indicesFromMinChild;
            }

            PositionIndex[] combinedIndices = new PositionIndex[indicesFromMinChild.Length + indicesFromMaxChild.Length];

            Array.Copy(indicesFromMinChild, 0, combinedIndices, 0, indicesFromMinChild.Length);
            Array.Copy(indicesFromMaxChild, 0, combinedIndices, indicesFromMinChild.Length, indicesFromMaxChild.Length);

            return combinedIndices;
        }

        public override string ToString()
        {
            string axis = $"({DirectionAxisFromCubeCenter[0].ToString()}|{DirectionAxisFromCubeCenter[1].ToString()})";

            return $"{{edge: {axis}, {MinValue.ToString()} - {MaxValue.ToString()}}}";
        }
    }
}
