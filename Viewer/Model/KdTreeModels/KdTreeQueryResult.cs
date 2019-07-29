using GeometricAlgorithms.Domain.VertexTypes;
using GeometricAlgorithms.MonoGame.Forms.Drawables;
using GeometricAlgorithms.Viewer.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Viewer.Model.KdTreeModels
{
    public class KdTreeQueryResult : ToggleableDrawable
    {
        readonly IDrawableFactoryProvider DrawableFactoryProvider;

        public int PointRadius { get; set; }

        public KdTreeQueryResult(IDrawableFactoryProvider drawableFactoryProvider) : base(false)
        {
            DrawableFactoryProvider = drawableFactoryProvider;
            PointRadius = 10;
        }

        public void Reset(IEnumerable<GenericVertex> vertices)
        {
            Drawable.Dispose();

            Drawable = DrawableFactoryProvider.DrawableFactory.CreateHighlightedPointCloud(
                vertices.Select(v => v.Position), PointRadius);
        }
    }
}
