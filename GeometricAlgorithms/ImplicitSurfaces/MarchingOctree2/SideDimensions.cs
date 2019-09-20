using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree2
{
    class SideDimensions
    {
        public readonly Dimension DirectionAxisFromCubeCenter;
        public readonly IReadOnlyList<Dimension> SideAxis;

        private SideDimensions(Dimension directionAxisFromCubeCenter)
        {
            DirectionAxisFromCubeCenter = directionAxisFromCubeCenter;
            SideAxis = Dimensions.All.Where(d => d != directionAxisFromCubeCenter).ToArray();
        }

        private static SideDimensions[] AllSideDimensions;

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
    }
}
