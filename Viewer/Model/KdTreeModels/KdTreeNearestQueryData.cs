using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Cameras;
using GeometricAlgorithms.Domain.Drawables;
using GeometricAlgorithms.Domain.Tasks;
using GeometricAlgorithms.KdTree;
using GeometricAlgorithms.Viewer.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Viewer.Model.KdTreeModels
{
    public class KdTreeNearestQueryData : IDrawable
    {
        private readonly IFuncExecutor FuncExecutor;
        private readonly ToggleableDrawable Drawable;

        private KdTree<VertexNormal> KdTree;

        public Vector3 QueryCenter { get; set; }
        public int PointCount { get; private set; }

        public bool IsCalculating { get; private set; }
        public bool QueryHasChangedSinceLastCalculation { get; private set; }

        private QueryCenterPoint QueryCenterDrawable { get; set; }
        private KdTreeQueryResult QueryResultDrawable { get; set; }

        public bool ShowQueryHelper { get => QueryCenterDrawable.EnableDraw; set => QueryCenterDrawable.EnableDraw = value; }
        public bool ShowQueryResult { get => QueryResultDrawable.EnableDraw; set => QueryResultDrawable.EnableDraw = value; }
        public bool CanQuery => KdTree != null;

        public Transformation Transformation { get; set; }

        public KdTreeNearestQueryData(IDrawableFactoryProvider drawableFactoryProvider, IFuncExecutor funcExecutor)
        {
            FuncExecutor = funcExecutor;

            Transformation = Transformation.Identity;
            PointCount = 100;
            QueryCenterDrawable = new QueryCenterPoint(drawableFactoryProvider);

            QueryResultDrawable = new KdTreeQueryResult(drawableFactoryProvider);

            Drawable = new ToggleableDrawable(new CompositeDrawable(QueryCenterDrawable, QueryResultDrawable));
        }


        public void Reset(KdTree<VertexNormal> kdTree)
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

        public void SetPointCount(int pointCount)
        {
            PointCount = pointCount;
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

            var radiusQuery = FuncExecutor.Execute((progress) => KdTree.FindNearestVertices(QueryCenter, PointCount, progress));

            radiusQuery.GetResult((vertices) =>
            {
                QueryResultDrawable.Reset(vertices.Values);
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
