using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.MeshQuerying
{
    class KdTreeBranch : KdTreeNode
    {
        public KdTreeNode MinimumChild { get; set; }
        public KdTreeNode MaximumChild { get; set; }
        public override int NodeCount { get; protected set; }
        public override int LeafCount { get; protected set; }

        private readonly Func<Vector3, float> DimensionSelector;

        public KdTreeBranch(
            BoundingBox boundingBox,
            Range<PositionIndex> vertices,
            KdTreeConfiguration configuration,
            KdTreeProgressUpdater progressUpdater,
            Dimension halvedDimension = Dimension.X)
            : base(boundingBox, vertices.Length)
        {
            int halfIndex = vertices.Length / 2;

            DimensionSelector = GetDimensionSelector(halvedDimension);

            //Sort values along current dimension
            var comparer = new VertexComparer(DimensionSelector);
            vertices.NthElement(halfIndex, comparer.Compare);

            //Split space at median along halved dimension
            float halfSpace = DimensionSelector(vertices[halfIndex].Position);

            //split value range into to sections, one for each child
            Range<PositionIndex> minChildVertices = vertices.GetRange(0, halfIndex);
            Range<PositionIndex> maxChildVertices = vertices.GetRange(halfIndex, vertices.Length - halfIndex);

            //Create bounding box halves along median
            BoundingBox minChildBox = BoundingBox.CreateContainer(minChildVertices.Select(v => v.Position));// boundingBox.GetMinHalfAlongDimension(halvedDimension, halfSpace);
            BoundingBox maxChildBox = BoundingBox.CreateContainer(maxChildVertices.Select(v => v.Position));// boundingBox.GetMaxHalfAlongDimension(halvedDimension, halfSpace);

            //If more vertices than what fits into to leafs, create more branches
            if (vertices.Length > configuration.MaximumPointsPerLeaf * 2)
            {
                Dimension nextDimension = GetNextDimension(halvedDimension);

                MinimumChild = new KdTreeBranch(minChildBox, minChildVertices, configuration, progressUpdater, nextDimension);
                MaximumChild = new KdTreeBranch(maxChildBox, maxChildVertices, configuration, progressUpdater, nextDimension);
            }
            else //create leafs
            {
                MinimumChild = new KdTreeLeaf(
                    minChildBox,
                    minChildVertices,
                    progressUpdater);

                MaximumChild = new KdTreeLeaf(
                    maxChildBox,
                    maxChildVertices,
                    progressUpdater);
            }

            //Add self and children count
            NodeCount = 1 + MinimumChild.NodeCount + MaximumChild.NodeCount;
            LeafCount = MinimumChild.LeafCount + MaximumChild.LeafCount;
        }

        protected virtual Dimension GetNextDimension(Dimension dimension)
        {
            return (Dimension)((int)(dimension + 1) % (int)Dimension.Count);
        }

        public override void FindInRadius(InRadiusQuery query)
        {

            if (MinimumChild.BoundingBox.GetMinimumDistance(query.SeachCenter) < query.SearchRadius)
            {
                MinimumChild.FindInRadius(query);
            }
            else
            {
                //If branch can be skipped, add progress for whole branch
                query.ProgressUpdater.UpdateAddOperation(MinimumChild.LeafCount);
            }

            if (MaximumChild.BoundingBox.GetMinimumDistance(query.SeachCenter) < query.SearchRadius)
            {
                MaximumChild.FindInRadius(query);
            }
            else
            {
                query.ProgressUpdater.UpdateAddOperation(MaximumChild.LeafCount);
            }
        }

        public override void FindNearestVertices(NearestVerticesQuery query)
        {
            //Enter child if still more more required to fill result or if distance is smaller than maxSearchRadius
            //which means that child potentially contains better points
            if (query.ResultSet.Count < query.PointAmount
                || MinimumChild.BoundingBox.GetMinimumDistance(query.SearchPosition) < query.MaxSearchRadius)
            {
                MinimumChild.FindNearestVertices(query);
            }
            else
            {
                //If branch can be skipped, add progress for whole branch
                query.ProgressUpdater.UpdateAddOperation(MinimumChild.LeafCount);
            }

            if (query.ResultSet.Count < query.PointAmount
                || MaximumChild.BoundingBox.GetMinimumDistance(query.SearchPosition) < query.MaxSearchRadius)
            {
                MaximumChild.FindNearestVertices(query);
            }
            else
            {
                query.ProgressUpdater.UpdateAddOperation(MaximumChild.LeafCount);
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

        public override void AddBoundingBoxes(List<BoundingBox> boundingBoxes)
        {
            base.AddBoundingBoxes(boundingBoxes);
            MinimumChild.AddBoundingBoxes(boundingBoxes);
            MaximumChild.AddBoundingBoxes(boundingBoxes);
        }

        private class VertexComparer : IComparer<PositionIndex>
        {
            public Func<Vector3, float> DimensionSelector { get; private set; }

            public VertexComparer(Func<Vector3, float> dimensionSelector)
            {
                DimensionSelector = dimensionSelector;
            }

            public int Compare(PositionIndex v1, PositionIndex v2)
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
