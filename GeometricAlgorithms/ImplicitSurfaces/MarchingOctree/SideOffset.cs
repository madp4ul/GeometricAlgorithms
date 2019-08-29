using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree
{
    class SideOffset
    {
        public readonly int A;
        public readonly int B;

        public SideOffset(int a, int b)
        {
            if ((a != 0 && a != 1) || (b != 0 && b != 1))
            {
                throw new ArgumentException();
            }

            A = a;
            B = b;
        }

        public override string ToString()
        {
            return $"{{offset2: {A} | {B} }}";
        }
    }
}
