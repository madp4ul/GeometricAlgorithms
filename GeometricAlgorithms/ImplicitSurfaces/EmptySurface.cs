using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometricAlgorithms.Domain;

namespace GeometricAlgorithms.ImplicitSurfaces
{
    public class EmptySurface : IImplicitSurface
    {
        public float GetApproximateSurfaceDistance(Vector3 position)
        {
            return float.MaxValue;
        }
    }
}
