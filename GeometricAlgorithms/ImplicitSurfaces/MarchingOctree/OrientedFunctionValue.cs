using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree
{
    struct OrientedFunctionValue
    {
        public readonly FunctionValue FunctionValue;
        public readonly FunctionValueOrientation Orientation;

        public OrientedFunctionValue(FunctionValue functionValue, FunctionValueOrientation orientation)
        {
            FunctionValue = functionValue;
            Orientation = orientation;
        }

        public bool HasFunctionValue => FunctionValue != null;

        public override string ToString()
        {
            return $"{{{Orientation.ToString()}, {FunctionValue?.ToString()}}}";
        }
    }
}
