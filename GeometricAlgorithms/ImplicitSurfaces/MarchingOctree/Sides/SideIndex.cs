using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.Sides
{
    public enum SideIndex : int
    {
        //first 3 bit determite axis (x,y,z), last determines min/max
        minX = 0b1000,
        maxX = 0b1001,
        minY = 0b0100,
        maxY = 0b0101,
        minZ = 0b0010,
        maxZ = 0b0011,
    }
}
