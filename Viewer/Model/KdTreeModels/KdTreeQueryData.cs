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
    abstract class KdTreeQueryData : ToggleableDrawable
    {
        protected readonly IDrawableFactoryProvider DrawableFactoryProvider;

        protected KdTree<GenericVertex> KdTree { get; private set; }

        public Vector3 QueryCenter { get; set; }
        public float QueryCenterDistance { get; set; }


        public ToggleableDrawable QueryCenterDrawable { get; set; }
        public KdTreeQueryResult QueryResultHighlights { get; set; }


        protected readonly CompositeDrawable DrawableList;
        public KdTreeQueryData(IDrawableFactoryProvider drawableFactoryProvider)
        {
            DrawableFactoryProvider = drawableFactoryProvider;

            EnableDraw = false;
            QueryCenterDrawable = new ToggleableDrawable(
                drawableFactoryProvider.DrawableFactory.CreatePointCloud(new[] { Vector3.Zero }, 50));

            QueryResultHighlights = new KdTreeQueryResult(drawableFactoryProvider);

            Drawable = DrawableList = new CompositeDrawable(QueryCenterDrawable, QueryResultHighlights);
        }

        public void Reset(KdTree<GenericVertex> kdTree)
        {
            KdTree = kdTree;

            if (QueryResultHighlights.EnableDraw)
            {
                CalculateQueryResult();
            }
        }

        public abstract void CalculateQueryResult();

        public override void Draw(ICamera camera)
        {
            QueryCenterDrawable.Transformation.Position = camera.Position + (camera.Forward.Normalized() * QueryCenterDistance);

            base.Draw(camera);
        }
    }
}
