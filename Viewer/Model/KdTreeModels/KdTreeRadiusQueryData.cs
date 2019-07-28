using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.VertexTypes;
using GeometricAlgorithms.KdTree;
using GeometricAlgorithms.MonoGame.Forms.Cameras;
using GeometricAlgorithms.MonoGame.Forms.Drawables;
using GeometricAlgorithms.Viewer.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Viewer.Model.KdTreeModels
{
    public class KdTreeRadiusQueryData : ToggleableDrawable
    {
        private KdTree<GenericVertex> KdTree;

        public Vector3 QueryCenter { get; set; }

        public float Radius { get; private set; }


        public QueryCenterPoint QueryCenterDrawable { get; set; }
        public KdTreeQueryResult QueryResultDrawable { get; set; }

        public KdTreeRadiusQueryData(IDrawableFactoryProvider drawableFactoryProvider)
        {
            Radius = 0.1f;
            EnableDraw = true;

            QueryCenterDrawable = new QueryCenterPoint(drawableFactoryProvider);

            QueryResultDrawable = new KdTreeQueryResult(drawableFactoryProvider);

            Drawable = new CompositeDrawable(QueryCenterDrawable, QueryResultDrawable);
        }


        public void Reset(KdTree<GenericVertex> kdTree)
        {
            KdTree = kdTree;

            QueryCenterDrawable.Reset();
        }

        public void SetRadius(float radius)
        {
            Radius = radius;
        }

        public void CalculateQueryResult()
        {
            var vertices = KdTree.FindInRadius(QueryCenter, Radius);

            QueryResultDrawable.Reset(vertices);
        }

        public override void Draw(ICamera camera)
        {
            float centerDistance = Radius + 0.1f;

            QueryCenterDrawable.SetPosition(camera.Position + (camera.Forward.Normalized() * centerDistance));

            base.Draw(camera);
        }
    }
}
