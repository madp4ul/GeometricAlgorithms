using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Drawables;
using GeometricAlgorithms.Domain.Tasks;
using GeometricAlgorithms.Viewer.Model.KdTreeModels;
using GeometricAlgorithms.Viewer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometricAlgorithms.Viewer.Model.NormalModels;
using GeometricAlgorithms.Viewer.Model.FaceModels;

namespace GeometricAlgorithms.Viewer.Model
{
    public class PointData
    {
        private readonly IDrawableFactoryProvider DrawableFactoryProvider;

        public Mesh Mesh { get; private set; }

        public readonly KdTreeData KdTreeData;
        public readonly NormalData NormalData;
        public readonly ApproximatedNormalData ApproximatedNormalData;
        public readonly FaceData FaceData;

        private readonly ContainerDrawable MeshPositionDrawable;
        public bool DrawMeshPositions { get => MeshPositionDrawable.EnableDraw; set => MeshPositionDrawable.EnableDraw = value; }

        public int UsedPointRadius { get; private set; }

        public PointData(IDrawableFactoryProvider drawableFactoryProvider, IFuncExecutor funcExecutor)
        {
            DrawableFactoryProvider = drawableFactoryProvider;
            Mesh = Mesh.CreateEmpty();
            MeshPositionDrawable = new ContainerDrawable();

            NormalData = new NormalData(drawableFactoryProvider);
            ApproximatedNormalData = new ApproximatedNormalData(drawableFactoryProvider, funcExecutor);

            FaceData = new FaceData(drawableFactoryProvider);

            KdTreeData = new KdTreeData(drawableFactoryProvider, funcExecutor);
        }

        public void Reset(Mesh mesh)
        {
            Reset(mesh, UsedPointRadius);
        }

        public void Reset(Mesh mesh, int pointRadius)
        {
            Mesh = mesh ?? throw new ArgumentNullException(nameof(mesh));
            UsedPointRadius = pointRadius;

            var pointCloud = DrawableFactoryProvider.DrawableFactory.CreatePointCloud(
                            Mesh.Positions, pointRadius);
            MeshPositionDrawable.SwapDrawable(pointCloud);

            NormalData.Reset(Mesh);
            ApproximatedNormalData.Reset(Mesh);

            FaceData.Reset(Mesh);

            KdTreeData.Reset(Mesh);
        }

        public IEnumerable<IDrawable> GetDrawables()
        {
            yield return MeshPositionDrawable;
            foreach (var drawable in KdTreeData.GetDrawables()
                .Concat(NormalData.GetDrawables())
                .Concat(ApproximatedNormalData.GetDrawables())
                .Concat(FaceData.GetDrawables())
                .Concat(NormalData.GetDrawables())
                )
            {
                yield return drawable;
            }
        }
    }
}
