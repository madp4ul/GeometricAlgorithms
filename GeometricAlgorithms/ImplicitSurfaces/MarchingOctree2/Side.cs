using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree2
{
    class Side
    {
        public readonly Dimension Dimension;

        public Side(SideOrientation orientation)
        {
            Dimension = orientation.GetAxis();
        }
    }
}
