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
    public class PointData : ToggleableDrawable
    {
        private readonly IDrawableFactoryProvider DrawableFactoryProvider;

        public Mesh Mesh { get; private set; }

        public readonly KdTreeData KdTreeData;

        public readonly NormalData NormalData;
        public readonly FaceApproximatedNormalData FaceApproximatedNormalData;

        public readonly FaceData FaceData;

        public PointData(IDrawableFactoryProvider drawableFactoryProvider, IFuncExecutor funcExecutor)
        {
            DrawableFactoryProvider = drawableFactoryProvider;
            Mesh = Mesh.CreateEmpty();
            Drawable = new EmptyDrawable();

            NormalData = new NormalData(drawableFactoryProvider);
            FaceApproximatedNormalData = new FaceApproximatedNormalData(drawableFactoryProvider);

            FaceData = new FaceData(drawableFactoryProvider);

            KdTreeData = new KdTreeData(drawableFactoryProvider, funcExecutor);
        }

        public void Reset(Mesh mesh, int pointRadius)
        {
            Mesh = mesh ?? throw new ArgumentNullException(nameof(mesh));

            if (Drawable != null)
            {
                Drawable.Dispose();
            }
            Drawable = DrawableFactoryProvider.DrawableFactory.CreatePointCloud(
                Mesh.Positions, pointRadius);

            NormalData.Reset(Mesh);
            FaceApproximatedNormalData.Reset(Mesh);

            FaceData.Reset(Mesh);

            KdTreeData.Reset(Mesh);
        }

        public override void Draw(ACamera camera)
        {
            base.Draw(camera);

            NormalData.Draw(camera);
            FaceApproximatedNormalData.Draw(camera);

            FaceData.Draw(camera);

            KdTreeData.Draw(camera);
        }
    }
}
