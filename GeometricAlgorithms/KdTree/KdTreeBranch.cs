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

        private Dimension HalfedDimension;
        private Func<Vector3, float> DimensionSelector;

        public KdTreeBranch(BoundingBox boundingBox, Range<TVertex> vertices, KdTreeConfiguration configuration, Dimension halfedDimension = Dimension.X)
            : base(boundingBox, vertices.Length)
        {
            HalfedDimension = halfedDimension;

            int halfIndex = vertices.Length / 2;

            DimensionSelector = GetDimensionSelector(halfedDimension);

            //Sort values along current dimension
            var comparer = new VertexComparer(DimensionSelector);
            vertices.NthElement(halfIndex, comparer.Compare);

            //Split space at median along halved dimension
            float halfSpace = DimensionSelector(vertices[halfIndex].Position);

            //Create bounding box halves along median
            BoundingBox minChildBox = boundingBox.GetMinHalfAlongDimension(halfedDimension, halfSpace);
            BoundingBox maxChildBox = boundingBox.GetMaxHalfAlongDimension(halfedDimension, halfSpace);

            //split value range into to sections, one for each child
            Range<TVertex> minChildVertices = vertices.GetRange(0, halfIndex);
            Range<TVertex> maxChildVertices = vertices.GetRange(halfIndex, vertices.Length - halfIndex);

            //If more vertices than what fits into to leafs, create more branches
            if (vertices.Length > configuration.MaximumPointsPerLeaf * 2)
            {
                Dimension nextDimension = GetNextDimension(halfedDimension);

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

        public override IReadOnlyList<TVertex> FindInRadius(Vector3 seachCenter, float searchRadius, RadiusQueryData data)
        {
            float positionAlongDimension = DimensionSelector(seachCenter);

            float distanceAboveMinimum = MinimumChild.BoundingBox.GetDistanceAboveDimension(positionAlongDimension, DimensionSelector);

            if (distanceAboveMinimum > 0)
            {

            }

            //update for minimum

            float previousDistance = data.GetDistance(HalfedDimension, true);

            //When going down a level in the tree, the bounding box shrinks.
            //So the distance only grows or stays the same.
            data.UpdateDistance(HalfedDimension, true, BoundingBox.GetDistanceAboveDimension(positionAlongDimension, DimensionSelector));

            //If distance is smaller than seach radius, it may contain points in radius
            if (data.MaximumDistance < searchRadius)
            {
                MinimumChild.FindInRadius(seachCenter, searchRadius, data);
            }
            //reset update
            data.UpdateDistance(HalfedDimension, true, previousDistance);

            //update for maximum

            previousDistance = data.GetDistance(HalfedDimension, false);
            //When going down a level in the tree, the bounding box shrinks.
            //So the distance only grows or stays the same.
            data.UpdateDistance(HalfedDimension, false, BoundingBox.GetDistanceBelowDimension(positionAlongDimension, DimensionSelector));

            //If distance is smaller than seach radius, it may contain points in radius
            if (data.MaximumDistance < searchRadius)
            {
                MaximumChild.FindInRadius(seachCenter, searchRadius, data);
            }
            //reset update
            data.UpdateDistance(HalfedDimension, false, previousDistance);
        }

        protected override IReadOnlyList<TVertex> FindNearestVertices(Vector3 searchPosition, int pointAmount)
        {
            throw new NotImplementedException();
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
