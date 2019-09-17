using GeometricAlgorithms.Domain;
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
        public CubeInsides(CubeOutsides outsideContainer)
        {
            //Todo create sides from available data and create all necessary edges and fv.
            //Create all insides
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

            int index = (int)orientationAxis * 4;

            SideOffset childSideOffset = childOffset.ExcludeDimension(orientationAxis);

            index += childSideOffset.MinimumDimensionValue * 2;
            index += childSideOffset.MaximumDimensionValue;

            return index;
        }
    }
}
