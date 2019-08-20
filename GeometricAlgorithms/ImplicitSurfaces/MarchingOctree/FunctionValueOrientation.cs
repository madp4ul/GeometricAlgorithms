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
