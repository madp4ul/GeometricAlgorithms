using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.Approximation
{
    class RefiningApproximation
    {
        private readonly LinkedList<EditableIndexVertex> EdgeIntersections = new LinkedList<EditableIndexVertex>();
        private readonly LinkedList<EditableIndexTriangle> Triangles = new LinkedList<EditableIndexTriangle>();

        public EdgeIntersection AddIntersection(Vector3 position)
        {
            var vertex = new EditableIndexVertex(position);

            var node = EdgeIntersections.AddLast(vertex);

            return new EdgeIntersection(node);
        }

        public NodeTriangulation AddTriangulation(IList<EditableIndexTriangle> triangles)
        {
            if (triangles.Count == 0)
            {
                throw new ArgumentException();
            }

            var firstNode = Triangles.AddLast(triangles[0]);

            var lastNode = firstNode;
            foreach (var triangle in triangles.Skip(1))
            {
                lastNode = Triangles.AddLast(triangle);
            }

            return new NodeTriangulation(firstNode, lastNode);
        }

        public Mesh GetApproximation()
        {
            int index = 0;
            foreach (var vertex in EdgeIntersections)
            {
                vertex.Index = index;

                index++;
            }

            var vertices = EdgeIntersections.Select(ei => ei.Position).ToArray();
            var faces = Triangles.Select(t => t.GetTriangle()).ToArray();

            return new Mesh(vertices, faces);
        }
    }
}
