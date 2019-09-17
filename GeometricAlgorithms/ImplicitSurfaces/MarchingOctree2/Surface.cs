using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree2
{
    class Surface
    {
        private readonly IImplicitSurface ImplicitSurface;

        private readonly List<Vector3> Positions = new List<Vector3>();
        private readonly List<Triangle> Faces = new List<Triangle>();

        private readonly List<FunctionValue> InnerValues = new List<FunctionValue>();
        private readonly List<FunctionValue> OuterValues = new List<FunctionValue>();

        public int FunctionValueCount => InnerValues.Count + OuterValues.Count;

        public Surface(IImplicitSurface implicitSurface)
        {
            ImplicitSurface = implicitSurface ?? throw new ArgumentNullException(nameof(implicitSurface));
        }
    }
}
