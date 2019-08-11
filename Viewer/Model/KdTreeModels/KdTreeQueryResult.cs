using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Drawables;
using GeometricAlgorithms.Viewer.Extensions;
using GeometricAlgorithms.Viewer.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Viewer.Model.KdTreeModels
{
    public class KdTreeQueryResult
    {
        readonly IDrawableFactoryProvider DrawableFactoryProvider;

        public int PointRadius { get; set; }

        private readonly ContainerDrawable HighlightDrawable;
        public bool Show { get => HighlightDrawable.EnableDraw; set => HighlightDrawable.EnableDraw = value; }

        public KdTreeQueryResult(IDrawableFactoryProvider drawableFactoryProvider)
        {
            DrawableFactoryProvider = drawableFactoryProvider;
            PointRadius = 10;

            HighlightDrawable = new ContainerDrawable(enable: false);
        }

        public void Reset()
        {
            Reset(Enumerable.Empty<Vector3>());
        }

        public void Reset(IEnumerable<Vector3> vertices)
        {
            var drawable = DrawableFactoryProvider.DrawableFactory.CreateHighlightedPointCloud(
                vertices, Color.LightBlue.ToVector3(), PointRadius);

            HighlightDrawable.SwapDrawable(drawable);
        }

        public IEnumerable<IDrawable> GetDrawables()
        {
            yield return HighlightDrawable;
        }
    }
}
