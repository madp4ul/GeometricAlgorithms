using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Domain
{
    public struct Line2
    {
        public readonly Vector2 Position;
        public readonly Vector2 Direction;

        public Line2(Vector2 position, Vector2 direction)
        {
            Position = position;
            Direction = direction;
        }

        public static Line2 FromPointToPoint(Vector2 position1, Vector2 position2)
        {
            return new Line2(position1, position2 - position1);
        }

        public Line2Intersection? Intersect(Line2 otherLine)
        {
            // if the lines intersect, the result contains the x and y of the intersection 
            //(treating the lines as infinite) and booleans for whether line segment 1 or line segment 2 contain the point
            float denominator = (otherLine.Direction.Y * Direction.X) - (otherLine.Direction.X * Direction.Y);
            if (denominator == 0)
            {
                return null;
            }

            float a = Position.Y - otherLine.Position.Y;
            float b = Position.X - otherLine.Position.X;

            float numerator1 = (otherLine.Direction.X * a) - (otherLine.Direction.Y * b);
            a = numerator1 / denominator;

            // if we cast these lines infinitely in both directions, they intersect here:
            var intersectionPosition = new Vector2(Position.X + (a * Direction.X), Position.Y + (a * Direction.Y));

            return new Line2Intersection(a, intersectionPosition);
        }
    }

    public struct Line2Intersection
    {
        public readonly float DirectionFactor;
        public readonly Vector2 IntersectionPosition;

        public Line2Intersection(float directionFactor, Vector2 intersectionPosition)
        {
            DirectionFactor = directionFactor;
            IntersectionPosition = intersectionPosition;
        }
    }
}
