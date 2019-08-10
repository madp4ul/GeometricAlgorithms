using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces
{
    public interface IImplicitSurface
    {
        /// <summary>
        /// Get distance to surface, negative distance means position is on inside
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        float GetApproximateSurfaceDistance(Vector3 position);
    }
}
