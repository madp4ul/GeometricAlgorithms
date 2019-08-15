using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.MeshQuerying
{
    public struct KdTreeConfiguration
    {
        public readonly int MaximumPointsPerLeaf;
        public readonly bool MinimizeContainers;

        public KdTreeConfiguration(int maximumPointsPerLeaf = 2, bool minimizeContainers = true)
        {
            MaximumPointsPerLeaf = maximumPointsPerLeaf;
            MinimizeContainers = minimizeContainers;
        }

        public static KdTreeConfiguration CreateChange(KdTreeConfiguration changedConfig, int? maximumPointsPerLeaf = null, bool? minimizeContainers = null)
        {
            return new KdTreeConfiguration(
                maximumPointsPerLeaf: maximumPointsPerLeaf ?? changedConfig.MaximumPointsPerLeaf,
                minimizeContainers: minimizeContainers ?? changedConfig.MinimizeContainers);
        }
    }
}