using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Domain.VertexTypes
{
    public class Vertex
    {
        public Vector3 Position { get; set; }

        public Vertex(Vector3 position)
        {
            Position = position;
        }

        public override bool Equals(object obj)
        {
            return obj is Vertex vertex &&
                   Position.Equals(vertex.Position);
        }

        public override int GetHashCode()
        {
            return -425606 + EqualityComparer<Vector3>.Default.GetHashCode(Position);
        }
    }
}
