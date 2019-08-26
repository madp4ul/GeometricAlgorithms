using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree
{
    struct SideOrientation
    {
        private const int XBit = 3;
        private const int YBit = 2;
        private const int ZBit = 1;
        private const int MaxBit = 0;

        public readonly SideIndex Index;

        public bool IsX => BitCalculator.IsOn((int)Index, XBit);
        public bool IsY => BitCalculator.IsOn((int)Index, YBit);
        public bool IsZ => BitCalculator.IsOn((int)Index, ZBit);
        public bool IsMax => BitCalculator.IsOn((int)Index, MaxBit);

        public SideOrientation(SideIndex sideIndex)
        {
            Index = sideIndex;
        }

        public SideOrientation(Dimension dimension, bool isMax)
        {
            int intex = 0;

            intex = BitCalculator.TurnOn(intex, SelectBit(dimension));

            if (isMax)
            {
                intex = BitCalculator.TurnOn(intex, MaxBit);
            }

            Index = (SideIndex)intex;
        }

        private static int SelectBit(Dimension dimension)
        {
            if (dimension == Dimension.X)
            {
                return XBit;
            }
            else if (dimension == Dimension.X)
            {
                return YBit;
            }
            else if (dimension == Dimension.X)
            {
                return ZBit;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public SideOrientation GetMirrored()
        {
            SideIndex mirrored = (SideIndex)BitCalculator.ToggleBit((int)Index, MaxBit);

            return new SideOrientation(mirrored);
        }

        public Dimension GetDirection()
        {
            if (IsX)
            {
                return Dimension.X;
            }
            else if (IsY)
            {
                return Dimension.Y;
            }
            else if (IsZ)
            {
                return Dimension.Z;
            }
            else
            {
                throw new Exception();
            }
        }

        public int GetArrayIndex()
        {
            return GetArrayIndex(Index);
        }

        public static int GetArrayIndex(SideIndex index)
        {
            switch (index)
            {
                case SideIndex.minX:
                    return 0;
                case SideIndex.maxX:
                    return 1;
                case SideIndex.minY:
                    return 2;
                case SideIndex.maxY:
                    return 3;
                case SideIndex.minZ:
                    return 4;
                case SideIndex.maxZ:
                    return 5;
                default:
                    throw new ArgumentException();
            }
        }

        public EdgeOrientation GetEdgeOrientation(int axisIndex, int minmax)
        {
            Dimension sideAxis = GetDirection();
            Dimension[] rotationAxis = Dimensions.All.Where(d => d != sideAxis).ToArray();

            return new EdgeOrientation(sideAxis, IsMax, rotationAxis[axisIndex], minmax == 1);
        }

        public override string ToString()
        {
            var axis = GetDirection();

            string dimString(Dimension d, SideOrientation o) => $"{d.ToString()}, " + (o.IsMax ? "is positive" : "is negative");

            return $"{{edge orientation: {dimString(axis, this)} | {dimString(axis, this)}}}";
        }

        public static SideIndex GetSideIndex(int arrayIndex)
        {
            switch (arrayIndex)
            {
                case 0:
                    return SideIndex.minX;
                case 1:
                    return SideIndex.maxX;
                case 2:
                    return SideIndex.minY;
                case 3:
                    return SideIndex.maxY;
                case 4:
                    return SideIndex.minZ;
                case 5:
                    return SideIndex.maxZ;
                default:
                    throw new ArgumentException();
            }
        }

        public static bool TryGetContainingOrientation(EdgeOrientation edge1, EdgeOrientation edge2, out SideOrientation sideOrientation)
        {
            var axis1 = edge1.GetAxis().Select(d => new Axis { Dimension = d, Positive = edge1.IsPositive(d) });
            var axis2 = edge2.GetAxis().Select(d => new Axis { Dimension = d, Positive = edge1.IsPositive(d) });

            var sideAxis = axis1.Union(axis2, new AxisComparer()).FirstOrDefault();

            if (sideAxis != null)
            {
                sideOrientation = new SideOrientation(sideAxis.Dimension, sideAxis.Positive);
                return true;
            }
            sideOrientation = new SideOrientation();
            return false;
        }

        private class Axis
        {
            public Dimension Dimension;
            public bool Positive;
        }

        private class AxisComparer : IEqualityComparer<Axis>
        {
            public bool Equals(Axis x, Axis y)
            {
                return x.Dimension == y.Dimension && x.Positive == y.Positive;
            }

            public int GetHashCode(Axis obj)
            {
                return (int)obj.Dimension;
            }
        }

    }
}
