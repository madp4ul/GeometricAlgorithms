using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Cameras;
using GeometricAlgorithms.Domain.Drawables;
using GeometricAlgorithms.Domain.Tasks;
using GeometricAlgorithms.Viewer.Model.KdTreeModels;
using GeometricAlgorithms.Viewer.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Viewer.Model
{
    public class PointData : ToggleableDrawable
    {
        readonly IDrawableFactoryProvider DrawableFactoryProvider;

        public Mesh<VertexNormal> Model { get; set; }

        public readonly KdTreeData KdTreeData;


        public PointData(IDrawableFactoryProvider drawableFactoryProvider, IFuncExecutor funcExecutor)
        {
            DrawableFactoryProvider = drawableFactoryProvider;
            Model = Mesh<VertexNormal>.CreateEmpty();
            Drawable = new EmptyDrawable();
            KdTreeData = new KdTreeData(drawableFactoryProvider, funcExecutor);
        }

        public PointData(
           Mesh<VertexNormal> model,
            int radius,
            IDrawableFactoryProvider drawableFactoryProvider,
            IFuncExecutor funcExecutor)
            : this(drawableFactoryProvider, funcExecutor)
        {
            Reset(model, radius);
            KdTreeData.Reset(model);
        }

        public void Reset(Mesh<VertexNormal> model, int radius)
        {
            Model = model ?? throw new ArgumentNullException(nameof(model));

            if (Drawable != null)
            {
                Drawable.Dispose();
            }
            Drawable = DrawableFactoryProvider.DrawableFactory.CreatePointCloud(
                model.Vertices.Select(v => v.Position), radius);

            KdTreeData.Reset(Model);
        }

        public override void Draw(ACamera camera)
        {
            base.Draw(camera);
            KdTreeData.Draw(camera);
        }
    }
}
