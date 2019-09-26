using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree
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

                //iterate over all side indices
                for (int a = 0; a < 2; a++)
                {
                    for (int b = 0; b < 2; b++)
                    {
                        var sideEdges = new SideOutsideEdges(current);

                        for (int edgeDimension = 0; edgeDimension < 2; edgeDimension++)
                        {
                            int indexAtDimensionOfEdge = edgeDimension == 0 ? b : a;

                            for (int edgeDirection = 0; edgeDirection < 2; edgeDirection++)
                            {
                                bool isEdgeInside = (edgeDimension == 0 && a != edgeDirection)
                                    || (edgeDimension == 1 && b != edgeDirection);

                                Edge selectedEdge;

                                if (isEdgeInside)
                                {
                                    int nonEdgeDimension = edgeDimension == 0 ? 1 : 0;

                                    Dimension dimIndexOfInsideEdge = sideEdges.Dimensions.SideAxis[nonEdgeDimension];
                                    selectedEdge = insideEdges[dimIndexOfInsideEdge, indexAtDimensionOfEdge == 1];
                                }
                                else
                                {
                                    SideOrientation outsideOrientation = new SideOrientation(
                                        dimension: sideEdges.Dimensions.SideAxis[edgeDimension],
                                        isMax: edgeDirection == 1);
                                    Side outsideToLookIn = outsideContainer[outsideOrientation];

                                    Dimension sharedDimension = SideDimensions.GetSharedAxis(outsideToLookIn.Dimensions, sideEdges.Dimensions);
                                    int dimensionIndexInCreatedSide = sideEdges.Dimensions.GetDimensionIndex(sharedDimension);

                                    selectedEdge = outsideToLookIn.Children.GetCrossEdge(sharedDimension, indexAtDimensionOfEdge);
                                }

                                sideEdges[edgeDimension, edgeDirection] = selectedEdge;
                            }
                        }

                        InnerCubeSides[ToIndex(current, a, b)] = new Side(implicitSurface, sideEdges);
                    }
                }

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
            return ToIndex(orientationAxis, childSideOffset.MinimumDimensionValue, childSideOffset.MaximumDimensionValue);
        }

        private int ToIndex(Dimension orientationAxis, int minimumDimensionValue, int maximumDimensionValue)
        {
            int index = (int)orientationAxis * 4;

            index += minimumDimensionValue * 2;
            index += maximumDimensionValue;

            return index;
        }
    }
}
