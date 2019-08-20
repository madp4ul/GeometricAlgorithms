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

        public OctreeOffset SetDimension(Dimension dimension, int value)
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
    }
}
