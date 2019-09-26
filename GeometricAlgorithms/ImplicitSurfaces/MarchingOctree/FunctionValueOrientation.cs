using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree2
{
    struct FunctionValueOrientation
    {
        private const int XMaxPosition = 2;
        private const int YMaxPosition = 1;
        private const int ZMaxPosition = 0;

        public readonly FunctionValueIndex Index;
        public bool IsXMaximum => BitCalculator.IsOn((int)Index, XMaxPosition);
        public bool IsYMaximum => BitCalculator.IsOn((int)Index, YMaxPosition);
        public bool IsZMaximum => BitCalculator.IsOn((int)Index, ZMaxPosition);

        public FunctionValueOrientation(FunctionValueIndex index)
        {
            Index = index;
        }

        public FunctionValueOrientation(Dimension dim1, bool dim1Max, Dimension dim2, bool dim2Max, Dimension dim3, bool dim3Max)
        {
            int intex = 0;

            if (dim1Max)
            {
                intex = BitCalculator.TurnOn(intex, SelectBitIndex(dim1));
            }
            if (dim2Max)
            {
                intex = BitCalculator.TurnOn(intex, SelectBitIndex(dim2));
            }
            if (dim3Max)
            {
                intex = BitCalculator.TurnOn(intex, SelectBitIndex(dim3));
            }

            Index = (FunctionValueIndex)intex;
        }

        private static int SelectBitIndex(Dimension dimension)
        {
            if (dimension == Dimension.X)
            {
                return XMaxPosition;
            }
            else if (dimension == Dimension.Y)
            {
                return YMaxPosition;
            }
            else if (dimension == Dimension.Z)
            {
                return ZMaxPosition;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public Vector3 GetCorner(BoundingBox box)
        {
            switch (Index)
            {
                case FunctionValueIndex._000:
                    return box.Minimum;
                case FunctionValueIndex._001:
                    return box.Minimum + new Vector3(0, 0, box.Maximum.Z - box.Minimum.Z);
                case FunctionValueIndex._010:
                    return box.Minimum + new Vector3(0, box.Maximum.Y - box.Minimum.Y, 0);
                case FunctionValueIndex._011:
                    return box.Minimum + new Vector3(0, box.Maximum.Y - box.Minimum.Y, box.Maximum.Z - box.Minimum.Z);
                case FunctionValueIndex._100:
                    return box.Minimum + new Vector3(box.Maximum.X - box.Minimum.X, 0, 0);
                case FunctionValueIndex._101:
                    return box.Minimum + new Vector3(box.Maximum.X - box.Minimum.X, 0, box.Maximum.Z - box.Minimum.Z);
                case FunctionValueIndex._110:
                    return box.Minimum + new Vector3(box.Maximum.X - box.Minimum.X, box.Maximum.Y - box.Minimum.Y, 0);
                case FunctionValueIndex._111:
                    return box.Maximum;
                default:
                    throw new ArgumentException();
            }
        }

        public FunctionValueOrientation GetMirrored(Dimension dimension)
        {
            int togglePosition;

            if (dimension == Dimension.X)
            {
                togglePosition = XMaxPosition;
            }
            else if (dimension == Dimension.Y)
            {
                togglePosition = YMaxPosition;
            }
            else if (dimension == Dimension.Z)
            {
                togglePosition = ZMaxPosition;
            }
            else
            {
                throw new ArgumentException();
            }

            FunctionValueIndex mirrored = (FunctionValueIndex)BitCalculator.ToggleBit((int)Index, togglePosition);

            return new FunctionValueOrientation(mirrored);
        }

        public Dimension[] GetInsideDimensions(OctreeOffset offset)
        {
            var self = this;
            return new Dimension[] { Dimension.X, Dimension.Y, Dimension.Z }
                 .Where(axis => (self.IsMaximum(axis) ? 0 : 1) == offset.GetValue(axis))
                 .ToArray();
        }

        public Dimension[] GetOutsideDimensions(OctreeOffset offset)
        {
            var self = this;
            return new Dimension[] { Dimension.X, Dimension.Y, Dimension.Z }
                 .Where(axis => (self.IsMaximum(axis) ? 1 : 0) == offset.GetValue(axis))
                 .ToArray();
        }

        public bool IsMaximum(Dimension dimension)
        {
            if (dimension == Dimension.X)
            {
                return IsXMaximum;
            }
            else if (dimension == Dimension.Y)
            {
                return IsYMaximum;
            }
            else if (dimension == Dimension.Z)
            {
                return IsZMaximum;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public override string ToString()
        {
            string dimString(Dimension d, FunctionValueOrientation o) => $"{d.ToString()}, " + (o.IsMaximum(d) ? "is positive" : "is negative");

            return $"(fv orientation: {dimString(Dimension.X, this)} | {dimString(Dimension.Y, this)} | {dimString(Dimension.Z, this)})";
        }

        public int GetArrayIndex()
        {
            return GetArrayIndex(Index);
        }

        public static int GetArrayIndex(FunctionValueIndex index)
        {
            //map to match expectation in triangulation table
            switch (index)
            {
                case FunctionValueIndex._000:
                    return 0;
                case FunctionValueIndex._001:
                    return 3;
                case FunctionValueIndex._010:
                    return 4;
                case FunctionValueIndex._011:
                    return 7;
                case FunctionValueIndex._100:
                    return 1;
                case FunctionValueIndex._101:
                    return 2;
                case FunctionValueIndex._110:
                    return 5;
                case FunctionValueIndex._111:
                    return 6;
                default:
                    throw new ArgumentException();
            }
        }

        public static FunctionValueIndex GetFunctionValueIndex(int index)
        {
            //map to match expectation in triangulation table
            switch (index)
            {
                case 0:
                    return FunctionValueIndex._000;
                case 1:
                    return FunctionValueIndex._100;
                case 2:
                    return FunctionValueIndex._101;
                case 3:
                    return FunctionValueIndex._001;
                case 4:
                    return FunctionValueIndex._010;
                case 5:
                    return FunctionValueIndex._110;
                case 6:
                    return FunctionValueIndex._111;
                case 7:
                    return FunctionValueIndex._011;
                default:
                    throw new ArgumentException();
            }
        }
    }
}
