using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Cameras;
using GeometricAlgorithms.Domain.Drawables;
using GeometricAlgorithms.Domain.Tasks;
using GeometricAlgorithms.Viewer.Model.KdTreeModels;
using GeometricAlgorithms.Viewer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Viewer.Model
{
    public class PointData : ToggleableDrawable
    {
        private readonly IDrawableFactoryProvider DrawableFactoryProvider;

        public Mesh<VertexNormal> Model { get; private set; }

        public readonly KdTreeData KdTreeData;
        public readonly NormalData NormalData;
        public readonly ApproximatedNormalData ApproximatedNormalData;

        public PointData(IDrawableFactoryProvider drawableFactoryProvider, IFuncExecutor funcExecutor)
        {
            DrawableFactoryProvider = drawableFactoryProvider;
            Model = Mesh<VertexNormal>.CreateEmpty();
            Drawable = new EmptyDrawable();
            NormalData = new NormalData(drawableFactoryProvider);
            ApproximatedNormalData = new ApproximatedNormalData(drawableFactoryProvider);
            KdTreeData = new KdTreeData(drawableFactoryProvider, funcExecutor);
        }

        public void Reset(Mesh<VertexNormal> model, int pointRadius)
        {
            Model = model ?? throw new ArgumentNullException(nameof(model));

            if (Drawable != null)
            {
                Drawable.Dispose();
            }
            Drawable = DrawableFactoryProvider.DrawableFactory.CreatePointCloud(
                Model.Vertices.Select(v => v.Position), pointRadius);

            NormalData.Reset(Model);
            ApproximatedNormalData.Reset(Model);
            KdTreeData.Reset(Model);
        }

        public override void Draw(ACamera camera)
        {
            base.Draw(camera);
            NormalData.Draw(camera);
            ApproximatedNormalData.Draw(camera);
            KdTreeData.Draw(camera);
        }
    }
}
