using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Domain
{
    public struct VertexNormal : IVertex
    {
        public Vector3 Position { get; set; }

        public Vector3? OriginalNormal { get; set; }
        public Vector3? ApproximatedNormal { get; set; }

        public VertexNormal(Vector3 position, Vector3? originalNormal = null)
        {
            Position = position;
            OriginalNormal = originalNormal;
            ApproximatedNormal = null;
        }
    }
}
