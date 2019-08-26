using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree
{
    struct OrientedEdge
    {
        public readonly Edge Edge;
        public readonly EdgeOrientation Orientation;

        public OrientedEdge(Edge edge, EdgeOrientation orientation)
        {
            Edge = edge;
            Orientation = orientation;
        }

        public bool HasEdge => Edge != null;

        public override string ToString()
        {
            return $"({Orientation.ToString()}, {Edge?.ToString()})";
        }
    }
}
