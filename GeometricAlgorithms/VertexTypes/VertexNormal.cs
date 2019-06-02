using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.VertexTypes
{
    public class VertexNormal : Vertex
    {
        public Vector3 Normal { get; set; }

        public VertexNormal(Vector3 position, Vector3 normal)
            : base(position)
        {
            Normal = normal;
        }
    }
}
