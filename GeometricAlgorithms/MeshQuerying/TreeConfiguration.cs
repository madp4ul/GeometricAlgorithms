using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.MeshQuerying
{
    public class TreeConfiguration
    {
        /// <summary>
        /// How many points can be in each leaf of the tree
        /// </summary>
        public readonly int MaximumPointsPerLeaf;

        /// <summary>
        /// If the tree node containers should be minimized to the minimum container
        /// that contains all points to increase query peformance
        /// </summary>
        public readonly bool MinimizeContainers;

        /// <summary>
        /// The scale of the parent container relative to the minimum container 
        /// that contains all points of the mesh.
        /// </summary>
        public readonly float MeshContainerScale;

        public TreeConfiguration(
            int maximumPointsPerLeaf = 2,
            bool minimizeContainers = true,
            float meshContainerScale = 1)
        {
            if (maximumPointsPerLeaf < 1
                || meshContainerScale < 1)
            {
                throw new ArgumentException();
            }

            MaximumPointsPerLeaf = maximumPointsPerLeaf;
            MinimizeContainers = minimizeContainers;
            MeshContainerScale = meshContainerScale;
        }

        public static TreeConfiguration CreateChange(TreeConfiguration changedConfig,
            int? maximumPointsPerLeaf = null,
            bool? minimizeContainers = null,
            float? meshContainerScale = null)
        {
            return new TreeConfiguration(
                maximumPointsPerLeaf: maximumPointsPerLeaf ?? changedConfig.MaximumPointsPerLeaf,
                minimizeContainers: minimizeContainers ?? changedConfig.MinimizeContainers,
                meshContainerScale: meshContainerScale ?? changedConfig.MeshContainerScale);
        }
    }
}