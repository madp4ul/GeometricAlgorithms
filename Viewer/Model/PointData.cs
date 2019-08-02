using GeometricAlgorithms.Domain.Cameras;
using GeometricAlgorithms.Domain.Drawables;
using GeometricAlgorithms.Domain.Tasks;
using GeometricAlgorithms.Domain.VertexTypes;
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

        public GenericVertex[] Points { get; set; }

        public readonly KdTreeData KdTreeData;


        public PointData(IDrawableFactoryProvider drawableFactoryProvider, IFuncExecutor funcExecutor)
        {
            DrawableFactoryProvider = drawableFactoryProvider;
            Points = new GenericVertex[0];
            Drawable = new EmptyDrawable();
            KdTreeData = new KdTreeData(drawableFactoryProvider, funcExecutor);
        }

        public PointData(
            GenericVertex[] points,
            int radius,
            IDrawableFactoryProvider drawableFactoryProvider,
            IFuncExecutor funcExecutor)
            : this(drawableFactoryProvider, funcExecutor)
        {
            Reset(points, radius);
            KdTreeData.Reset(points);
        }

        public void Reset(GenericVertex[] points, int radius)
        {
            Points = points ?? throw new ArgumentNullException(nameof(points));

            if (Drawable != null)
            {
                Drawable.Dispose();
            }
            Drawable = DrawableFactoryProvider.DrawableFactory.CreatePointCloud(
                points.Select(v => v.Position), radius);

            KdTreeData.Reset(Points);
        }

        public override void Draw(ACamera camera)
        {
            base.Draw(camera);
            KdTreeData.Draw(camera);
        }
    }
}
