using GeometricAlgorithms.Viewer.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Viewer.Model.KdTreeModels
{
    class KdTreeRadiusQueryData : KdTreeQueryData
    {
        public KdTreeRadiusQueryData(IDrawableFactoryProvider drawableFactoryProvider) : base(drawableFactoryProvider)
        {
            Radius = 0.1f;
        }

        public float Radius { get; private set; }

        public void SetRadius(float radius)
        {
            Radius = radius;
            QueryCenterDistance = radius + 0.1f;
        }

        public override void CalculateQueryResult()
        {
            var vertices = KdTree.FindInRadius(QueryCenter, Radius);

            QueryResultHighlights.Reset(vertices);
        }
    }
}
