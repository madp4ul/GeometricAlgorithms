using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.KdTree
{
    public class KdTree<TVertex> where TVertex : IVertex
    {
        private readonly KdTreeNode<TVertex> Root;

        public IReadOnlyCollection<TVertex> Vertices { get; set; }

        public KdTree(TVertex[] vertices, KdTreeConfiguration configuration = null, IProgressUpdater progressUpdater = null)
        {
            if (configuration == null)
            {
                configuration = KdTreeConfiguration.Default;
            }

            Vertices = vertices;
            var range = Range<TVertex>.FromArray(vertices, 0, vertices.Length);
            var rootBoundingBox = BoundingBox.CreateContainer(vertices);

            var updater = new KdTreeProgressUpdater(
                progressUpdater,
                (2 * vertices.Length) / configuration.MaximumPointsPerLeaf,
                "Building Kd-Tree");

            if (vertices.Length > configuration.MaximumPointsPerLeaf)
            {
                Root = new KdTreeBranch<TVertex>(rootBoundingBox, range, configuration, updater);
            }
            else
            {
                Root = new KdTreeLeaf<TVertex>(rootBoundingBox, range, updater);
            }

            updater.IsCompleted();
        }

        public KdTree<TVertex> Reshape(KdTreeConfiguration configuration)
        {
            return new KdTree<TVertex>(Vertices.ToArray(), configuration);
        }

        public List<TVertex> FindInRadius(
            Vector3 seachCenter,
            float searchRadius,
            IProgressUpdater progressUpdater = null)
        {
            var resultList = new List<TVertex>();

            var kdTreeProgress = new KdTreeProgressUpdater(progressUpdater, Root.LeafCount, "Looking for vertices in radius");

            Root.FindInRadius(new InRadiusQuery<TVertex>(seachCenter, searchRadius, resultList, kdTreeProgress));

            kdTreeProgress.IsCompleted();

            return resultList;
        }

        public SortedList<float, TVertex> FindNearestVertices(
            Vector3 searchPosition,
            int pointAmount,
            IProgressUpdater progressUpdater = null)
        {
            var resultSet = new SortedList<float, TVertex>(new DistanceComparer());

            var kdTreeProgress = new KdTreeProgressUpdater(progressUpdater, Root.LeafCount, "Looking for nearest points");

            Root.FindNearestVertices(new NearestVerticesQuery<TVertex>(
                searchPosition,
                pointAmount,
                resultSet,
                kdTreeProgress
            ));

            kdTreeProgress.IsCompleted();

            return resultSet;
        }

        public List<BoundingBox> GetBoundingBoxes()
        {
            var boxes = new List<BoundingBox>();
            Root.AddBoundingBoxes(boxes);
            return boxes;
        }
    }
}
