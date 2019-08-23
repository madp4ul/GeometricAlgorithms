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
        private const int XDirectionBitPosition = 0;
        private const int XPositiveBitPosition = 1 + XDirectionBitPosition;
        private const int YDirectionBitPosition = 2;
        private const int YPositiveBitPosition = 1 + YDirectionBitPosition;
        private const int ZDirectionBitPosition = 4;
        private const int ZPositiveBitPosition = 1 + ZDirectionBitPosition;

        public readonly EdgeIndex Index;
        public bool IsInXDirection => BitCalculator.IsOn((int)Index, XDirectionBitPosition);
        public bool IsXPositive => BitCalculator.IsOn((int)Index, XPositiveBitPosition);

        public bool IsInYDirection => BitCalculator.IsOn((int)Index, YDirectionBitPosition);
        public bool IsYPositive => BitCalculator.IsOn((int)Index, YPositiveBitPosition);

        public bool IsInZDirection => BitCalculator.IsOn((int)Index, ZDirectionBitPosition);
        public bool IsZPositive => BitCalculator.IsOn((int)Index, ZPositiveBitPosition);

        public EdgeOrientation(EdgeIndex index)
        {
            Index = index;
        }

        public EdgeOrientation(Dimension dim1, bool dim1Positive, Dimension dim2, bool dim2Positive)
        {
            int intex = 0;

            intex = BitCalculator.TurnOn(intex, SelectInDirectionBit(dim1));
            intex = BitCalculator.TurnOn(intex, SelectInDirectionBit(dim2));

            if (dim1Positive)
            {
                intex = BitCalculator.TurnOn(intex, SelectIsPositiveBit(dim1));
            }
            if (dim2Positive)
            {
                intex = BitCalculator.TurnOn(intex, SelectIsPositiveBit(dim2));
            }

            Index = (EdgeIndex)intex;
        }

        private int SelectInDirectionBit(Dimension dimension)
        {
            if (dimension == Dimension.X)
            {
                return XDirectionBitPosition;
            }
            else if (dimension == Dimension.X)
            {
                return YDirectionBitPosition;
            }
            else if (dimension == Dimension.X)
            {
                return ZDirectionBitPosition;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        private int SelectIsPositiveBit(Dimension dimension)
        {
            return SelectInDirectionBit(dimension) + 1;
        }

        public Dimension[] GetAxis()
        {
            if (!IsInXDirection)
            {
                return new[] { Dimension.Y, Dimension.Z };
            }
            else if (!IsInYDirection)
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
                bitPositionToToggle = XPositiveBitPosition;
            }
            else if (dimension == Dimension.Y)
            {
                bitPositionToToggle = YPositiveBitPosition;
            }
            else if (dimension == Dimension.Z)
            {
                bitPositionToToggle = ZPositiveBitPosition;
            }
            else
            {
                throw new ArgumentException("No valid dimension was given");
            }

            EdgeIndex mirrored = (EdgeIndex)BitCalculator.ToggleBit((int)Index, bitPositionToToggle);

            return new EdgeOrientation(mirrored);
        }

        public bool IsPositive(Dimension dimension)
        {
            return (dimension == Dimension.X && IsXPositive)
                || (dimension == Dimension.Y && IsYPositive)
                || (dimension == Dimension.Z && IsZPositive);
        }

        public int GetArrayIndex()
        {
            return GetArrayIndex(Index);
        }

        public static int GetArrayIndex(EdgeIndex index)
        {
            switch (index)
            {
                case EdgeIndex._000x:
                    return 0;
                case EdgeIndex._100z:
                    return 1;
                case EdgeIndex._001x:
                    return 2;
                case EdgeIndex._000z:
                    return 3;
                case EdgeIndex._010x:
                    return 4;
                case EdgeIndex._110z:
                    return 5;
                case EdgeIndex._011x:
                    return 6;
                case EdgeIndex._010z:
                    return 7;
                case EdgeIndex._000y:
                    return 8;
                case EdgeIndex._100y:
                    return 9;
                case EdgeIndex._101y:
                    return 10;
                case EdgeIndex._001y:
                    return 11;
                default:
                    throw new ArgumentException();
            }
        }
    }

    struct EdgeDirection
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
