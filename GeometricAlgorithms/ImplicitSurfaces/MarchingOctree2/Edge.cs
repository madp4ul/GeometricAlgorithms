using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree2
{
    class Edge
    {
        public readonly Dimension[] Dimensions;

        public Edge(EdgeOrientation orientation)
        {
            Dimensions = orientation.GetAxis();
        }
    }
}
