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
        public ATreeNode MinimumChild { get; private set; }
        public ATreeNode MaximumChild { get; private set; }

        public override int ChildCount => 2;

        private readonly Func<Vector3, float> DimensionSelector;

        public KdTreeBranch(
            ATreeNode parent,
            BoundingBox boundingBox,
            Range<PositionIndex> vertices,
            TreeConfiguration configuration,
            OperationProgressUpdater progressUpdater,
            int depth,
            Dimension halvedDimension = Dimension.X)
            : base(parent, boundingBox, vertices.Length, depth)
        {
            int halfIndex = vertices.Length / 2;

            DimensionSelector = GetDimensionSelector(halvedDimension);

            //Sort values along current dimension
            var comparer = new PositionIndexComparer(DimensionSelector);
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

                MinimumChild = new KdTreeBranch(this, minChildBox, minChildVertices, configuration, progressUpdater, depth, nextDimension);
                MaximumChild = new KdTreeBranch(this, maxChildBox, maxChildVertices, configuration, progressUpdater, depth, nextDimension);
            }
            else //create leafs
            {
                MinimumChild = new TreeLeaf(this, minChildBox, minChildVertices, progressUpdater, depth + 1);

                MaximumChild = new TreeLeaf(this, maxChildBox, maxChildVertices, progressUpdater, depth + 1);
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

    }
}
