using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.Sides
{
    struct OrientedSide
    {
        public readonly SideOrientation Orientation;
        public readonly Side Side;

        public OrientedSide(SideOrientation orientation, Side side)
        {
            Orientation = orientation;
            Side = side ?? throw new ArgumentNullException(nameof(side));
        }

        public override string ToString()
        {
            return $"(oriented side: {Orientation.ToString()}, {Side.ToString()})";
        }
    }
}
