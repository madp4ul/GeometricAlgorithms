using GeometricAlgorithms.VertexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.KdTree
{
    class KdTree<TVertex> : I3DQueryable<TVertex> where TVertex : Vertex
    {
        private KdTreeNode<TVertex> Root;

        public KdTree(TVertex[] vertices, KdTreeConfiguration configuration = null)
        {
            if (configuration == null)
            {
                configuration = KdTreeConfiguration.Default;
            }

            var range = Range<TVertex>.FromArray(vertices, 0, vertices.Length);
            var rootBoundingBox = BoundingBox.CreateContainer(vertices);

            if (vertices.Length > configuration.MaximumPointsPerLeaf)
            {
                Root = new KdTreeBranch<TVertex>(rootBoundingBox, range, configuration);
            }
            else
            {
                Root = new KdTreeLeaf<TVertex>(rootBoundingBox, range, configuration);
            }
        }

        public IReadOnlyList<TVertex> FindInRadius(Vector3 seachCenter, float searchRadius)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<TVertex> FindNearestVertices(Vector3 searchPosition, int pointAmount)
        {
            throw new NotImplementedException();
        }
    }
}
