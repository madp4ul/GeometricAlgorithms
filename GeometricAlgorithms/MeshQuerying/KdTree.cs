using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.MeshQuerying
{
    public class KdTree
    {
        private readonly KdTreeNode Root;

        public readonly BoundingBox MeshContainer;

        public Mesh Mesh { get; private set; }

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
                Root = new KdTreeBranch(MeshContainer, range, configuration, updater);
            }
            else
            {
                Root = new KdTreeLeaf(MeshContainer, range, updater);
            }

            updater.IsCompleted();
        }

        public KdTree Reshape(KdTreeConfiguration configuration)
        {
            return new KdTree(Mesh, configuration);
        }

        /// <summary>
        /// Query for indices of vertices that are in radius around query center
        /// </summary>
        /// <param name="queryCenter"></param>
        /// <param name="queryRadius"></param>
        /// <param name="progressUpdater"></param>
        /// <returns></returns>
        public List<PositionIndex> FindInRadius(
            Vector3 queryCenter,
            float queryRadius,
            IProgressUpdater progressUpdater = null)
        {
            var resultList = new List<PositionIndex>();

            var kdTreeProgress = new OperationProgressUpdater(progressUpdater, Root.LeafCount, "Looking for vertices in radius");

            Root.FindInRadius(new InRadiusQuery(queryCenter, queryRadius, resultList, kdTreeProgress));

            kdTreeProgress.IsCompleted();

            return resultList;
        }

        /// <summary>
        /// Query for amount of indices of closest vertices
        /// </summary>
        /// <param name="queryPosition"></param>
        /// <param name="pointAmount"></param>
        /// <param name="progressUpdater"></param>
        /// <returns></returns>
        public SortedList<float, PositionIndex> FindNearestVertices(
            Vector3 queryPosition,
            int pointAmount,
            IProgressUpdater progressUpdater = null)
        {
            var resultSet = new SortedList<float, PositionIndex>(new DistanceComparer());

            var kdTreeProgress = new OperationProgressUpdater(progressUpdater, Root.LeafCount, "Looking for nearest points");

            Root.FindNearestVertices(new NearestVerticesQuery(
                queryPosition,
                pointAmount,
                resultSet,
                kdTreeProgress
            ));

            kdTreeProgress.IsCompleted();

            return resultSet;
        }

        public List<BoundingBox> GetLeafBoudingBoxes()
        {
            var leaves = new List<KdTreeLeaf>();
            Root.AddLeaves(leaves);
            return leaves.Select(leaf => leaf.BoundingBox).ToList();
        }

        public List<BoundingBox> GetBranchBoudingBoxes()
        {
            var branches = new List<KdTreeBranch>();
            Root.AddBranches(branches);
            return branches.Select(leaf => leaf.BoundingBox).ToList();
        }
    }
}
