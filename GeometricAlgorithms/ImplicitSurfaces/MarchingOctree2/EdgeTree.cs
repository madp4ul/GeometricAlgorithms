using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree2
{
    class EdgeTree
    {
        private EdgeTreeNode Root;

        private readonly Surface Surface;

        public EdgeTree(IImplicitSurface implicitSurface)
        {
            Surface = new Surface(implicitSurface);
        }
    }
}
