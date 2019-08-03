using GeometricAlgorithms.Domain.Drawables;
using GeometricAlgorithms.Domain.Vertices;
using GeometricAlgorithms.Viewer.Extensions;
using GeometricAlgorithms.Viewer.Providers;
using System;
using System.Collections.Generic;
using System.Drawing;
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

        public void Reset()
        {
            Reset(Enumerable.Empty<GenericVertex>());
        }

        public void Reset(IEnumerable<GenericVertex> vertices)
        {
            Drawable.Dispose();

            Drawable = DrawableFactoryProvider.DrawableFactory.CreateHighlightedPointCloud(
                vertices.Select(v => v.Position), Color.LightBlue.ToVector3(), PointRadius);
        }
    }
}
