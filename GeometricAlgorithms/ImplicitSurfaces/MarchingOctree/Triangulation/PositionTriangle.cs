using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.Triangulation
{
    class PositionTriangle : Triangle
    {
        public readonly Vector3 Position1;
        public readonly Vector3 Position2;
        public readonly Vector3 Position3;

        public PositionTriangle(PositionIndex index1, PositionIndex index2, PositionIndex index3)
            : base(index1.Index, index2.Index, index3.Index)
        {
            Position1 = index1.Position;
            Position2 = index2.Position;
            Position3 = index3.Position;
        }

        public float GetDistance(Vector3 position)
        {
            throw new NotImplementedException();
        }
    }
}
