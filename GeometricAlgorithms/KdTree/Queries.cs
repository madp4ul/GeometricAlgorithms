using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
        public Vector3 SeachCenter;
        public float SearchRadius;
        public List<TVertex> ResultSet;
        public float SearchRadiusSquared;

        public InRadiusQuery(Vector3 seachCenter, float searchRadius, List<TVertex> resultSet)
        {
            this.SeachCenter = seachCenter;
            this.SearchRadius = searchRadius;
            this.ResultSet = resultSet ?? throw new ArgumentNullException(nameof(resultSet));

            this.SearchRadiusSquared = searchRadius * searchRadius;
        }
    }
}
