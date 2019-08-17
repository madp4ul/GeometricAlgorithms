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
        private readonly ATreeNode RootNode;

        public readonly BoundingBox MeshContainer;

        public Mesh Mesh { get; private set; }

        protected override ATreeNode Root => RootNode;

        public KdTree(Mesh mesh, KdTreeConfiguration configuration = null, IProgressUpdater progressUpdater = null)
        {
            if (configuration == null)
            {
                configuration = new KdTreeConfiguration();
            }

            Mesh = mesh;

            //Needs position mapping to preserve original indices because 
            //they are necessary to find the other related data in the model
            var positionMapping = mesh.Positions
                .Select((vector, index) => new PositionIndex(vector, index))
                .ToArray();

            var range = Range<PositionIndex>.FromArray(positionMapping, 0, mesh.VertexCount);
            MeshContainer = BoundingBox.CreateContainer(mesh.Positions);

            var updater = new OperationProgressUpdater(
                progressUpdater,
                (2 * mesh.VertexCount) / configuration.MaximumPointsPerLeaf,
                "Building Kd-Tree");

            if (mesh.VertexCount > configuration.MaximumPointsPerLeaf)
            {
                RootNode = new KdTreeBranch(MeshContainer, range, configuration, updater);
            }
            else
            {
                RootNode = new TreeLeaf(MeshContainer, range, updater);
            }

            updater.IsCompleted();
        }
    }
}
