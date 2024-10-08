﻿using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Tasks;
using GeometricAlgorithms.Domain.Trees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.MeshQuerying
{
    public abstract class ATree : IEnumerableTree
    {
        public readonly Mesh Mesh;
        public readonly TreeConfiguration Configuration;

        public ATreeNode Root { get; protected set; }
        public BoundingBox MeshContainer { get; protected set; }

        public ATree(Mesh mesh, TreeConfiguration configuration)
        {
            Mesh = mesh;
            Configuration = configuration ?? new TreeConfiguration();

            MeshContainer = BoundingBox.CreateContainer(mesh.Positions);
            MeshContainer.ScaleAroundCenter(configuration.MeshContainerScale);
        }

        public List<BoundingBox> GetLeafBoudingBoxes()
        {
            var leaves = new List<TreeLeaf>();
            Root.AddLeaves(leaves);
            return leaves.Select(leaf => leaf.BoundingBox).ToList();
        }

        public List<BoundingBox> GetBranchBoudingBoxes()
        {
            var branches = new List<ATreeBranch>();
            Root.AddBranches(branches);
            return branches.Select(leaf => leaf.BoundingBox).ToList();
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
        public PriorityQueue<PositionIndexDistance> FindNearestVertices(
            Vector3 queryPosition,
            int pointAmount,
            IProgressUpdater progressUpdater = null)
        {
            var resultSet = new PriorityQueue<PositionIndexDistance>(capacity: pointAmount);

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

        public ITreeEnumerator GetTreeEnumerator()
        {
            return new TreeEnumerator(this);
        }
    }
}
