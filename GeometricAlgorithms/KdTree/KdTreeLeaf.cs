using GeometricAlgorithms.Domain.VertexTypes;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.KdTree
{
    class KdTreeLeaf<TVertex> : KdTreeNode<TVertex> where TVertex : Vertex
    {
        public Range<TVertex> Vertices { get; set; }

        public KdTreeLeaf(BoundingBox boundingBox, Range<TVertex> vertices, KdTreeConfiguration configuration)
               : base(boundingBox, vertices.Length)
        {
            Vertices = vertices ?? throw new ArgumentNullException(nameof(vertices));
        }

        public override void FindInRadius(InRadiusQuery<TVertex> query)
        {
            foreach (var vertex in Vertices)
            {
                if (Vector3.DistanceSquared(vertex.Position, query.SeachCenter) < query.SearchRadiusSquared)
                {
                    query.ResultSet.Add(vertex);
                }
            }
        }

        public override void FindNearestVertices(NearestVerticesQuery<TVertex> query)
        {
            foreach (TVertex vertex in Vertices)
            {
                float distance = Vector3.Distance(vertex.Position, query.SearchPosition);
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
        }
    }
}
