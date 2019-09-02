using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.MeshQuerying
{
    public class KdTree : ATree
    {
        public KdTree(Mesh mesh, TreeConfiguration configuration = null, IProgressUpdater progressUpdater = null)
            : base(mesh, configuration)
        {
            if (configuration == null)
            {
                configuration = new TreeConfiguration();
            }


            //Needs position mapping to preserve original indices because 
            //they are necessary to find the other related data in the model
            var positionMapping = mesh.Positions
                .Select((vector, index) => new PositionIndex(vector, index))
                .ToArray();

            var range = Range<PositionIndex>.FromArray(positionMapping, 0, mesh.VertexCount);

            var updater = new OperationProgressUpdater(
                progressUpdater,
                (2 * mesh.VertexCount) / configuration.MaximumPointsPerLeaf,
                "Building Kd-Tree");

            if (mesh.VertexCount > configuration.MaximumPointsPerLeaf)
            {
                Root = new KdTreeBranch(parent: null, MeshContainer, range, configuration, updater, depth: 1);
            }
            else
            {
                Root = new TreeLeaf(parent: null, MeshContainer, range, updater, depth: 1);
            }

            updater.IsCompleted();
        }
    }
}
