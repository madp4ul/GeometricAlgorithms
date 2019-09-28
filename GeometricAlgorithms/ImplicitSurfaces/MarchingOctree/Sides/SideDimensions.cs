using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.Sides
{
    class SideDimensions
    {
        public readonly Dimension DirectionAxisFromCubeCenter;
        public readonly IReadOnlyList<Dimension> SideAxis;

        /// <summary>
        /// If when looking at the side in a way that sideaxis[0] goes to the right,
        /// the sidesaxis[1] goes up.
        /// </summary>
        public readonly bool SideOnlookedFromMin;

        private readonly int[] DimensionIndices;



        private SideDimensions(Dimension directionAxisFromCubeCenter)
        {
            DirectionAxisFromCubeCenter = directionAxisFromCubeCenter;
            SideAxis = Dimensions.All.Where(d => d != directionAxisFromCubeCenter).ToArray();

            DimensionIndices = ComputeDimensionIndices();
            SideOnlookedFromMin = GetSideOnlookedFromMin();
        }

        private bool GetSideOnlookedFromMin()
        {
            switch (DirectionAxisFromCubeCenter)
            {
                case Dimension.X:
                    return true;
                case Dimension.Y:
                    return false;
                case Dimension.Z:
                    return true;
                default:
                    throw new NotImplementedException();
            }
        }

        private int[] ComputeDimensionIndices()
        {
            var dimensionIndices = new int[(int)Dimension.Count];

            for (int i = 0; i < (int)Dimension.Count; i++)
            {
                int index = i;

                if (i > (int)DirectionAxisFromCubeCenter)
                {
                    index--;
                }
                else if (i == (int)DirectionAxisFromCubeCenter)
                {
                    index = -1;
                }

                dimensionIndices[i] = index;
            }

            return dimensionIndices;
        }

        public int GetDimensionIndex(Dimension dimension)
        {
            int index = DimensionIndices[(int)dimension];

            if (index == -1)
            {
                throw new ArgumentException("The dimension does not have an index on this side.");
            }

            return index;
        }

        private static readonly SideDimensions[] AllSideDimensions;

        static SideDimensions()
        {
            AllSideDimensions = new SideDimensions[(int)Dimension.Count];

            for (int i = 0; i < (int)Dimension.Count; i++)
            {
                AllSideDimensions[i] = new SideDimensions((Dimension)i);
            }
        }

        public static SideDimensions GetForDirectionAxis(Dimension directionAxisFromCubeCenter)
        {
            return AllSideDimensions[(int)directionAxisFromCubeCenter];
        }

        public static Dimension GetSharedAxis(SideDimensions dim1, SideDimensions dim2)
        {
            for (int i = 0; i < dim1.SideAxis.Count; i++)
            {
                for (int j = 0; j < dim2.SideAxis.Count; j++)
                {
                    if (dim1.SideAxis[i] == dim2.SideAxis[j])
                    {
                        return dim1.SideAxis[i];
                    }
                }
            }

            throw new NotImplementedException("This should not happen.");
        }
    }
}
