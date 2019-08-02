using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.KdTree
{
    class NearestVerticesQuery<TVertex>
    {
        public Vector3 SearchPosition;
        public int PointAmount;
        public float MaxSearchRadius;
        public SortedList<float, TVertex> ResultSet;
    }

    class InRadiusQuery<TVertex>
    {
        public readonly Vector3 SeachCenter;
        public readonly float SearchRadius;
        public readonly List<TVertex> ResultSet;
        public readonly float SearchRadiusSquared;
        public readonly KdTreeProgressUpdater ProgressUpdater;

        public InRadiusQuery(Vector3 seachCenter, float searchRadius, List<TVertex> resultSet, KdTreeProgressUpdater progressUpdater)
        {
            this.SeachCenter = seachCenter;
            this.SearchRadius = searchRadius;
            this.ResultSet = resultSet ?? throw new ArgumentNullException(nameof(resultSet));

            this.SearchRadiusSquared = searchRadius * searchRadius;

            this.ProgressUpdater = progressUpdater;
        }
    }
}
