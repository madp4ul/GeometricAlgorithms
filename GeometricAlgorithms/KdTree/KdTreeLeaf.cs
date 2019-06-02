using GeometricAlgorithms.VertexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

        protected override IReadOnlyList<TVertex> FindInRadius(Vector3 seachCenter, float searchRadius)
        {
            throw new NotImplementedException();
        }

        protected override IReadOnlyList<TVertex> FindNearestVertices(Vector3 searchPosition, int pointAmount)
        {
            throw new NotImplementedException();
        }
    }
}
