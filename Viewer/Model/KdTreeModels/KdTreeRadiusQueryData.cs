using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Cameras;
using GeometricAlgorithms.Domain.Drawables;
using GeometricAlgorithms.Domain.Tasks;
using GeometricAlgorithms.KdTree;
using GeometricAlgorithms.MonoGame.Forms.Cameras;
using GeometricAlgorithms.MonoGame.Forms.Drawables;
using GeometricAlgorithms.Viewer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Viewer.Model.KdTreeModels
{
    public class KdTreeRadiusQueryData : IDrawable
    {
        private readonly IFuncExecutor FuncExecutor;
        private readonly ToggleableDrawable Drawable;

        private KdTree.KdTree KdTree;

        public Vector3 QueryCenter { get; set; }
        public float Radius { get; private set; }

        public bool IsCalculating { get; private set; }
        public bool QueryHasChangedSinceLastCalculation { get; private set; }

        private QueryCenterPoint QueryCenterDrawable { get; set; }
        private KdTreeQueryResult QueryResultDrawable { get; set; }


        public bool ShowQueryHelper { get => QueryCenterDrawable.EnableDraw; set => QueryCenterDrawable.EnableDraw = value; }
        public bool ShowQueryResult { get => QueryResultDrawable.EnableDraw; set => QueryResultDrawable.EnableDraw = value; }
        public bool CanQuery => KdTree != null;

        public Transformation Transformation { get; set; }

        public KdTreeRadiusQueryData(IDrawableFactoryProvider drawableFactoryProvider, IFuncExecutor funcExecutor)
        {
            FuncExecutor = funcExecutor;

            Transformation = Transformation.Identity;
            Radius = 0.1f;
            QueryCenterDrawable = new QueryCenterPoint(drawableFactoryProvider);

            QueryResultDrawable = new KdTreeQueryResult(drawableFactoryProvider);

            Drawable = new ToggleableDrawable(new CompositeDrawable(QueryCenterDrawable, QueryResultDrawable));
        }


        public void Reset(KdTree.KdTree kdTree)
        {
            KdTree = kdTree;

            QueryCenterDrawable.Reset();
            QueryResultDrawable.Reset();
        }

        public void HideAll()
        {
            ShowQueryHelper = false;
            ShowQueryResult = false;
        }

        public void SetRadius(float radius)
        {
            Radius = radius;
            QueryHasChangedSinceLastCalculation = true;
        }

        public void CalculateQueryResult()
        {
            if (!CanQuery)
            {
                throw new InvalidOperationException("Can not query before tree generated");
            }

            IsCalculating = true;
            QueryHasChangedSinceLastCalculation = false;
            var radiusQuery = FuncExecutor.Execute((progress) => KdTree.FindInRadius(QueryCenter, Radius, progress));

            radiusQuery.GetResult((vertexIndices) =>
            {
                QueryResultDrawable.Reset(vertexIndices.Select(i => KdTree.Model.Positions[i]));
                IsCalculating = false;
            });
        }

        public void Draw(ACamera camera)
        {
            //Refresh query center based on camera position - can only happen in draw
            float centerDistance = 0.2f;
            Vector3 newQueryCenter = camera.Position + (camera.Forward.Normalized() * centerDistance);

            if (!newQueryCenter.Equals(QueryCenter))
            {
                QueryHasChangedSinceLastCalculation = true;
                QueryCenter = newQueryCenter;
                QueryCenterDrawable.SetPosition(QueryCenter);
            }

            QueryCenterDrawable.Draw(camera);
            QueryResultDrawable.Draw(camera);
        }

        public void Dispose()
        {
            Drawable.Dispose();
        }
    }
}
