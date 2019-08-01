using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Drawables;
using GeometricAlgorithms.Domain.Providers;
using GeometricAlgorithms.Viewer.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Viewer.Model.KdTreeModels
{
    public class QueryCenterPoint : ToggleableDrawable
    {
        readonly IDrawableFactoryProvider DrawableFactoryProvider;

        public QueryCenterPoint(IDrawableFactoryProvider drawableFactoryProvider) : base(false)
        {
            DrawableFactoryProvider = drawableFactoryProvider ?? throw new ArgumentNullException(nameof(drawableFactoryProvider));
        }

        public void Reset()
        {
            Drawable = DrawableFactoryProvider.DrawableFactory.CreateHighlightedPointCloud(
                points: new[] { Vector3.Zero },
                highlightColor: Color.Red.ToVector3(),
                radius: 10);
        }

        public void SetPosition(Vector3 position)
        {
            Transformation.Position = position;
        }
    }
}
