using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometricAlgorithms.Domain;

namespace GeometricAlgorithms.MeshQuerying
{
    abstract class ATreeBranch : ATreeNode
    {
        public override int NodeCount { get; protected set; }
        public override int LeafCount { get; protected set; }

        public ATreeBranch(ATreeNode parent, BoundingBox boundingBox, int verticesCount, int depth)
            : base(parent, boundingBox, verticesCount, depth)
        {
        }

        internal sealed override void AddBranches(List<ATreeBranch> branches)
        {
            branches.Add(this);

            foreach (var child in GetChildren())
            {
                child.AddBranches(branches);
            }
        }

        internal sealed override void AddLeaves(List<TreeLeaf> leaves)
        {
            foreach (var child in GetChildren())
            {
                child.AddLeaves(leaves);
            }
        }

        internal sealed override void FindInRadius(InRadiusQuery query)
        {
            foreach (var child in GetChildren())
            {
                if (child.BoundingBox.GetMinimumDistance(query.SeachCenter) < query.SearchRadius)
                {
                    child.FindInRadius(query);
                }
                else
                {
                    //If branch can be skipped, add progress for whole branch
                    query.ProgressUpdater.UpdateAddOperation(child.LeafCount);
                }
            }
        }

        internal sealed override void FindNearestVertices(NearestVerticesQuery query)
        {
            foreach (var child in GetChildren())
            {
                //Enter child if still more more required to fill result or if distance is smaller than maxSearchRadius
                //which means that child potentially contains better points
                if (query.ResultSet.Count < query.PointAmount
                    || child.BoundingBox.GetMinimumDistance(query.SearchPosition) < query.MaxSearchRadius)
                {
                    child.FindNearestVertices(query);
                }
                else
                {
                    //If branch can be skipped, add progress for whole branch
                    query.ProgressUpdater.UpdateAddOperation(child.LeafCount);
                }
            }
        }

        public abstract IEnumerable<ATreeNode> GetChildren();
    }
}
