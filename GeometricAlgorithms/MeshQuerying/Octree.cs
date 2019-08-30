using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Tasks;

namespace GeometricAlgorithms.MeshQuerying
{
    public class Octree : ATree
    {
        public Octree(Mesh mesh, TreeConfiguration configuration = null, IProgressUpdater progressUpdater = null)
            : base(mesh, configuration)
        {
            //Needs position mapping to preserve original indices because 
            //they are necessary to find the other related data in the model
            var positionMapping = mesh.Positions
                .Select((vector, index) => new PositionIndex(vector, index))
                .ToArray();

            var range = Range<PositionIndex>.FromArray(positionMapping, 0, mesh.VertexCount);

            MeshContainer.GrowToCube();

            var updater = new OperationProgressUpdater(
                progressUpdater,
                (8 * mesh.VertexCount) / configuration.MaximumPointsPerLeaf,
                "Building Kd-Tree");

            if (mesh.VertexCount > configuration.MaximumPointsPerLeaf)
            {
                Root = new OctreeBranch(MeshContainer, range, configuration, updater, depth: 1);
            }
            else
            {
                Root = new TreeLeaf(MeshContainer, range, updater, depth: 1);
            }

            updater.IsCompleted();
        }
    }
}
