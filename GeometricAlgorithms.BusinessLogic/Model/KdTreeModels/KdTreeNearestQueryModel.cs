using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Drawables;
using GeometricAlgorithms.Domain.Tasks;
using GeometricAlgorithms.MeshQuerying;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.BusinessLogic.Model.KdTreeModels
{
    public class KdTreeNearestQueryModel : IHasDrawables, IUpdatable<MeshQuerying.KdTree>
    {
        private readonly IFuncExecutor FuncExecutor;
        private readonly CameraChangedEventDrawable CameraChangedEvent;

        private MeshQuerying.KdTree KdTree;

        public Vector3 QueryCenter { get; set; }
        public int PointCount { get; private set; }

        public bool IsCalculating { get; private set; }
        public bool QueryHasChangedSinceLastCalculation { get; private set; }

        public readonly QueryCenterPointModel QueryCenterPoint;
        public readonly KdTreeQueryResultModel QueryResult;

        public event Action Updated;

        public bool CanQuery => KdTree != null;

        public KdTreeNearestQueryModel(IDrawableFactoryProvider drawableFactoryProvider, IFuncExecutor funcExecutor)
        {
            FuncExecutor = funcExecutor;

            PointCount = 100;
            QueryCenterPoint = new QueryCenterPointModel(drawableFactoryProvider);

            QueryResult = new KdTreeQueryResultModel(drawableFactoryProvider);

            CameraChangedEvent = new CameraChangedEventDrawable();
            CameraChangedEvent.CameraChanged += OnCameraChanged;
        }

        public void Update(MeshQuerying.KdTree kdTree)
        {
            KdTree = kdTree;

            QueryCenterPoint.Update();
            QueryResult.Update(vertices: null);

            Updated?.Invoke();
        }

        public void HideAll()
        {
            QueryCenterPoint.Show = false;
            QueryResult.Show = false;
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

            radiusQuery.GetResult((vertexIndices) =>
            {
                QueryResult.Update(vertexIndices.Values.Select(pi => pi.Position));
                IsCalculating = false;
            });
        }

        public IEnumerable<IDrawable> GetDrawables()
        {
            yield return CameraChangedEvent;
            foreach (var drawable in QueryCenterPoint.GetDrawables()
                .Concat(QueryResult.GetDrawables())
                )
            {
                yield return drawable;
            }
        }

        private void OnCameraChanged(ACamera camera)
        {
            //Refresh query center based on camera position - can only happen in draw
            float centerDistance = 0.2f;
            Vector3 newQueryCenter = camera.Position + (camera.Forward.Normalized() * centerDistance);

            if (!newQueryCenter.Equals(QueryCenter))
            {
                QueryHasChangedSinceLastCalculation = true;
                QueryCenter = newQueryCenter;
                QueryCenterPoint.SetPosition(QueryCenter);
            }
        }
    }
}
