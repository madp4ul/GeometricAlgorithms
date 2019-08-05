using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.KdTree
{
    class VertexPosition
    {
        public readonly Vector3 Position;
        public readonly int OriginalIndex;

        public VertexPosition(Vector3 position, int originalIndex)
        {
            Position = position;
            OriginalIndex = originalIndex;
        }
    }
}
