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
        public bool IsComplete => Edges.Any(e => e == null || !e.IsComplete);

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
            Edge biggerDimMax = child10 != null && child11 != null ? new Edge(child10.Edges[3], child11.Edges[3]) : null;

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
            if (!IsComplete)
            {
                throw new InvalidOperationException("Side cant be used because it is not complete");
            }

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
                        smallerDimMax: GetChildEdge(SideEdgeIndex.BiggerDimMin),
                        biggerDimMin: Edges[2].GetChild(0),
                        biggerDimMax: GetChildEdge(SideEdgeIndex.SmallerDimMin),
                        result: Result);
                }
                else if (smallerDimIndex == 1 && biggerDimIndex == 0)
                {
                    side = new Side(Axis,
                        smallerDimMin: GetChildEdge(SideEdgeIndex.BiggerDimMin),
                        smallerDimMax: Edges[1].GetChild(0),
                        biggerDimMin: Edges[2].GetChild(1),
                        biggerDimMax: GetChildEdge(SideEdgeIndex.SmallerDimMax),
                        result: Result);

                }
                else if (smallerDimIndex == 0 && biggerDimIndex == 1)
                {
                    side = new Side(Axis,
                        smallerDimMin: Edges[0].GetChild(1),
                        smallerDimMax: GetChildEdge(SideEdgeIndex.BiggerDimMax),
                        biggerDimMin: GetChildEdge(SideEdgeIndex.SmallerDimMin),
                        biggerDimMax: Edges[3].GetChild(0),
                        result: Result);
                }
                else if (smallerDimIndex == 1 && biggerDimIndex == 1)
                {
                    side = new Side(Axis,
                        smallerDimMin: GetChildEdge(SideEdgeIndex.BiggerDimMax),
                        smallerDimMax: Edges[1].GetChild(1),
                        biggerDimMin: GetChildEdge(SideEdgeIndex.SmallerDimMax),
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

        private FunctionValue MiddleValueApproximation => new FunctionValue(Middle, Edges.Average(e => e.Minimum.Value));
        private readonly Edge[] MiddleEdges = new Edge[4];
        private Edge GetChildEdge(SideEdgeIndex index)
        {
            if (MiddleEdges[(int)index] == null)
            {
                Edge edge = null;

                if (index == SideEdgeIndex.SmallerDimMin)
                {
                    edge = ApproximateEdge(Edges[(int)SideEdgeIndex.SmallerDimMin].MiddleValueApproximation, MiddleValueApproximation);
                }
                else if (index == SideEdgeIndex.SmallerDimMax)
                {
                    edge = ApproximateEdge(MiddleValueApproximation, Edges[(int)SideEdgeIndex.SmallerDimMax].MiddleValueApproximation);
                }
                else if (index == SideEdgeIndex.BiggerDimMin)
                {
                    edge = ApproximateEdge(Edges[(int)SideEdgeIndex.BiggerDimMin].MiddleValueApproximation, MiddleValueApproximation);
                }
                else if (index == SideEdgeIndex.BiggerDimMax)
                {
                    edge = ApproximateEdge(MiddleValueApproximation, Edges[(int)SideEdgeIndex.BiggerDimMax].MiddleValueApproximation);
                }

                MiddleEdges[(int)index] = edge;
            }

            return MiddleEdges[(int)index];
        }

        private Edge ApproximateEdge(FunctionValue edgeStart, FunctionValue edgeEnd)
        {
            float? interpolationValue = TriangleEdgeIntersection(edgeStart.Position, edgeEnd.Position);

            if (interpolationValue.HasValue && 0 <= interpolationValue.Value && interpolationValue.Value <= 1)
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
                Line2 triangleLine = Line2.FromPointToPoint(
                    triangleEdge.Edge1.VertexPosition.WithoutDimension(Axis),
                    triangleEdge.Edge2.VertexPosition.WithoutDimension(Axis));

                var intersection = Line2.Intersect(edgeLine, triangleLine);
                if (intersection.HasValue)
                {
                    float newInterpolationValue = intersection.Value.DistanceFromLine1Position * edgeLine.Direction.Length;
                    interpolationValue = !interpolationValue.HasValue
                        ? newInterpolationValue
                        : default;
                }
            }

            return interpolationValue;
        }

        public override string ToString()
        {
            string complete = IsComplete ? "is complete" : "is not complete";

            return $"{{side: {Axis.ToString()}, {complete}}}";
        }

        private enum SideEdgeIndex : int
        {
            SmallerDimMin = 0,
            SmallerDimMax = 1,
            BiggerDimMin = 2,
            BiggerDimMax = 3
        }
    }
}
