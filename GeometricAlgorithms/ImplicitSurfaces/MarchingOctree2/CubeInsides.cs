﻿using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree2
{
    /// <summary>
    /// Creates and provides all the inner sides of a cube (Those between two children of the cube)
    /// So the inner sides of the current cube will be oute sides of its child cubes
    /// </summary>
    class CubeInsides
    {
        private readonly Side[] InnerCubeSides = new Side[12];

        /// <summary>
        /// Create insides for a cube from available data in outside container
        /// </summary>
        /// <param name="outsideContainer">outer sides of the cube</param>
        public CubeInsides(ImplicitSurfaceProvider implicitSurface, CubeOutsides outsideContainer)
        {
            //Create all insides
            foreach (var side in outsideContainer.Where(s => !s.HasChildren))
            {
                side.CreateChildren();
            }

            var insideEdges = new CubeInsideEdges(implicitSurface, outsideContainer);

            for (int i = 0; i < (int)Dimension.Count; i++)
            {
                Dimension current = (Dimension)i;
                var edgeDirectionsFromCubeCenter = Dimensions.All.Where(d => d != current).ToArray();

                var sideEdges00 = new SideOutsideEdges(current);
                sideEdges00[0, 0] = outsideContainer[new SideOrientation(edgeDirectionsFromCubeCenter[0], false)]
                    .Children[0, 0]
                    .Edges[edgeDirectionsFromCubeCenter[0], true];

                //iterate over all side indices
                for (int a = 0; a < 2; a++)
                {
                    for (int b = 0; b < 2; b++)
                    {
                        var sideEdges = new SideOutsideEdges(current);

                        for (int edgeDimension = 0; edgeDimension < 2; edgeDimension++)
                        {
                            for (int edgeDirection = 0; edgeDirection < 2; edgeDirection++)
                            {
                                bool isEdgeInside = (edgeDimension == 0 && a != edgeDimension)
                                    || (edgeDimension == 1 && b != edgeDimension);

                                Edge selectedEdge;

                                if (isEdgeInside)
                                {
                                    Dimension dimIndexOfInsideEdge = edgeDirectionsFromCubeCenter[edgeDimension == 0 ? 1 : 0];
                                    bool directionIndexOfInsideEdge = (edgeDimension == 0 ? a : b) == 0;

                                    selectedEdge = insideEdges[dimIndexOfInsideEdge, directionIndexOfInsideEdge];
                                }
                                else
                                {
                                    SideOrientation outsideOrientation = new SideOrientation(
                                        dimension: edgeDirectionsFromCubeCenter[edgeDimension],
                                        isMax: edgeDirection == 1);
                                    Side outsideToLookIn = outsideContainer[outsideOrientation];

                                    //TODO improve

                                    var dimensionInOutside = outsideToLookIn.Dimensions.SideAxis
                                         .Select((dimension, index) => new { dimension, index })
                                         .First(d => sideEdges.Dimensions.SideAxis.Contains(d.dimension));

                                    var dimensionInCreatedSide = sideEdges.Dimensions.SideAxis
                                        .Select((dimension, index) => new { dimension, index })
                                        .First(d => sideEdges.Dimensions.SideAxis.Contains(d.dimension));

                                    int direction = dimensionInCreatedSide.index == 0 ? a : b;

                                    selectedEdge = GetCrossEdge(outsideToLookIn, dimensionInOutside.index, direction);
                                }

                                sideEdges[edgeDimension, edgeDirection] = selectedEdge;
                            }
                        }
                    }
                }

            }

            //Todo create sides from available data and create all necessary edges and fv.

        }

        private Edge GetCrossEdge(Side side, int dimensionIndex, int directionIndex)
        {
            if (dimensionIndex == 0)
            {
                return side.Children[directionIndex, 0].Edges[1, 1];
            }
            else
            {
                return side.Children[0, directionIndex].Edges[0, 1];
            }
        }

        public Side this[SideOrientation orientation, OctreeOffset childOffset]
        {
            get => InnerCubeSides[ToIndex(orientation, childOffset)];
            set => InnerCubeSides[ToIndex(orientation, childOffset)] = value;
        }

        private int ToIndex(SideOrientation orientation, OctreeOffset childOffset)
        {
            Dimension orientationAxis = orientation.GetAxis();

            //If (is max and offset is 1) or (is min and offset is not 1)
            if (orientation.IsMax == (childOffset.GetValue(orientationAxis) == 1))
            {
                throw new InvalidOperationException("The given orientation from the offset is not on the inside");
            }

            SideOffset childSideOffset = childOffset.ExcludeDimension(orientationAxis);

            return ToIndex(orientationAxis, childSideOffset);
        }

        private int ToIndex(Dimension orientationAxis, SideOffset childSideOffset)
        {
            int index = (int)orientationAxis * 4;

            index += childSideOffset.MinimumDimensionValue * 2;
            index += childSideOffset.MaximumDimensionValue;

            return index;
        }
    }
}
