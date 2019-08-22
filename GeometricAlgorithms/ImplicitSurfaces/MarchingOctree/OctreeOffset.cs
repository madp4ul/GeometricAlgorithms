using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree
{
    class OctreeOffset
    {
        public readonly int X;
        public readonly int Y;
        public readonly int Z;

        public OctreeOffset(int x, int y, int z)
        {
            if ((x != 0 && y != 1) || (y != 0 && y != 1) || (z != 0 && z != 1))
            {
                throw new ArgumentException();
            }

            X = x;
            Y = y;
            Z = z;
        }

        public OctreeOffset ToggleDimension(Dimension dimension)
        {
            return new OctreeOffset(
                dimension == Dimension.X ? ToggleValue(X) : X,
                dimension == Dimension.Y ? ToggleValue(Y) : Y,
                dimension == Dimension.Z ? ToggleValue(Z) : Z);
        }

        private int ToggleValue(int value)
        {
            return value == 0 ? 1 : 0;
        }

        public OctreeOffset SetValue(Dimension dimension, int value)
        {
            return new OctreeOffset(
                dimension == Dimension.X ? value : X,
                dimension == Dimension.Y ? value : Y,
                dimension == Dimension.Z ? value : Z);
        }

        public int GetValue(Dimension dimension)
        {
            switch (dimension)
            {
                case Dimension.X:
                    return X;
                case Dimension.Y:
                    return Y;
                case Dimension.Z:
                    return Z;
                case Dimension.Count:
                default:
                    throw new ArgumentException();
            }
        }

        public SideOffset ExcludeDimension(Dimension dimension)
        {
            if (dimension == Dimension.X)
            {
                return new SideOffset(Y, Z);
            }
            else if (dimension == Dimension.Y)
            {
                return new SideOffset(X, Z);
            }
            else if (dimension == Dimension.Z)
            {
                return new SideOffset(X, Y);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public int ExcludeDimensions(Dimension[] dimensions)
        {
            if (!dimensions.Contains(Dimension.X))
            {
                return X;
            }
            else if (!dimensions.Contains(Dimension.Y))
            {
                return Y;
            }
            else if (!dimensions.Contains(Dimension.Z))
            {
                return Z;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public IEnumerable<Dimension> GetDifferingDemensions(OctreeOffset other)
        {
            if (other.X != X)
            {
                yield return Dimension.X;
            }
            if (other.Y != Y)
            {
                yield return Dimension.Y;
            }
            if (other.Z != Z)
            {
                yield return Dimension.Z;
            }
        }

        public bool HasOnlyDifferencesOnDimensions(Dimension[] dimensions, OctreeOffset other)
        {
            return (other.X != X && !dimensions.Contains(Dimension.X))
                || (other.Y != Y && !dimensions.Contains(Dimension.Y))
                || (other.Z != Z && !dimensions.Contains(Dimension.Z));
        }

        public static OctreeOffset WithValueAtDimension(Dimension dimension, int valueAtDimension, int otherValue1, int othervalue2)
        {
            if (dimension == Dimension.X)
            {
                return new OctreeOffset(valueAtDimension, otherValue1, othervalue2);
            }
            else if (dimension == Dimension.Y)
            {
                return new OctreeOffset(otherValue1, valueAtDimension, othervalue2);
            }
            else if (dimension == Dimension.Z)
            {
                return new OctreeOffset(otherValue1, othervalue2, valueAtDimension);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public static OctreeOffset WithValuesAtDimension(
            Dimension dimension1, int valueAtDimension1,
            Dimension dimension2, int valueAtDimension2,
            int otherValue)
        {
            if (dimension1 == dimension2)
            {
                throw new ArgumentException("cant set same dimension to two different values");
            }

            return new OctreeOffset(
                dimension1 == Dimension.X ? valueAtDimension1 : (dimension2 == Dimension.X ? valueAtDimension2 : otherValue),
                dimension1 == Dimension.Y ? valueAtDimension1 : (dimension2 == Dimension.Y ? valueAtDimension2 : otherValue),
                dimension1 == Dimension.Z ? valueAtDimension1 : (dimension2 == Dimension.Z ? valueAtDimension2 : otherValue));
        }

        public override bool Equals(object obj)
        {
            return obj is OctreeOffset offset &&
                   X == offset.X &&
                   Y == offset.Y &&
                   Z == offset.Z;
        }

        public override int GetHashCode()
        {
            var hashCode = -307843816;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            hashCode = hashCode * -1521134295 + Z.GetHashCode();
            return hashCode;
        }
    }
}
