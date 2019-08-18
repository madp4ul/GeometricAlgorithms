using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Tasks;
using GeometricAlgorithms.Extensions;

namespace GeometricAlgorithms.MeshQuerying
{
    internal class OctreeBranch : ATreeBranch
    {
        public ATreeNode Child000 { get; private set; }
        public ATreeNode Child001 { get; private set; }
        public ATreeNode Child010 { get; private set; }
        public ATreeNode Child011 { get; private set; }
        public ATreeNode Child100 { get; private set; }
        public ATreeNode Child101 { get; private set; }
        public ATreeNode Child110 { get; private set; }
        public ATreeNode Child111 { get; private set; }

        public readonly Vector3 Middle;


        public OctreeBranch(
            BoundingBox boundingBox,
            Range<PositionIndex> vertices,
            TreeConfiguration configuration,
            OperationProgressUpdater progressUpdater)
            : base(boundingBox, vertices.Length)
        {
            //TODO
            //Sort by x and find index of first element of bigger half
            //then each half by y and then each of the 4 halfs by z
            //unlike kdtree for each child individually create a branch or leaf!

            Middle = boundingBox.Minimum + (boundingBox.Diagonal / 2f);

            SplitVerticesByX(configuration, vertices, boundingBox, new int[3], progressUpdater);




            //Add self and children count
            NodeCount = 1 + GetChildren().Sum(c => c.NodeCount);
            LeafCount = GetChildren().Sum(c => c.LeafCount);
        }

        private void SplitVerticesByX(
            TreeConfiguration configuration,
            Range<PositionIndex> vertices,
            BoundingBox boundingBox,
            int[] dimensionOffset,
            OperationProgressUpdater progressUpdater)
        {
            float selectDimension(Vector3 v) => v.X;
            Dimension dimension = Dimension.X;

            SplitVertices(configuration, vertices, boundingBox, selectDimension, dimension, dimensionOffset, progressUpdater, SplitVerticesByY);
        }

        private void SplitVerticesByY(
            TreeConfiguration configuration,
            Range<PositionIndex> vertices,
            BoundingBox boundingBox,
            int[] dimensionOffset,
            OperationProgressUpdater progressUpdater)
        {
            float selectDimension(Vector3 v) => v.Y;
            Dimension dimension = Dimension.Y;

            SplitVertices(configuration, vertices, boundingBox, selectDimension, dimension, dimensionOffset, progressUpdater, SplitVerticesByZ);
        }

        private void SplitVerticesByZ(
            TreeConfiguration configuration,
            Range<PositionIndex> vertices, BoundingBox boundingBox,
            int[] dimensionOffset,
            OperationProgressUpdater progressUpdater)
        {
            float selectDimension(Vector3 v) => v.Z;
            Dimension dimension = Dimension.Z;

            SplitVertices(configuration, vertices, boundingBox, selectDimension, dimension, dimensionOffset, progressUpdater, SelectChild);
        }

        private void SelectChild(
            TreeConfiguration configuration,
            Range<PositionIndex> vertices,
            BoundingBox boundingBox,
            int[] dimensionOffset,
            OperationProgressUpdater progressUpdater)
        {
            var child = CreateChild(configuration, vertices, boundingBox, progressUpdater);

            if (dimensionOffset[(int)Dimension.X] == 0)
            {
                if (dimensionOffset[(int)Dimension.Y] == 0)
                {
                    if (dimensionOffset[(int)Dimension.Z] == 0)
                    {
                        Child000 = child;
                    }
                    else
                    {
                        //TODO make all this nice
                    }
                }
                else
                {

                }
            }
            else
            {

            }
        }

        private ATreeNode CreateChild(TreeConfiguration configuration, Range<PositionIndex> vertices, BoundingBox boundingBox, OperationProgressUpdater progressUpdater)
        {
            if (configuration.MaximumPointsPerLeaf < vertices.Length)
            {
                return new OctreeBranch(boundingBox, vertices, configuration, progressUpdater);
            }
            else
            {
                return new TreeLeaf(boundingBox, vertices, progressUpdater);
            }
        }

        private void SplitVertices(
            TreeConfiguration configuration,
            Range<PositionIndex> vertices,
            BoundingBox boundingBox,
            Func<Vector3, float> dimensionSelector,
            Dimension dimension,
            int[] dimensionOffset,
            OperationProgressUpdater progressUpdater,
            Action<TreeConfiguration, Range<PositionIndex>, BoundingBox, int[], OperationProgressUpdater> nextSplit)
        {
            vertices.Sort(new PositionIndexComparer(dimensionSelector));
            int offsetOfBiggerHalf = vertices
                .Select(v => v.Position)
                .FirstIndexOrDefault(pos => dimensionSelector(pos) > Middle.X)
                ?? vertices.Length;

            Range<PositionIndex> minHalf = vertices.GetRange(0, offsetOfBiggerHalf);
            Range<PositionIndex> maxHalf = vertices.GetRange(offsetOfBiggerHalf, vertices.Length - offsetOfBiggerHalf);

            //Create bounding box halves along median
            BoundingBox minChildBox = configuration.MinimizeContainers
                ? BoundingBox.CreateContainer(minHalf.Select(v => v.Position))
                : boundingBox.GetMinHalfAlongDimension(dimension, dimensionSelector(Middle));
            BoundingBox maxChildBox = configuration.MinimizeContainers
                ? BoundingBox.CreateContainer(maxHalf.Select(v => v.Position))
                : boundingBox.GetMaxHalfAlongDimension(dimension, dimensionSelector(Middle));

            nextSplit(configuration, minHalf, minChildBox, dimensionOffset, progressUpdater);

            dimensionOffset[(int)dimension] = 1;

            nextSplit(configuration, maxHalf, maxChildBox, dimensionOffset, progressUpdater);

        }

        public override IEnumerable<ATreeNode> GetChildren()
        {
            yield return Child000;
            yield return Child001;
            yield return Child010;
            yield return Child011;
            yield return Child100;
            yield return Child101;
            yield return Child110;
            yield return Child111;
        }
    }
}
