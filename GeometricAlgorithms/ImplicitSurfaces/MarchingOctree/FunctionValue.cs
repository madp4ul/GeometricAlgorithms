using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree
{
    public class FunctionValue
    {
        public readonly Vector3 Position;
        public readonly float Value;

        public FunctionValue(Vector3 position, float value)
        {
            Position = position;
            Value = value;
        }

        public bool IsInside()
        {
            return Value < 0;
        }
    }
}
