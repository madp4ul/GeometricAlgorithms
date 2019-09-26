using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree2.PointPartitioning
{
    class OctreeNode
    {
        public readonly Vector3 Middle;

        public OctreeNode[,,] Children { get; private set; }
        public bool HasChildren => Children != null;

        private readonly Range<PositionIndex> Vertices;
        public int VerticesCount => Vertices.Length;

        public readonly BoundingBox BoundingBox;

        public OctreeNode(Range<PositionIndex> vertices, BoundingBox boundingBox)
        {
            Vertices = vertices ?? throw new ArgumentNullException(nameof(vertices));
            BoundingBox = boundingBox ?? throw new ArgumentNullException(nameof(boundingBox));

            Middle = boundingBox.Minimum + (boundingBox.Diagonal / 2f);
        }

        public void CreateChildren()
        {
            if (HasChildren)
            {
                throw new InvalidOperationException();
            }

            Children = new OctreeNode[2, 2, 2];
            var childData = new ChildData[2, 2, 2];

            //Sort by x and find index of first element of bigger half
            //then each half by y and then each of the 4 halfs by z
            //unlike kdtree for each child individually create a branch or leaf!

            firstSplit();

            //////////////Local functions below            

            void firstSplit()
            {
                float selectDimension(Vector3 v) => v.X;
                Dimension dimension = Dimension.X;

                splitVertices(Vertices, BoundingBox, selectDimension, dimension, new int[3], next: secondSplit);
            }

            void secondSplit(Range<PositionIndex> verticesHalf, BoundingBox splitBoundingBox, int[] dimensionOffset)
            {
                float selectDimension(Vector3 v) => v.Y;
                Dimension dimension = Dimension.Y;

                splitVertices(verticesHalf, splitBoundingBox, selectDimension, dimension, dimensionOffset, next: thirdSplit);
            }

            void thirdSplit(Range<PositionIndex> verticesHalf, BoundingBox splitBoundingBox, int[] dimensionOffset)
            {
                float selectDimension(Vector3 v) => v.Z;
                Dimension dimension = Dimension.Z;

                splitVertices(verticesHalf, splitBoundingBox, selectDimension, dimension, dimensionOffset, next: createChild);
            }

            void createChild(Range<PositionIndex> verticesHalf, BoundingBox splitBoundingBox, int[] dimensionOffset)
            {
                Children[dimensionOffset[0], dimensionOffset[1], dimensionOffset[2]] = new OctreeNode(verticesHalf, splitBoundingBox);
            }

            void splitVertices(
                Range<PositionIndex> verticesToSplit,
                BoundingBox boundingBoxOfVerticesToSplit,
                Func<Vector3, float> dimensionSelector,
                Dimension dimension,
                int[] dimensionOffset,
                Action<Range<PositionIndex>, BoundingBox, int[]> next)
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
                BoundingBox minChildBox = boundingBoxOfVerticesToSplit.GetMinHalfAlongDimension(dimension, middleDimension);
                BoundingBox maxChildBox = boundingBoxOfVerticesToSplit.GetMaxHalfAlongDimension(dimension, middleDimension);

                dimensionOffset[(int)dimension] = 0;
                next(minHalf, minChildBox, dimensionOffset);

                dimensionOffset[(int)dimension] = 1;
                next(maxHalf, maxChildBox, dimensionOffset);
            }
        }
        private class ChildData
        {
            public readonly BoundingBox BoundingBox;
            public readonly Range<PositionIndex> Vertices;

            public ChildData(BoundingBox boundingBox, Range<PositionIndex> vertices)
            {
                BoundingBox = boundingBox ?? throw new ArgumentNullException(nameof(boundingBox));
                Vertices = vertices ?? throw new ArgumentNullException(nameof(vertices));
            }
        }
    }
}
