using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree
{
    struct FunctionValueOrientation
    {
        private const int XMaxPosition = 0;
        private const int YMaxPosition = 1;
        private const int ZMaxPosition = 2;

        public readonly FunctionValueIndex Index;
        public bool IsXMaximum => BitCalculator.IsOn((int)Index, XMaxPosition);
        public bool IsYMaximum => BitCalculator.IsOn((int)Index, YMaxPosition);
        public bool IsZMaximum => BitCalculator.IsOn((int)Index, ZMaxPosition);

        public FunctionValueOrientation(FunctionValueIndex index)
        {
            Index = index;
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

        public int GetArrayIndex()
        {
            return GetArrayIndex(Index);
        }

        public static int GetArrayIndex(FunctionValueIndex index)
        {
            return (int)index;
        }
    }
}
