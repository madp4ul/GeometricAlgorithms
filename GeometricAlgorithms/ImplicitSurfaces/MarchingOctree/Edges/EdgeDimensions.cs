using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.Edges
{
    class EdgeDimensions
    {
        public readonly IReadOnlyList<Dimension> DirectionAxisFromCubeCenter;
        public readonly Dimension SideAxis;

        private EdgeDimensions(params Dimension[] directionAxisFromCubeCenter)
        {
            if (directionAxisFromCubeCenter.Length != 2)
            {
                throw new ArgumentException();
            }

            DirectionAxisFromCubeCenter = directionAxisFromCubeCenter;
            SideAxis = Dimensions.All.Where(d => !directionAxisFromCubeCenter.Contains(d)).Single();
        }

        private static readonly EdgeDimensions[] AllEdgeDimensions;

        /// <summary>
        /// /////////////////////////////TODO
        /// </summary>

        static EdgeDimensions()
        {
            AllEdgeDimensions = new EdgeDimensions[(int)Dimension.Count];

            for (int i = 0; i < (int)Dimension.Count; i++)
            {
                AllEdgeDimensions[i] = new EdgeDimensions((Dimension)i);
            }
        }

        public static EdgeDimensions GetForDirectionAxis(Dimension directionAxisFromCubeCenter)
        {
            return AllEdgeDimensions[(int)directionAxisFromCubeCenter];
        }
    }
}
