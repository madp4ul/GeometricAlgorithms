using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.FunctionValues
{
    public enum FunctionValueIndex : int
    {
        //(x-positive|y-posivite-z-positive)
        _000 = 0b000,
        _001 = 0b001,
        _010 = 0b010,
        _011 = 0b011,
        _100 = 0b100,
        _101 = 0b101,
        _110 = 0b110,
        _111 = 0b111,
    }
}
