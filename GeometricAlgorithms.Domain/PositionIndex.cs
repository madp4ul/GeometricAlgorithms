using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Domain
{
    public struct PositionIndex
    {
        /// <summary>
        /// Position of vertex
        /// </summary>
        public readonly Vector3 Position;

        /// <summary>
        /// Index of related data in mesh this belongs to
        /// </summary>
        public readonly int Index;

        public PositionIndex(Vector3 position, int originalIndex)
        {
            Position = position;
            Index = originalIndex;
        }

        public override string ToString()
        {
            return $"(position index: {Position.ToString()} at {Index})";
        }
    }
}
