using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.KdTree
{
    public class KdTree<TVertex> : I3DQueryable<TVertex> where TVertex : IVertex
    {
        private readonly KdTreeNode<TVertex> Root;

        public IReadOnlyCollection<TVertex> Vertices { get; set; }

        public KdTree(TVertex[] vertices, KdTreeConfiguration configuration = null)
        {
            if (configuration == null)
            {
                configuration = KdTreeConfiguration.Default;
            }

            Vertices = vertices;
            var range = Range<TVertex>.FromArray(vertices, 0, vertices.Length);
            var rootBoundingBox = BoundingBox.CreateContainer(vertices);

            if (vertices.Length > configuration.MaximumPointsPerLeaf)
            {
                Root = new KdTreeBranch<TVertex>(rootBoundingBox, range, configuration);
            }
            else
            {
                Root = new KdTreeLeaf<TVertex>(rootBoundingBox, range);
            }
        }

        public KdTree<TVertex> Reshape(KdTreeConfiguration configuration)
        {
            return new KdTree<TVertex>(Vertices.ToArray(), configuration);
        }

        public List<TVertex> FindInRadius(Vector3 seachCenter, float searchRadius)
        {
            var resultList = new List<TVertex>();

            Root.FindInRadius(new InRadiusQuery<TVertex>(seachCenter, searchRadius, resultList));

            return resultList;
        }

        public SortedList<float, TVertex> FindNearestVertices(Vector3 searchPosition, int pointAmount)
        {
            var resultSet = new SortedList<float, TVertex>(new DistanceComparer());

            Root.FindNearestVertices(new NearestVerticesQuery<TVertex>
            {
                MaxSearchRadius = float.PositiveInfinity,
                PointAmount = pointAmount,
                SearchPosition = searchPosition,
                ResultSet = resultSet
            });

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
