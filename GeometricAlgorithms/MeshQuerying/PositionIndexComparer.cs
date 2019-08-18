using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.MeshQuerying
{
    class PositionIndexComparer : IComparer<PositionIndex>
    {
        public Func<Vector3, float> DimensionSelector { get; private set; }

        public PositionIndexComparer(Func<Vector3, float> dimensionSelector)
        {
            DimensionSelector = dimensionSelector;
        }

        public int Compare(PositionIndex v1, PositionIndex v2)
        {
            float diff = DimensionSelector(v1.Position) - DimensionSelector(v2.Position);

            if (diff > 0)
            {
                return 1;
            }
            if (diff < 0)
            {
                return -1;
            }

            return 0;
        }
    }
}
