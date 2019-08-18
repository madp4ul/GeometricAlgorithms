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
        private readonly ATreeNode[,,] Children;

        public readonly Vector3 Middle;

        public OctreeBranch(
            BoundingBox boundingBox,
            Range<PositionIndex> vertices,
            TreeConfiguration configuration,
            OperationProgressUpdater progressUpdater,
            int depth)
            : base(boundingBox, vertices.Length, depth)
        {
            Children = new ATreeNode[2, 2, 2];
            var childData = new ChildData[2, 2, 2];

            //Sort by x and find index of first element of bigger half
            //then each half by y and then each of the 4 halfs by z
            //unlike kdtree for each child individually create a branch or leaf!
            Middle = boundingBox.Minimum + (boundingBox.Diagonal / 2f);

            firstSplit();

            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < 2; y++)
                {
                    for (int z = 0; z < 2; z++)
                    {
                        ChildData data = childData[x, y, z];
                        var child = data.IsBranch
                            ? new OctreeBranch(data.BoundingBox, data.Vertices, configuration, progressUpdater, depth + 1) as ATreeNode
                            : new TreeLeaf(data.BoundingBox, data.Vertices, progressUpdater, depth + 1) as ATreeNode;

                        Children[x, y, z] = child;
                    }
                }
            }

            //Add self and children count
            NodeCount = 1 + GetChildren().Sum(c => c.NodeCount);
            LeafCount = GetChildren().Sum(c => c.LeafCount);

            //////////////Local functions below            
            void splitVertices(
            Range<PositionIndex> verticesToSplit,
            BoundingBox boundingBoxOfVerticesToSplit,
            Func<Vector3, float> dimensionSelector,
            Dimension dimension,
            int[] dimensionOffset,
            Action<Range<PositionIndex>, BoundingBox, int[]> nextSplit)
            {
                float middleDimension = dimensionSelector(Middle);

                verticesToSplit.Sort(new PositionIndexComparer(dimensionSelector));
                int offsetOfBiggerHalf = verticesToSplit
                    .Select(v => v.Position)
                    .FirstIndexOrDefault(pos => dimensionSelector(pos) > middleDimension)
                    ?? verticesToSplit.Length;

                Range<PositionIndex> minHalf = verticesToSplit.GetRange(0, offsetOfBiggerHalf);
                Range<PositionIndex> maxHalf = verticesToSplit.GetRange(offsetOfBiggerHalf, verticesToSplit.Length - offsetOfBiggerHalf);

                //Create bounding box halves along median
                BoundingBox minChildBox = configuration.MinimizeContainers
                    ? BoundingBox.CreateContainer(minHalf.Select(v => v.Position))
                    : boundingBoxOfVerticesToSplit.GetMinHalfAlongDimension(dimension, middleDimension);
                BoundingBox maxChildBox = configuration.MinimizeContainers
                    ? BoundingBox.CreateContainer(maxHalf.Select(v => v.Position))
                    : boundingBoxOfVerticesToSplit.GetMaxHalfAlongDimension(dimension, middleDimension);

                dimensionOffset[(int)dimension] = 0;
                nextSplit(minHalf, minChildBox, dimensionOffset);

                dimensionOffset[(int)dimension] = 1;
                nextSplit(maxHalf, maxChildBox, dimensionOffset);
            }

            void firstSplit()
            {
                float selectDimension(Vector3 v) => v.X;
                Dimension dimension = Dimension.X;

                splitVertices(vertices, boundingBox, selectDimension, dimension, new int[3], secondSplit);
            }

            void secondSplit(Range<PositionIndex> verticesHalf, BoundingBox splitBoundingBox, int[] dimensionOffset)
            {
                float selectDimension(Vector3 v) => v.Y;
                Dimension dimension = Dimension.Y;

                splitVertices(verticesHalf, splitBoundingBox, selectDimension, dimension, dimensionOffset, thirdSplit);
            }

            void thirdSplit(Range<PositionIndex> verticesHalf, BoundingBox splitBoundingBox, int[] dimensionOffset)
            {
                float selectDimension(Vector3 v) => v.Z;
                Dimension dimension = Dimension.Z;

                splitVertices(verticesHalf, splitBoundingBox, selectDimension, dimension, dimensionOffset, selectChild);
            }

            void selectChild(Range<PositionIndex> verticesHalf, BoundingBox splitBoundingBox, int[] dimensionOffset)
            {
                var child = createChild(verticesHalf, splitBoundingBox);

                childData[dimensionOffset[0], dimensionOffset[1], dimensionOffset[2]] = child;
            }

            ChildData createChild(Range<PositionIndex> verticesHalf, BoundingBox splitBoundingBox)
            {
                return new ChildData(
                   isBranch: configuration.MaximumPointsPerLeaf < verticesHalf.Length,
                   boundingBox: splitBoundingBox,
                   vertices: verticesHalf);
            }
        }

        public override IEnumerable<ATreeNode> GetChildren()
        {
            yield return Children[0, 0, 0];
            yield return Children[0, 0, 1];
            yield return Children[0, 1, 0];
            yield return Children[0, 1, 1];
            yield return Children[1, 0, 0];
            yield return Children[1, 0, 1];
            yield return Children[1, 1, 0];
            yield return Children[1, 1, 1];
        }

        public ATreeNode this[int x, int y, int z]
        {
            get => Children[x, y, z];
        }

        private class ChildData
        {
            public readonly bool IsBranch;
            public readonly BoundingBox BoundingBox;
            public readonly Range<PositionIndex> Vertices;

            public ChildData(bool isBranch, BoundingBox boundingBox, Range<PositionIndex> vertices)
            {
                IsBranch = isBranch;
                BoundingBox = boundingBox ?? throw new ArgumentNullException(nameof(boundingBox));
                Vertices = vertices ?? throw new ArgumentNullException(nameof(vertices));
            }
        }
    }
}
