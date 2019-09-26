using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree
{
    class CubeOutsideChildren
    {
        private readonly CubeOutsides[,,] Children;

        public CubeOutsideChildren(CubeOutsides[,,] children)
        {
            Children = children ?? throw new ArgumentNullException(nameof(children));
        }

        public CubeOutsides this[int x, int y, int z]
        {
            get => Children[x, y, z];
        }
    }
}
