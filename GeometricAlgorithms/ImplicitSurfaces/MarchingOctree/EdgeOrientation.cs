using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree
{
    class EdgeOrientation
    {
        private const int XDirBitPosition = 1;
        private const int XNegBitPosition = 2;
        private const int YDirBitPosition = 3;
        private const int YNegBitPosition = 4;
        private const int ZDirBitPosition = 5;
        private const int ZNegBitPosition = 6;

        public readonly EdgeIndex Index;
        public readonly EdgeDirection DirectionX;
        public readonly EdgeDirection DirectionY;
        public readonly EdgeDirection DirectionZ;

        public EdgeOrientation(EdgeIndex index)
        {
            Index = index;
            int intex = (int)index;

            DirectionX = new EdgeDirection(BitCalculator.IsOn(intex, XDirBitPosition), BitCalculator.IsOn(intex, XNegBitPosition));
            DirectionY = new EdgeDirection(BitCalculator.IsOn(intex, YDirBitPosition), BitCalculator.IsOn(intex, YNegBitPosition));
            DirectionZ = new EdgeDirection(BitCalculator.IsOn(intex, ZDirBitPosition), BitCalculator.IsOn(intex, ZNegBitPosition));
        }

        public Dimension[] GetDirections()
        {
            if (!DirectionX.IsInDirection)
            {
                return new[] { Dimension.Y, Dimension.Z };
            }
            else if (!DirectionY.IsInDirection)
            {
                return new[] { Dimension.X, Dimension.Z };
            }
            else
            {
                return new[] { Dimension.X, Dimension.Y };
            }
        }

        public EdgeOrientation GetMirrored(Dimension dimension)
        {

            int bitPositionToToggle;
            if (dimension == Dimension.X)
            {
                bitPositionToToggle = XNegBitPosition;
            }
            else if (dimension == Dimension.Y)
            {
                bitPositionToToggle = YNegBitPosition;
            }
            else if (dimension == Dimension.Z)
            {
                bitPositionToToggle = ZNegBitPosition;
            }
            else
            {
                throw new ArgumentException("No valid dimension was given");
            }

            EdgeIndex mirrored = (EdgeIndex)BitCalculator.ToggleBit((int)Index, bitPositionToToggle);

            return new EdgeOrientation(mirrored);
        }
    }

    class EdgeDirection
    {
        public readonly bool IsInDirection;
        public readonly bool Negative;

        public EdgeDirection(bool isInDirection, bool negative)
        {
            if (!isInDirection && negative)
            {
                throw new ArgumentException("Can not be negative without being moved in direction");
            }

            IsInDirection = isInDirection;
            Negative = negative;
        }
    }
}
