using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.MeshQuerying
{
    class KdTreeLeaf : KdTreeNode
    {
        public Range<PositionIndex> Vertices { get; set; }
        public override int NodeCount { get => 1; protected set { } }

        public override int LeafCount { get => 1; protected set { } }

        public KdTreeLeaf(BoundingBox boundingBox, Range<PositionIndex> vertices, OperationProgressUpdater progressUpdater)
               : base(boundingBox, vertices.Length)
        {
            Vertices = vertices ?? throw new ArgumentNullException(nameof(vertices));
            progressUpdater.UpdateAddOperation();
        }

        public override void FindInRadius(InRadiusQuery query)
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

        public override void FindNearestVertices(NearestVerticesQuery query)
        {
            foreach (var vertex in Vertices)
            {
                float distance = (vertex.Position - query.SearchPosition).Length;
                if (distance < query.MaxSearchRadius)
                {
                    //If list is full, remove last element to be replaced with new point
                    if (query.ResultSet.Count == query.PointAmount)
                    {
                        query.ResultSet.RemoveAt(query.ResultSet.Count - 1);
                    }

                    query.ResultSet.Add(distance, vertex);

                    //If list is full after adding point refresh max search radius with last
                    if (query.ResultSet.Count == query.PointAmount)
                    {
                        query.MaxSearchRadius = query.ResultSet.Keys[query.ResultSet.Count - 1];
                    }
                }
            }

            query.ProgressUpdater.UpdateAddOperation();
        }

        public override void AddLeaves(List<KdTreeLeaf> leaves)
        {
            leaves.Add(this);
        }

        public override void AddBranches(List<KdTreeBranch> branches)
        {

        }
    }
}
