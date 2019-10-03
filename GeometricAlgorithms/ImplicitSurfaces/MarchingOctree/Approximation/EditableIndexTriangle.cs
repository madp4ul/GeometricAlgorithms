using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.Approximation
{
    class EditableIndexTriangle
    {
        public readonly EdgeIntersection Position0;
        public readonly EdgeIntersection Position1;
        public readonly EdgeIntersection Position2;

        public EditableIndexTriangle(EdgeIntersection position0, EdgeIntersection position1, EdgeIntersection position2)
        {
            Position0 = position0 ?? throw new ArgumentNullException(nameof(position0));
            Position1 = position1 ?? throw new ArgumentNullException(nameof(position1));
            Position2 = position2 ?? throw new ArgumentNullException(nameof(position2));
        }



        public Triangle GetTriangle()
        {
            return new Triangle(Position0.VertexIndex.Index, Position1.VertexIndex.Index, Position2.VertexIndex.Index);
        }
    }
}
