using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.MeshQuerying
{
    public class TreeConfiguration
    {
        public readonly int MaximumPointsPerLeaf;
        public readonly bool MinimizeContainers;

        public TreeConfiguration(int maximumPointsPerLeaf = 2, bool minimizeContainers = true)
        {
            MaximumPointsPerLeaf = maximumPointsPerLeaf;
            MinimizeContainers = minimizeContainers;
        }

        public static TreeConfiguration CreateChange(TreeConfiguration changedConfig, int? maximumPointsPerLeaf = null, bool? minimizeContainers = null)
        {
            return new TreeConfiguration(
                maximumPointsPerLeaf: maximumPointsPerLeaf ?? changedConfig.MaximumPointsPerLeaf,
                minimizeContainers: minimizeContainers ?? changedConfig.MinimizeContainers);
        }
    }
}