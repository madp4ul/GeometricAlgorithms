using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.MeshQuerying
{
    public struct KdTreeConfiguration
    {
        public int MaximumPointsPerLeaf { get; set; }


        public static KdTreeConfiguration Default
        {
            get
            {
                return new KdTreeConfiguration
                {
                    MaximumPointsPerLeaf = 2,
                };
            }
        }
    }
}