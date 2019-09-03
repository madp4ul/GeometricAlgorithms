using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces
{
    public interface IFiniteImplicitSurface : IImplicitSurface
    {
        BoundingBox DefinedArea { get; }
    }
}
