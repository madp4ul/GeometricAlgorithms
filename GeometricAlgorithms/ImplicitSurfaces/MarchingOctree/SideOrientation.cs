using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree
{
    class SideOrientation
    {
        private const int MinMaxBit = 0b0001;
        private const int MinMaxBitInverted = 0b1110;
        private const int XBit = 0b1000;
        private const int YBit = 0b010;
        private const int ZBit = 0b0010;

        public readonly bool IsMax;
        public readonly bool IsX;
        public readonly bool IsY;
        public readonly bool IsZ;
        public readonly SideIndex Index;

        public SideOrientation(SideIndex sideIndex)
        {
            Index = sideIndex;
            IsMax = ((int)sideIndex & MinMaxBit) > 0;

            IsZ = ((int)sideIndex & ZBit) > 0;
            IsY = ((int)sideIndex & YBit) > 0;
            IsX = ((int)sideIndex & XBit) > 0;
        }

        public SideOrientation GetMirrored()
        {
            SideIndex mirrored = Index;

            //if is max, turn max off, else turn max on
            mirrored = (SideIndex)(((int)mirrored & MinMaxBit) > 0 ? ((int)mirrored & MinMaxBitInverted) : ((int)mirrored | MinMaxBit));

            return new SideOrientation(mirrored);
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
