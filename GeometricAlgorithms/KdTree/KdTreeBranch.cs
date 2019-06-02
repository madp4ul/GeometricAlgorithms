using GeometricAlgorithms.VertexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.KdTree
{
    class KdTreeBranch<TVertex> : KdTreeNode<TVertex> where TVertex : Vertex
    {
        public KdTreeNode<TVertex> MinimumChild { get; set; }
        public KdTreeNode<TVertex> MaximumChild { get; set; }

        private Dimension HalfedDimension { get; set; }

        public KdTreeBranch(BoundingBox boundingBox, Range<TVertex> vertices, KdTreeConfiguration configuration, Dimension halfedDimension = Dimension.X)
            : base(boundingBox, vertices.Length, halfedDimension)
        {
            HalfedDimension = halfedDimension;

            //If more vertices than what fits into to leafs, create more branches
            if (vertices.Length > configuration.MaximumPointsPerLeaf * 2)
            {

            }
            else //create leafs
            {
                //Sort values along current dimension
                var comparer = new VertexComparer(halfedDimension);
                vertices.Sort(comparer);

                //split value range into to sections, one for each child
                int halfIndex = vertices.Length / 2;
                MinimumChild = new KdTreeLeaf<TVertex>(boundingBox, vertices.GetRange(0, halfIndex), configuration);
                MaximumChild = new KdTreeLeaf<TVertex>(boundingBox, vertices.GetRange(halfIndex, vertices.Length - halfIndex), configuration);
            }
        }

        private int FindMedianIndex(Range<TVertex> vertices)
        {

        }

        private void CreateChildren(Range<TVertex> vertices, KdTreeConfiguration config,)
        {

        }

        protected override IReadOnlyList<TVertex> FindInRadius(Vector3 seachCenter, float searchRadius)
        {
            throw new NotImplementedException();
        }

        protected override IReadOnlyList<TVertex> FindNearestVertices(Vector3 searchPosition, int pointAmount)
        {
            throw new NotImplementedException();
        }

        private class VertexComparer : IComparer<TVertex>
        {
            private Func<TVertex, float> DimensionSelector;

            public VertexComparer(Dimension dimension)
            {
                switch (dimension)
                {
                    case Dimension.X:
                        DimensionSelector = v => v.Position.X;
                        break;
                    case Dimension.Y:
                        DimensionSelector = v => v.Position.Y;
                        break;
                    case Dimension.Z:
                        DimensionSelector = v => v.Position.Z;
                        break;
                    default:
                        throw new ArgumentException("No valid dimension");
                }
            }

            public int Compare(TVertex v1, TVertex v2)
            {
                float diff = DimensionSelector(v1) - DimensionSelector(v2);

                if (diff > 0)
                {
                    return 1;
                }
                if (diff < 0)
                {
                    return -1;
                }

                return 0;
            }
        }
    }
}
