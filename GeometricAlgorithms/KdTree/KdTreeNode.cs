using GeometricAlgorithms.VertexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.KdTree
{
    abstract class KdTreeNode<TVertex> where TVertex : Vertex
    {


        public int VertexCount { get; set; }

        public BoundingBox BoundingBox { get; private set; }
        

        protected KdTreeNode(BoundingBox boundingBox, int verticesCount)
        {
            VertexCount = verticesCount;
            BoundingBox = boundingBox ?? throw new ArgumentNullException(nameof(boundingBox));
        }



        protected abstract IReadOnlyList<TVertex> FindNearestVertices(Vector3 searchPosition, int pointAmount);
        protected abstract IReadOnlyList<TVertex> FindInRadius(Vector3 seachCenter, float searchRadius);
    }
}
