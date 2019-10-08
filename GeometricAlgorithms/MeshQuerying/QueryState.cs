using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Tasks;
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
        public readonly PriorityQueue<PositionIndexDistance> ResultSet;
        public readonly OperationProgressUpdater ProgressUpdater;

        public float MaxSearchRadius = float.PositiveInfinity;

        public NearestVerticesQuery(
            Vector3 searchPosition,
            int pointAmount,
            PriorityQueue<PositionIndexDistance> resultSet,
            OperationProgressUpdater progressUpdater)
        {
            SearchPosition = searchPosition;
            PointAmount = pointAmount;
            ResultSet = resultSet ?? throw new ArgumentNullException(nameof(resultSet));
            ProgressUpdater = progressUpdater ?? throw new ArgumentNullException(nameof(progressUpdater));
        }
    }

    public class PositionIndexDistance : IComparable<PositionIndexDistance>
    {
        public readonly float Distance;
        public readonly PositionIndex PositionIndex;

        public PositionIndexDistance(float distance, PositionIndex positionIndex)
        {
            Distance = distance;
            PositionIndex = positionIndex;
        }

        public int CompareTo(PositionIndexDistance other)
        {
            float diff = Distance - other.Distance;
            //order descending
            return diff > 0 ? -1 : (diff < 0 ? 1 : 0);
        }
    }

    class InRadiusQuery
    {
        public readonly Vector3 SeachCenter;
        public readonly float SearchRadius;
        public readonly List<PositionIndex> ResultSet;
        public readonly float SearchRadiusSquared;
        public readonly OperationProgressUpdater ProgressUpdater;

        public InRadiusQuery(Vector3 seachCenter, float searchRadius, List<PositionIndex> resultSet, OperationProgressUpdater progressUpdater)
        {
            this.SeachCenter = seachCenter;
            this.SearchRadius = searchRadius;
            this.ResultSet = resultSet ?? throw new ArgumentNullException(nameof(resultSet));

            this.SearchRadiusSquared = searchRadius * searchRadius;

            this.ProgressUpdater = progressUpdater;
        }
    }
}
