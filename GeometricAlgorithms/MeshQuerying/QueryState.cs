using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.MeshQuerying
{
    class NearestVerticesQuery
    {
        public readonly Vector3 SearchPosition;
        public readonly int PointAmount;
        public readonly SortedList<float, PositionIndex> ResultSet;
        public readonly KdTreeProgressUpdater ProgressUpdater;

        public float MaxSearchRadius = float.PositiveInfinity;

        public NearestVerticesQuery(
            Vector3 searchPosition,
            int pointAmount,
            SortedList<float, PositionIndex> resultSet,
            KdTreeProgressUpdater progressUpdater)
        {
            SearchPosition = searchPosition;
            PointAmount = pointAmount;
            ResultSet = resultSet ?? throw new ArgumentNullException(nameof(resultSet));
            ProgressUpdater = progressUpdater ?? throw new ArgumentNullException(nameof(progressUpdater));
        }
    }

    class InRadiusQuery
    {
        public readonly Vector3 SeachCenter;
        public readonly float SearchRadius;
        public readonly List<PositionIndex> ResultSet;
        public readonly float SearchRadiusSquared;
        public readonly KdTreeProgressUpdater ProgressUpdater;

        public InRadiusQuery(Vector3 seachCenter, float searchRadius, List<PositionIndex> resultSet, KdTreeProgressUpdater progressUpdater)
        {
            this.SeachCenter = seachCenter;
            this.SearchRadius = searchRadius;
            this.ResultSet = resultSet ?? throw new ArgumentNullException(nameof(resultSet));

            this.SearchRadiusSquared = searchRadius * searchRadius;

            this.ProgressUpdater = progressUpdater;
        }
    }
}
