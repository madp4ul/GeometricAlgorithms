using GeometricAlgorithms.Domain;
using GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.Approximation;
using GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.FunctionValues;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.Edges
{
    class Edge
    {
        private readonly ImplicitSurfaceProvider ImplicitSurface;
        private readonly RefiningApproximation Approximation;

        public readonly Dimension[] DirectionAxisFromCubeCenter;

        public readonly FunctionValue MinValue;
        public readonly FunctionValue MaxValue;

        public EdgeChildren Children { get; private set; }
        public bool HasChildren => Children != null;

        private EdgeIntersection EdgeIntersection;

        /// <summary>
        /// Nodes that are using this edge
        /// </summary>
        public readonly EdgeNodes UsingNodes;


        public Edge(
            Edge parent,
            ImplicitSurfaceProvider implicitSurface,
            Dimension[] directionAxisFromCubeCenter,
            FunctionValue minValue,
            FunctionValue maxValue)
            : this(parent.Approximation, implicitSurface, directionAxisFromCubeCenter, minValue, maxValue)
        {
            UsingNodes = new EdgeNodes(parent.UsingNodes);
        }

        public Edge(
            RefiningApproximation approximation,
            ImplicitSurfaceProvider implicitSurface,
            Dimension[] directionAxisFromCubeCenter,
            FunctionValue minValue, FunctionValue maxValue)
        {
            Approximation = approximation;
            ImplicitSurface = implicitSurface;
            DirectionAxisFromCubeCenter = directionAxisFromCubeCenter;

            MinValue = minValue;
            MaxValue = maxValue;

            ComputeEdgeIntersection();

            UsingNodes = new EdgeNodes(null);
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
                minEdge: new Edge(parent: this, ImplicitSurface, DirectionAxisFromCubeCenter, MinValue, middleValue),
                maxEdge: new Edge(parent: this, ImplicitSurface, DirectionAxisFromCubeCenter, middleValue, MaxValue));

            //Intersections now have to be gathered from children
            if (EdgeIntersection != null)
            {
                EdgeIntersection.Dispose();
                EdgeIntersection = null;
            }
        }

        /// <summary>
        /// Get cached position idices of surface intersections 
        /// </summary>
        /// <param name="surfaceApproximation"></param>
        /// <returns></returns>
        public EdgeSurfaceIntersections GetSurfaceIntersections()
        {
            EdgeIntersection[] intersectionArray = ComputeSurfaceIntersections();

            var intersections = new EdgeSurfaceIntersections(MinValue.IsInside, intersectionArray);

            return intersections;
        }

        private EdgeIntersection[] ComputeSurfaceIntersections()
        {
            if (HasChildren)
            {
                return GetSurfaceIntersectionPositionIndicesFromChildren();
            }
            else if (EdgeIntersection != null)
            {
                return new[] { EdgeIntersection };
            }
            else
            {
                return new EdgeIntersection[0];
            }
        }

        /// <summary>
        /// use this in constructor only
        /// </summary>
        private void ComputeEdgeIntersection()
        {
            if (MinValue.IsInside != MaxValue.IsInside)
            {
                float surfaceZeroInterpolationFactor = Math.Abs(MinValue.Value / (MinValue.Value - MaxValue.Value));

                Vector3 interpolationPosition = Vector3.Interpolate(MinValue.Position, MaxValue.Position, surfaceZeroInterpolationFactor);

                EdgeIntersection = Approximation.AddIntersection(interpolationPosition);
            }
        }

        private EdgeIntersection[] GetSurfaceIntersectionPositionIndicesFromChildren()
        {
            var indicesFromMinChild = Children[0].ComputeSurfaceIntersections();
            var indicesFromMaxChild = Children[1].ComputeSurfaceIntersections();

            if (indicesFromMinChild.Length == 0)
            {
                return indicesFromMaxChild;
            }
            if (indicesFromMaxChild.Length == 0)
            {
                return indicesFromMinChild;
            }

            EdgeIntersection[] combinedIndices = new EdgeIntersection[indicesFromMinChild.Length + indicesFromMaxChild.Length];

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
