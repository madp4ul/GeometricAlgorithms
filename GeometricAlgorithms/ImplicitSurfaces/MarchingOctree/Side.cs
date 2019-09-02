using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree
{
    class Side
    {
        public readonly Dimension Axis;

        /// <summary>
        /// Side is complete if all edges are there. If the side is incomplete
        /// its data cant be used but it still may contain complete children.
        /// </summary>
        public bool IsComplete => !Edges.Any(e => e == null || !e.IsComplete);

        private readonly Side[,] Children;

        private readonly Edge[] Edges;
        public Vector3 Middle => Edges[0].Minimum.Position
            + (Edges[0].Maximum.Position - Edges[0].Minimum.Position) / 2
            + (Edges[3].Maximum.Position - Edges[3].Minimum.Position) / 2;


        private readonly List<TriangleEdge> TriangleEdges = new List<TriangleEdge>();
        private readonly SurfaceResult Result;

        /// <summary>
        /// Construct side from edges. Side has a main dimension which has to be
        /// part of the 2 dimensions of each edge. 
        /// </summary>
        /// <param name="smallerDimMin">Edge where the second dimension is the smaller of the two left dimensions and it is negative</param>
        /// <param name="smallerDimMax">Edge where the second dimension is the smaller of the two left dimensions and it is positive</param>
        /// <param name="biggerDimMin">Edge where the second dimension is the bigger of the two left dimensions and it is negative</param>
        /// <param name="biggerDimMax">Edge where the second dimension is the bigger of the two left dimensions and it is positive</param>
        public Side(Dimension dimension, Edge smallerDimMin, Edge smallerDimMax, Edge biggerDimMin, Edge biggerDimMax, SurfaceResult result)
        {
            Axis = dimension;
            Result = result;
            Edges = new Edge[4]
            {
                smallerDimMin,
                smallerDimMax,
                biggerDimMin,
                biggerDimMax,
            };
            Children = new Side[2, 2];
        }

        /// <summary>
        /// Create side from children. Children could be null. If all given children exist,
        /// the parent side information can be calculated and the parent is complete
        /// </summary>
        /// <param name="dimension"></param>
        /// <param name="child00"></param>
        /// <param name="child01"></param>
        /// <param name="child10"></param>
        /// <param name="child11"></param>
        /// <param name="result"></param>
        public Side(Dimension dimension, Side child00, Side child01, Side child10, Side child11)
        {
            if ((child00 != null && child00.Axis != dimension)
                || (child01 != null && child01.Axis != dimension)
                || (child10 != null && child10.Axis != dimension)
                || (child11 != null && child11.Axis != dimension))
            {
                throw new ArgumentException("Child sides dont belong to same side");
            }

            Axis = dimension;

            Children = new Side[2, 2]
            {
                {child00, child01 },
                {child10, child11 },
            };

            //Try to create as much information as possible from known children
            Edge smallerDimMin = child00 != null && child01 != null ? new Edge(child00.Edges[0], child01.Edges[0]) : null;
            Edge smallerDimMax = child10 != null && child11 != null ? new Edge(child10.Edges[1], child11.Edges[1]) : null;
            Edge biggerDimMin = child00 != null && child10 != null ? new Edge(child00.Edges[2], child10.Edges[2]) : null;
            Edge biggerDimMax = child01 != null && child11 != null ? new Edge(child01.Edges[3], child11.Edges[3]) : null;

            //select the right side children for each outer edge and put to child edges together
            Edges = new Edge[4]
            {
                smallerDimMin,
                smallerDimMax,
                biggerDimMin,
                biggerDimMax,
            };
        }

        public void AddTriangleEdge(TriangleEdge triangleEdge)
        {
            TriangleEdges.Add(triangleEdge);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sideMax">Orientation of this side</param>
        /// <param name="edgeOrientation"></param>
        /// <returns></returns>
        public Edge GetEdge(EdgeOrientation edgeOrientation)
        {
            var directions = edgeOrientation.GetAxis();

            if (directions[0] != Axis && directions[1] != Axis)
            {
                throw new ArgumentException("Edge is not part of the side");
            }

            //The axis of the edge that it does not share with the side
            Dimension otherAxis = directions[0] == Axis ? directions[1] : directions[0];

            //if the second dimension of the edge is the smaller dimension of the two left
            bool isSmallerDim =
                (Axis == Dimension.X && otherAxis == Dimension.Y)
                || (Axis == Dimension.Y && otherAxis == Dimension.X)
                || (Axis == Dimension.Z && otherAxis == Dimension.X);

            bool isMax = edgeOrientation.IsPositive(otherAxis);

            int index = (isSmallerDim ? 0 : 2) + (isMax ? 1 : 0);

            return Edges[index];
        }

        public IEnumerable<OrientedEdge> GetEdges(SideOrientation orientation)
        {
            for (int axisIndex = 0; axisIndex < 2; axisIndex++)
            {
                for (int minmax = 0; minmax < 2; minmax++)
                {
                    EdgeOrientation edgeOrientation = orientation.GetEdgeOrientation(axisIndex, minmax);
                    Edge edge = GetEdge(edgeOrientation);

                    yield return new OrientedEdge(edge, edgeOrientation);
                }
            }
        }

        public Side GetChildSide(SideOffset dimensionsWithoutSideAxis)
        {
            return GetChildSide(dimensionsWithoutSideAxis.A, dimensionsWithoutSideAxis.B);
        }

        public Side GetChildSide(int smallerDimIndex, int biggerDimIndex)
        {
            //if no child there we try to approximate its data from parent.
            //only possible if parent data is complete
            if (Children[smallerDimIndex, biggerDimIndex] == null && IsComplete)
            {
                //Child will only be approximated if the side was created from 4 edges
                Children[smallerDimIndex, biggerDimIndex] = ApproximateChildSide(smallerDimIndex, biggerDimIndex);
            }

            return Children[smallerDimIndex, biggerDimIndex];
        }

        private Side ApproximateChildSide(int smallerDimIndex, int biggerDimIndex)
        {
            if (Children[smallerDimIndex, biggerDimIndex] == null)
            {
                Side side = null;

                if (smallerDimIndex == 0 && biggerDimIndex == 0)
                {
                    side = new Side(Axis,
                        smallerDimMin: Edges[0].GetChild(0),
                        smallerDimMax: GetApproximatedEdgeOfChild(SideEdgeIndex.BiggerDimMin),
                        biggerDimMin: Edges[2].GetChild(0),
                        biggerDimMax: GetApproximatedEdgeOfChild(SideEdgeIndex.SmallerDimMin),
                        result: Result);
                }
                else if (smallerDimIndex == 1 && biggerDimIndex == 0)
                {
                    side = new Side(Axis,
                        smallerDimMin: GetApproximatedEdgeOfChild(SideEdgeIndex.BiggerDimMin),
                        smallerDimMax: Edges[1].GetChild(0),
                        biggerDimMin: Edges[2].GetChild(1),
                        biggerDimMax: GetApproximatedEdgeOfChild(SideEdgeIndex.SmallerDimMax),
                        result: Result);

                }
                else if (smallerDimIndex == 0 && biggerDimIndex == 1)
                {
                    side = new Side(Axis,
                        smallerDimMin: Edges[0].GetChild(1),
                        smallerDimMax: GetApproximatedEdgeOfChild(SideEdgeIndex.BiggerDimMax),
                        biggerDimMin: GetApproximatedEdgeOfChild(SideEdgeIndex.SmallerDimMin),
                        biggerDimMax: Edges[3].GetChild(0),
                        result: Result);
                }
                else if (smallerDimIndex == 1 && biggerDimIndex == 1)
                {
                    side = new Side(Axis,
                        smallerDimMin: GetApproximatedEdgeOfChild(SideEdgeIndex.BiggerDimMax),
                        smallerDimMax: Edges[1].GetChild(1),
                        biggerDimMin: GetApproximatedEdgeOfChild(SideEdgeIndex.SmallerDimMax),
                        biggerDimMax: Edges[3].GetChild(1),
                        result: Result);
                }

                //although the triangle edge wont belong to edges that are part of the child sides
                //they will be able to compute intersections with their child edges
                side.TriangleEdges.AddRange(this.TriangleEdges);

                Children[smallerDimIndex, biggerDimIndex] = side;
            }

            return Children[smallerDimIndex, biggerDimIndex];


        }

        private Lazy<FunctionValue> MiddleValueApproximation => new Lazy<FunctionValue>(GetAverageMiddleFunctionValue);
        private readonly Edge[] MiddleEdges = new Edge[4];
        private Edge GetApproximatedEdgeOfChild(SideEdgeIndex index)
        {
            if (MiddleEdges[(int)index] == null)
            {
                Edge edge = null;

                if (index == SideEdgeIndex.SmallerDimMin)
                {
                    edge = ApproximateEdge(Edges[(int)SideEdgeIndex.SmallerDimMin].MiddleValueApproximation, MiddleValueApproximation.Value);
                }
                else if (index == SideEdgeIndex.SmallerDimMax)
                {
                    edge = ApproximateEdge(MiddleValueApproximation.Value, Edges[(int)SideEdgeIndex.SmallerDimMax].MiddleValueApproximation);
                }
                else if (index == SideEdgeIndex.BiggerDimMin)
                {
                    edge = ApproximateEdge(Edges[(int)SideEdgeIndex.BiggerDimMin].MiddleValueApproximation, MiddleValueApproximation.Value);
                }
                else if (index == SideEdgeIndex.BiggerDimMax)
                {
                    edge = ApproximateEdge(MiddleValueApproximation.Value, Edges[(int)SideEdgeIndex.BiggerDimMax].MiddleValueApproximation);
                }

                MiddleEdges[(int)index] = edge;
            }

            return MiddleEdges[(int)index];
        }

        private Edge ApproximateEdge(FunctionValue edgeStart, FunctionValue edgeEnd)
        {
            float? interpolationValue = TriangleEdgeIntersection(edgeStart.Position, edgeEnd.Position);

            if (interpolationValue.HasValue)
            {
                var lazy = new Lazy<int>(() => Result.AddPosition(edgeStart.Position + (edgeEnd.Position - edgeStart.Position) * interpolationValue.Value));
                return new Edge(edgeStart, edgeEnd, interpolationValue, lazy);
            }
            else
            {
                return new Edge(edgeStart, edgeEnd, null, null);
            }
        }

        private float? TriangleEdgeIntersection(Vector3 edgeStart, Vector3 edgeEnd)
        {
            Line2 edgeLine = Line2.FromPointToPoint(edgeStart.WithoutDimension(Axis), edgeEnd.WithoutDimension(Axis));

            float? interpolationValue = null;
            foreach (var triangleEdge in TriangleEdges)
            {
                var intersection = edgeLine.Intersect(triangleEdge.TriangleLine);
                if (intersection.HasValue && intersection.Value.DirectionFactor >= 0 && intersection.Value.DirectionFactor <= 1)
                {
                    interpolationValue = !interpolationValue.HasValue
                        ? intersection.Value.DirectionFactor
                        : default;
                }
            }

            return interpolationValue;
        }

        private FunctionValue GetAverageMiddleFunctionValue()
        {
            if (!IsComplete)
            {
                throw new InvalidOperationException();
            }

            var fvMinMin = Edges[(int)SideEdgeIndex.SmallerDimMin].Minimum;
            var fvMaxMax = Edges[(int)SideEdgeIndex.SmallerDimMax].Maximum;

            float? diagValue1 = InterpolateMiddleWithDiagonal(fvMinMin, fvMaxMax);

            var fvMinMax = Edges[(int)SideEdgeIndex.SmallerDimMin].Maximum;
            var fvMaxMin = Edges[(int)SideEdgeIndex.SmallerDimMax].Minimum;

            float? diagValue2 = InterpolateMiddleWithDiagonal(fvMinMax, fvMaxMin);

            float middleValue;

            if (diagValue1.HasValue && diagValue2.HasValue)
            {
                middleValue = (diagValue1.Value + diagValue2.Value) / 2f;
            }
            else if (diagValue1.HasValue)
            {
                middleValue = diagValue1.Value;
            }
            else if (diagValue2.HasValue)
            {
                middleValue = diagValue2.Value;
            }
            else
            {
                throw new Exception("Should not happen");
            }

            return new FunctionValue(Middle, middleValue);
        }

        private float? InterpolateMiddleWithDiagonal(FunctionValue start, FunctionValue end)
        {
            var minValue = new InterpolationValue(0, start.Value);
            var maxValue = new InterpolationValue(1, end.Value);

            Line2 diagonal = Line2.FromPointToPoint(
                start.Position.WithoutDimension(Axis),
                end.Position.WithoutDimension(Axis));

            foreach (var triangleEdge in TriangleEdges)
            {
                var intersection = diagonal.Intersect(triangleEdge.TriangleLine);

                if (intersection.HasValue)
                {
                    if (0 < intersection.Value.DirectionFactor && intersection.Value.DirectionFactor < 0.5f)
                    {
                        minValue = new InterpolationValue(intersection.Value.DirectionFactor, 0);
                    }
                    else if (0.5f < intersection.Value.DirectionFactor && intersection.Value.DirectionFactor < 1)
                    {
                        maxValue = new InterpolationValue(intersection.Value.DirectionFactor, 0);
                    }
                }
            }

            if (minValue.Value == 0 && maxValue.Value == 0)
            {
                return null;
            }

            const float interpolationAtMiddle = 0.5f;

            float range = maxValue.Interpolation - minValue.Interpolation;
            float valueRange = maxValue.Value - minValue.Value;

            float middleInterpolationFactor = (interpolationAtMiddle - minValue.Interpolation) / range;

            float middleValue = minValue.Value + middleInterpolationFactor * valueRange;

            return middleValue;
        }

        public FunctionValue GetFunctionValue(SideFunctionValueIndex index)
        {
            if (index == SideFunctionValueIndex.SmallerDimMinBiggerDimMin)
            {
                return Edges[(int)SideEdgeIndex.SmallerDimMin]?.Minimum
                    ?? Edges[(int)SideEdgeIndex.BiggerDimMin]?.Minimum
                    ?? Children[0, 0]?.GetFunctionValue(index);
            }
            else if (index == SideFunctionValueIndex.SmallerDimMinBiggerDimMax)
            {
                return Edges[(int)SideEdgeIndex.SmallerDimMin]?.Maximum
                    ?? Edges[(int)SideEdgeIndex.BiggerDimMax]?.Minimum
                    ?? Children[0, 1]?.GetFunctionValue(index);
            }
            else if (index == SideFunctionValueIndex.SmallerDimMaxBiggerDimMin)
            {
                return Edges[(int)SideEdgeIndex.SmallerDimMax]?.Minimum
                    ?? Edges[(int)SideEdgeIndex.BiggerDimMin]?.Maximum
                    ?? Children[1, 0]?.GetFunctionValue(index);
            }
            else if (index == SideFunctionValueIndex.SmallerDimMaxBiggerDimMax)
            {
                return Edges[(int)SideEdgeIndex.SmallerDimMax]?.Maximum
                    ?? Edges[(int)SideEdgeIndex.BiggerDimMax]?.Maximum
                    ?? Children[1, 1]?.GetFunctionValue(index);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public override string ToString()
        {
            string complete = IsComplete ? "is complete" : "is not complete";

            return $"{{side: {Axis.ToString()}, {complete}}}";
        }

        private struct InterpolationValue
        {
            public readonly float Interpolation;
            public readonly float Value;

            public InterpolationValue(float interpolation, float value)
            {
                Interpolation = interpolation;
                Value = value;
            }
        }


        private enum SideEdgeIndex : int
        {
            SmallerDimMin = 0,
            SmallerDimMax = 1,
            BiggerDimMin = 2,
            BiggerDimMax = 3
        }

        public enum SideFunctionValueIndex : int
        {
            SmallerDimMinBiggerDimMin = 0,
            SmallerDimMaxBiggerDimMin = 1,
            SmallerDimMinBiggerDimMax = 2,
            SmallerDimMaxBiggerDimMax = 3,
        }
    }
}
