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

        public static Line2Intersection? Intersect(Line2 line1, Line2 line2)
        {
            // line1 = P + lambda1 * r
            // line2 = Q + lambda2 * s
            // r and s must be normalized (length = 1)
            // returns intersection point O of line1 with line2 = [ Ox, Oy ] 
            // returns null if lines do not intersect or are identical
            Vector2 normalizedDiection1 = line1.Direction.Normalized();
            Vector2 normalizedDiection2 = line2.Direction.Normalized();

            var PQx = line2.Position.X - line1.Position.X;
            var PQy = line2.Position.Y - line2.Position.Y;
            var rx = normalizedDiection1.X;
            var ry = normalizedDiection1.Y;
            var rxt = -ry;
            var ryt = rx;
            var qx = PQx * rx + PQy * ry;
            var qy = PQx * rxt + PQy * ryt;
            var sx = normalizedDiection2.X * rx + normalizedDiection2.Y * ry;
            var sy = normalizedDiection2.X * rxt + normalizedDiection2.Y * ryt;
            // if lines are identical or do not cross...
            if (sy == 0) return null;
            var a = (qx - qy * sx / sy);
            var intersectionPoint = new Vector2(line1.Position.X + a * rx, line1.Position.Y + a * ry);

            return new Line2Intersection(a, intersectionPoint);
        }

        public Line2Intersection2? Intersect2(Line2 otherLine)
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

            return new Line2Intersection2(a, intersectionPosition);
        }
    }

    public struct Line2Intersection2
    {
        public readonly float DirectionFactor;
        public readonly Vector2 IntersectionPosition;

        public Line2Intersection2(float directionFactor, Vector2 intersectionPosition)
        {
            DirectionFactor = directionFactor;
            IntersectionPosition = intersectionPosition;
        }
    }

    public struct Line2Intersection
    {
        public readonly float DistanceFromLine1Position;
        public readonly Vector2 IntersectionPosition;

        public Line2Intersection(float distanceFromLine1Position, Vector2 intersectionPosition)
        {
            DistanceFromLine1Position = distanceFromLine1Position;
            IntersectionPosition = intersectionPosition;
        }
    }
}
