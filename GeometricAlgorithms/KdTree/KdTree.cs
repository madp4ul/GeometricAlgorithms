using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.KdTree
{
    public class KdTree
    {
        private readonly KdTreeNode Root;

        public Mesh Model { get; set; }

        public KdTree(Mesh model, KdTreeConfiguration configuration = null, IProgressUpdater progressUpdater = null)
        {
            if (configuration == null)
            {
                configuration = KdTreeConfiguration.Default;
            }

            Model = model;

            //Needs position mapping to preserve original indices because 
            //they are necessary to find the other related data in the model
            var positionMapping = model.Positions.Select((vector, index) => new VertexPosition(vector, index)).ToArray();

            var range = Range<VertexPosition>.FromArray(positionMapping, 0, model.Positions.Length);
            var rootBoundingBox = BoundingBox.CreateContainer(model.Positions);

            var updater = new KdTreeProgressUpdater(
                progressUpdater,
                (2 * model.Positions.Length) / configuration.MaximumPointsPerLeaf,
                "Building Kd-Tree");

            if (model.Positions.Length > configuration.MaximumPointsPerLeaf)
            {
                Root = new KdTreeBranch(rootBoundingBox, range, configuration, updater);
            }
            else
            {
                Root = new KdTreeLeaf(rootBoundingBox, range, updater);
            }

            updater.IsCompleted();
        }

        public KdTree Reshape(KdTreeConfiguration configuration)
        {
            return new KdTree(Model, configuration);
        }

        public List<int> FindInRadius(
            Vector3 seachCenter,
            float searchRadius,
            IProgressUpdater progressUpdater = null)
        {
            var resultList = new List<int>();

            var kdTreeProgress = new KdTreeProgressUpdater(progressUpdater, Root.LeafCount, "Looking for vertices in radius");

            Root.FindInRadius(new InRadiusQuery(seachCenter, searchRadius, resultList, kdTreeProgress));

            kdTreeProgress.IsCompleted();

            return resultList;
        }

        public SortedList<float, int> FindNearestVertices(
            Vector3 searchPosition,
            int pointAmount,
            IProgressUpdater progressUpdater = null)
        {
            var resultSet = new SortedList<float, int>(new DistanceComparer());

            var kdTreeProgress = new KdTreeProgressUpdater(progressUpdater, Root.LeafCount, "Looking for nearest points");

            Root.FindNearestVertices(new NearestVerticesQuery(
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
