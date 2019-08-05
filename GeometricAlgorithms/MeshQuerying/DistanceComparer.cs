using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.MeshQuerying
{
    class DistanceComparer : IComparer<float>
    {
        public int Compare(float x, float y)
        {
            int result = x.CompareTo(y);

            if (result == 0)
            {
                return 1;   // Handle equality as beeing greater
            }

            return result;
        }
    }
}
