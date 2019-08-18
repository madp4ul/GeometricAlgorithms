using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Tasks;

namespace GeometricAlgorithms.MeshQuerying
{
    class Octree : ATree
    {
        private readonly ATreeNode _Root;
        protected override ATreeNode Root => _Root;


        private readonly Mesh _Mesh;
        public override Mesh Mesh => _Mesh;


        private readonly BoundingBox _MeshContainer;
        public override BoundingBox MeshContainer => _MeshContainer;

        public Octree(Mesh mesh, TreeConfiguration configuration = null, IProgressUpdater progressUpdater = null)
        {
            if (configuration == null)
            {
                configuration = new TreeConfiguration();
            }

            _Mesh = mesh;

            //Needs position mapping to preserve original indices because 
            //they are necessary to find the other related data in the model
            var positionMapping = mesh.Positions
                .Select((vector, index) => new PositionIndex(vector, index))
                .ToArray();

            var range = Range<PositionIndex>.FromArray(positionMapping, 0, mesh.VertexCount);
            _MeshContainer = BoundingBox.CreateContainer(mesh.Positions);

            var updater = new OperationProgressUpdater(
                progressUpdater,
                (2 * mesh.VertexCount) / configuration.MaximumPointsPerLeaf,
                "Building Kd-Tree");

            if (mesh.VertexCount > configuration.MaximumPointsPerLeaf)
            {
                _Root = new OctreeBranch(MeshContainer, range, configuration, updater);
            }
            else
            {
                _Root = new TreeLeaf(MeshContainer, range, updater);
            }

            updater.IsCompleted();
        }
    }
}
