using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Domain
{
    public enum Dimension
    {
        X = 0,
        Y = 1,
        Z = 2,
        Count = 3
    }

    public static class Dimensions
    {
        public static readonly Dimension[] All = new Dimension[] { Dimension.X, Dimension.Y, Dimension.Z };
    }
}
