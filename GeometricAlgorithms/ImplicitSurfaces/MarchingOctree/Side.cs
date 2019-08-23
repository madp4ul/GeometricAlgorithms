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
        public bool IsComplete => Edges.Any(e => e == null);

        private readonly Side[,] Children;

        private readonly Edge[] Edges;


        /// <summary>
        /// Construct side from edges. Side has a main dimension which has to be
        /// part of the 2 dimensions of each edge. 
        /// </summary>
        /// <param name="smallerDimMin">Edge where the second dimension is the smaller of the two left dimensions and it is negative</param>
        /// <param name="smallerDimMax">Edge where the second dimension is the smaller of the two left dimensions and it is positive</param>
        /// <param name="biggerDimMin">Edge where the second dimension is the bigger of the two left dimensions and it is negative</param>
        /// <param name="biggerDimMax">Edge where the second dimension is the bigger of the two left dimensions and it is positive</param>
        public Side(Dimension dimension, Edge smallerDimMin, Edge smallerDimMax, Edge biggerDimMin, Edge biggerDimMax)
        {
            Axis = dimension;
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
                //TODO add value approximated child
            }

            return Children[smallerDimIndex, biggerDimIndex];
        }


    }
}
