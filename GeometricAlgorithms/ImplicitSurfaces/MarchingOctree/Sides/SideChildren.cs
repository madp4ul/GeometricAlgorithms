﻿using GeometricAlgorithms.Domain;
using GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.Edges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.Sides
{
    class SideChildren
    {
        private readonly Side[,] Sides = new Side[2, 2];

        /// <summary>
        /// Create from children. child01 is the child that is to the negative side of the middle position along the smaller dimension
        /// and to the positive side along the bigger dimension.
        /// </summary>
        /// <param name="child00"></param>
        /// <param name="child01"></param>
        /// <param name="child10"></param>
        /// <param name="child11"></param>
        public SideChildren(Side child00, Side child01, Side child10, Side child11)
        {
            this[0, 0] = child00 ?? throw new ArgumentNullException(nameof(child00));
            this[0, 1] = child01 ?? throw new ArgumentNullException(nameof(child01));
            this[1, 0] = child10 ?? throw new ArgumentNullException(nameof(child10));
            this[1, 1] = child11 ?? throw new ArgumentNullException(nameof(child11));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="minDimensionIndex">0 is min dimension, 1 is max dimension</param>
        /// <param name="maxDimensionIndex">0 is negative direction, 1 is positive direction</param>
        /// <returns></returns>
        public Side this[int minDimensionIndex, int maxDimensionIndex]
        {
            get { return Sides[minDimensionIndex, maxDimensionIndex]; }
            private set { Sides[minDimensionIndex, maxDimensionIndex] = value; }
        }

        public Edge GetCrossEdge(int dimensionIndex, int directionIndex)
        {
            if (dimensionIndex == 0)
            {
                return this[directionIndex, 0].Edges[1, 1];
            }
            else
            {
                return this[0, directionIndex].Edges[0, 1];
            }
        }

        public Edge GetCrossEdge(Dimension crossDimension, int directionIndex)
        {
            int dimensionIndex = Sides[0, 0].Dimensions.GetDimensionIndex(crossDimension);

            return GetCrossEdge(dimensionIndex, directionIndex);
        }
    }
}
