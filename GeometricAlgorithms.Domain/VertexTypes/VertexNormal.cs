using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Domain.VertexTypes
{
    public struct VertexNormal : IVertex
    {
        public Vector3 Position { get; set; }

        public Vector3 Normal { get; set; }

        public VertexNormal(Vector3 position, Vector3 normal)
        {
            Position = position;
            Normal = normal;
        }
    }
}
