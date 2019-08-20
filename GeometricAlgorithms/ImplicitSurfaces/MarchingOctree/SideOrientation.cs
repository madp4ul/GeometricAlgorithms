using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree
{
    class SideOrientation
    {
        private const int XBit = 0;
        private const int YBit = 1;
        private const int ZBit = 2;
        private const int MaxBit = 3;

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

        private int SelectBit(Dimension dimension)
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
    }
}
