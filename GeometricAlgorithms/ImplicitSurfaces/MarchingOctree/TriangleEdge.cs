using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree
{
    class TriangleEdge
    {
        public readonly Edge Edge1;
        public readonly Edge Edge2;
        public readonly Line2 TriangleLine;

        public TriangleEdge(Edge edge1, Edge edge2, Dimension sideAxis)
        {
            Edge1 = edge1;
            Edge2 = edge2;

            TriangleLine = Line2.FromPointToPoint(
                    edge1.VertexPosition.WithoutDimension(sideAxis),
                    edge2.VertexPosition.WithoutDimension(sideAxis));
        }

        public override string ToString()
        {
            return $"{{triangle edge: {Edge1.ToString()}-{Edge2.ToString()}}}";
        }
    }
}
