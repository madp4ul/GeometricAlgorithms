using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.KdTree
{
    class KdTreeConfiguration
    {
        public int MaximumPointsPerLeaf { get; set; }


        public static KdTreeConfiguration Default
        {
            get
            {
                return new KdTreeConfiguration
                {
                    MaximumPointsPerLeaf = 10,
                };
            }
        }
    }