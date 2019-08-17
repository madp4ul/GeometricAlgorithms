using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.MeshQuerying
{
    class KdTreeBranch : ATreeBranch
    {
        public ATreeNode MinimumChild { get; set; }
        public ATreeNode MaximumChild { get; set; }
        public override int NodeCount { get; protected set; }
        public override int LeafCount { get; protected set; }

        private readonly Func<Vector3, float> DimensionSelector;

        public KdTreeBranch(
            BoundingBox boundingBox,
            Range<PositionIndex> vertices,
            KdTreeConfiguration configuration,
            OperationProgressUpdater progressUpdater,
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
            BoundingBox minChildBox = configuration.MinimizeContainers
                ? BoundingBox.CreateContainer(minChildVertices.Select(v => v.Position))
                : boundingBox.GetMinHalfAlongDimension(halvedDimension, halfSpace);
            BoundingBox maxChildBox = configuration.MinimizeContainers
                ? BoundingBox.CreateContainer(maxChildVertices.Select(v => v.Position))
                : boundingBox.GetMaxHalfAlongDimension(halvedDimension, halfSpace);

            //If more vertices than what fits into to leafs, create more branches
            if (vertices.Length > configuration.MaximumPointsPerLeaf * 2)
            {
                Dimension nextDimension = GetNextDimension(halvedDimension);

                MinimumChild = new KdTreeBranch(minChildBox, minChildVertices, configuration, progressUpdater, nextDimension);
                MaximumChild = new KdTreeBranch(maxChildBox, maxChildVertices, configuration, progressUpdater, nextDimension);
            }
            else //create leafs
            {
                MinimumChild = new TreeLeaf(
                    minChildBox,
                    minChildVertices,
                    progressUpdater);

                MaximumChild = new TreeLeaf(
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

        public override IEnumerable<ATreeNode> GetChildren()
        {
            yield return MinimumChild;
            yield return MaximumChild;
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
