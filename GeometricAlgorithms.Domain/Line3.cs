using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Domain
{
    public struct Line3
    {
        public readonly Vector3 Position;
        public readonly Vector3 Direction;

        public Line3(Vector3 position, Vector3 direction)
        {
            Position = position;
            Direction = direction;
        }

        public override bool Equals(object obj)
        {
            return obj is Line3 line &&
                   Position.Equals(line.Position) &&
                   Direction.Equals(line.Direction);
        }

        public override int GetHashCode()
        {
            var hashCode = 1218595012;
            hashCode = hashCode * -1521134295 + EqualityComparer<Vector3>.Default.GetHashCode(Position);
            hashCode = hashCode * -1521134295 + EqualityComparer<Vector3>.Default.GetHashCode(Direction);
            return hashCode;
        }
    }
}
