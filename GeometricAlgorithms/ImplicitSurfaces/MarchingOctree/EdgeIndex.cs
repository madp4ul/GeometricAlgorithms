using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree
{
    public enum EdgeIndex : int
    {
        //Edge name is min point of edge+axis of moves on from there
        //value: (x-direction|x-positive|y-direction|y-positive|z-direction|z-positive)
        _000x = 0b001010,//0,
        _100z = 0b111000,//1,
        _001x = 0b001011,//2,
        _000z = 0b101000,//3,
        _010x = 0b001110,//4,
        _110z = 0b111100,//5,
        _011x = 0b001111,//6,
        _010z = 0b101100,//7,
        _000y = 0b100010,//8,
        _100y = 0b110010,//9,
        _101y = 0b110011,//10,
        _001y = 0b100011,//11,
    }
}
