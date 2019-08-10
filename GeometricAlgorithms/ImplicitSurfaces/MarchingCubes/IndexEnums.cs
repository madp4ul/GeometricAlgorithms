using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingCubes
{
    public enum EdgeIndex : int
    {
        _000x = 0,
        _100z = 1,
        _001x = 2,
        _000z = 3,
        _010x = 4,
        _110z = 5,
        _011x = 6,
        _010z = 7,
        _000y = 8,
        _100y = 9,
        _101y = 10,
        _001y = 11,
    }

    public enum VertexIndex : int
    {
    }
}
