using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Tasks;

namespace GeometricAlgorithms.MeshQuerying
{
    internal class TreeLeaf : ATreeNode
    {
        public Range<PositionIndex> Vertices { get; set; }

        public sealed override int NodeCount { get => 1; protected set { } }

        public sealed override int LeafCount { get => 1; protected set { } }

        public override int ChildCount => 0;

        public TreeLeaf(ATreeNode parent, BoundingBox boundingBox, Range<PositionIndex> vertices, OperationProgressUpdater progressUpdater, int depth)
               : base(parent, boundingBox, vertices.Length, depth)
        {
            Vertices = vertices ?? throw new ArgumentNullException(nameof(vertices));
            progressUpdater.UpdateAddOperation();
        }

        internal sealed override void AddLeaves(List<TreeLeaf> leaves)
        {
            leaves.Add(this);
        }

        internal sealed override void AddBranches(List<ATreeBranch> branches)
        {

        }

        internal sealed override void FindInRadius(InRadiusQuery query)
        {
            foreach (var vertex in Vertices)
            {
                if ((vertex.Position - query.SeachCenter).LengthSquared < query.SearchRadiusSquared)
                {
                    query.ResultSet.Add(vertex);
                }
            }
            query.ProgressUpdater.UpdateAddOperation();
        }

        internal sealed override void FindNearestVertices(NearestVerticesQuery query)
        {
            foreach (var vertex in Vertices)
            {
                float distance = (vertex.Position - query.SearchPosition).Length;
                if (distance < query.MaxSearchRadius)
                {
                    //If list is full, remove element with furthest distance to be replaced with new point
                    if (query.ResultSet.Count == query.PointAmount)
                    {
                        query.ResultSet.Dequeue();
                    }

                    query.ResultSet.Enqueue(new PositionIndexDistance(distance, vertex));

                    //If list is full after adding point recalculate max search radius with furthest distance
                    if (query.ResultSet.Count == query.PointAmount)
                    {
                        query.MaxSearchRadius = query.ResultSet.Peek().Distance;
                    }
                }
            }

            query.ProgressUpdater.UpdateAddOperation();
        }
    }
}
