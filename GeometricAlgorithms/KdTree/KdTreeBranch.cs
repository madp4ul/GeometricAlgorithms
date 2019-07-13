using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.VertexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.KdTree
{
    class KdTreeBranch<TVertex> : KdTreeNode<TVertex> where TVertex : IVertex
    {
        public KdTreeNode<TVertex> MinimumChild { get; set; }
        public KdTreeNode<TVertex> MaximumChild { get; set; }

        private readonly Dimension HalvedDimension;
        private readonly Func<Vector3, float> DimensionSelector;

        public KdTreeBranch(BoundingBox boundingBox, Range<TVertex> vertices, KdTreeConfiguration configuration, Dimension halvedDimension = Dimension.X)
            : base(boundingBox, vertices.Length)
        {
            HalvedDimension = halvedDimension;

            int halfIndex = vertices.Length / 2;

            DimensionSelector = GetDimensionSelector(halvedDimension);

            //Sort values along current dimension
            var comparer = new VertexComparer(DimensionSelector);
            vertices.NthElement(halfIndex, comparer.Compare);

            //Split space at median along halved dimension
            float halfSpace = DimensionSelector(vertices[halfIndex].Position);

            //Create bounding box halves along median
            BoundingBox minChildBox = boundingBox.GetMinHalfAlongDimension(halvedDimension, halfSpace);
            BoundingBox maxChildBox = boundingBox.GetMaxHalfAlongDimension(halvedDimension, halfSpace);

            //split value range into to sections, one for each child
            Range<TVertex> minChildVertices = vertices.GetRange(0, halfIndex);
            Range<TVertex> maxChildVertices = vertices.GetRange(halfIndex, vertices.Length - halfIndex);

            //If more vertices than what fits into to leafs, create more branches
            if (vertices.Length > configuration.MaximumPointsPerLeaf * 2)
            {
                Dimension nextDimension = GetNextDimension(halvedDimension);

                MinimumChild = new KdTreeBranch<TVertex>(minChildBox, minChildVertices, configuration, nextDimension);
                MaximumChild = new KdTreeBranch<TVertex>(maxChildBox, maxChildVertices, configuration, nextDimension);
            }
            else //create leafs
            {
                MinimumChild = new KdTreeLeaf<TVertex>(
                    minChildBox,
                    minChildVertices,
                    configuration);

                MaximumChild = new KdTreeLeaf<TVertex>(
                    maxChildBox,
                    maxChildVertices,
                    configuration);
            }
        }

        protected virtual Dimension GetNextDimension(Dimension dimension)
        {
            return (Dimension)((int)(dimension + 1) % (int)Dimension.Count);
        }

        public override void FindInRadius(InRadiusQuery<TVertex> query)
        {

            if (MinimumChild.BoundingBox.GetMinimumDistance(query.SeachCenter) < query.SearchRadius)
            {
                MinimumChild.FindInRadius(query);
            }

            if (MaximumChild.BoundingBox.GetMinimumDistance(query.SeachCenter) < query.SearchRadius)
            {
                MaximumChild.FindInRadius(query);
            }
        }

        public override void FindNearestVertices(NearestVerticesQuery<TVertex> query)
        {
            //Enter child if still more more required to fill result or if distance is smaller than maxSearchRadius
            //which means that child potentially contains better points
            if (query.ResultSet.Count < query.PointAmount
                || MinimumChild.BoundingBox.GetMinimumDistance(query.SearchPosition) < query.MaxSearchRadius)
            {
                MinimumChild.FindNearestVertices(query);
            }

            if (query.ResultSet.Count < query.PointAmount
                || MaximumChild.BoundingBox.GetMinimumDistance(query.SearchPosition) < query.MaxSearchRadius)
            {
                MaximumChild.FindNearestVertices(query);
            }
        }

        private static Func<Vector3, float> GetDimensionSelector(Dimension dimension)
        {
            switch (dimension)
            {
                case Dimension.X:
                    return v => v.X;
                case Dimension.Y:
                    return v => v.Y;
                case Dimension.Z:
                    return v => v.Z;
                default:
                    throw new ArgumentException("No valid dimension");
            }
        }

        private class VertexComparer : IComparer<TVertex>
        {
            public Func<Vector3, float> DimensionSelector { get; private set; }

            public VertexComparer(Func<Vector3, float> dimensionSelector)
            {
                DimensionSelector = dimensionSelector;
            }

            public int Compare(TVertex v1, TVertex v2)
            {
                float diff = DimensionSelector(v1.Position) - DimensionSelector(v2.Position);

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
