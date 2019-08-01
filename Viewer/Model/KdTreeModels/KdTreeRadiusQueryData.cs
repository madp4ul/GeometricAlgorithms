using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Cameras;
using GeometricAlgorithms.Domain.Drawables;
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
    public class KdTreeRadiusQueryData : IDrawable
    {
        private KdTree<GenericVertex> KdTree;

        private readonly ToggleableDrawable Drawable;

        public Vector3 QueryCenter { get; set; }

        public float Radius { get; private set; }


        private QueryCenterPoint QueryCenterDrawable { get; set; }
        private KdTreeQueryResult QueryResultDrawable { get; set; }


        public bool ShowQueryHelper { get => QueryCenterDrawable.EnableDraw; set => QueryCenterDrawable.EnableDraw = value; }
        public bool ShowQueryResult { get => QueryResultDrawable.EnableDraw; set => QueryResultDrawable.EnableDraw = value; }
        public Transformation Transformation { get; set; }

        public KdTreeRadiusQueryData(IDrawableFactoryProvider drawableFactoryProvider)
        {
            Transformation = Transformation.Identity;
            Radius = 0.1f;
            QueryCenterDrawable = new QueryCenterPoint(drawableFactoryProvider);

            QueryResultDrawable = new KdTreeQueryResult(drawableFactoryProvider);

            Drawable = new ToggleableDrawable(new CompositeDrawable(QueryCenterDrawable, QueryResultDrawable));
        }


        public void Reset(KdTree<GenericVertex> kdTree)
        {
            KdTree = kdTree;

            QueryCenterDrawable.Reset();
        }

        public void HideAll()
        {
            ShowQueryHelper = false;
            ShowQueryResult = false;
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

        public void Draw(ACamera camera)
        {
            //Refresh query center based on camera position - can only happen in draw
            float centerDistance = Radius + 0.1f;
            QueryCenter = camera.Position + (camera.Forward.Normalized() * centerDistance);
            QueryCenterDrawable.SetPosition(QueryCenter);

            QueryCenterDrawable.Draw(camera);
            QueryResultDrawable.Draw(camera);
        }

        public void Dispose()
        {
            Drawable.Dispose();
        }
    }
}
