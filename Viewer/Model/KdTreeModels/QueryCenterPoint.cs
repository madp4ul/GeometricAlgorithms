using GeometricAlgorithms.Domain;
using GeometricAlgorithms.MonoGame.Forms.Drawables;
using GeometricAlgorithms.Viewer.Providers;
using System;
using System.Collections.Generic;
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
            Drawable = DrawableFactoryProvider.DrawableFactory.CreatePointCloud(new[] { Vector3.Zero }, 50);
        }

        public void SetPosition(Vector3 position)
        {
            Transformation.Position = position;

        }
    }
}
