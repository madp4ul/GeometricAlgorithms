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

        private readonly ImplicitSurfaceProvider ImplicitSurfaceProvider;
        private readonly SurfaceApproximation SurfaceApproximation;

        public EdgeTree(IImplicitSurface implicitSurface)
        {
            ImplicitSurfaceProvider = new ImplicitSurfaceProvider(implicitSurface);
            SurfaceApproximation = new SurfaceApproximation();
        }
    }
}
