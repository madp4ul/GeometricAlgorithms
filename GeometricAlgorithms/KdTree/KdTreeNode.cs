using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.VertexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.KdTree
{
    abstract class KdTreeNode<TVertex> where TVertex : IVertex
    {
        public int VertexCount { get; set; }

        public BoundingBox BoundingBox { get; private set; }


        protected KdTreeNode(BoundingBox boundingBox, int verticesCount)
        {
            VertexCount = verticesCount;
            BoundingBox = boundingBox ?? throw new ArgumentNullException(nameof(boundingBox));
        }

        public abstract void FindInRadius(InRadiusQuery<TVertex> query);

        public abstract void FindNearestVertices(NearestVerticesQuery<TVertex> query);
    }
}
