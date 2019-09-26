using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree.PointPartitioning
{
    public class PositionIndex
    {
        /// <summary>
        /// Position of vertex
        /// </summary>
        public readonly Vector3 Position;

        /// <summary>
        /// Index of related data in mesh this belongs to
        /// </summary>
        public readonly int OriginalIndex;

        public PositionIndex(Vector3 position, int originalIndex)
        {
            Position = position;
            OriginalIndex = originalIndex;
        }
    }
}
