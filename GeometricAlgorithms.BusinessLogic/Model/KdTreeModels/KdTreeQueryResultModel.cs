using GeometricAlgorithms.BusinessLogic.Extensions;
using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Drawables;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.BusinessLogic.Model.KdTreeModels
{
    public class KdTreeQueryResultModel : IHasDrawables, IUpdatable<IEnumerable<Vector3>>
    {
        readonly IDrawableFactoryProvider DrawableFactoryProvider;

        public int PointRadius { get; set; }

        private readonly ContainerDrawable HighlightDrawable;

        public event Action Updated;

        public bool Show { get => HighlightDrawable.EnableDraw; set => HighlightDrawable.EnableDraw = value; }

        public KdTreeQueryResultModel(IDrawableFactoryProvider drawableFactoryProvider)
        {
            DrawableFactoryProvider = drawableFactoryProvider;
            PointRadius = 10;

            HighlightDrawable = new ContainerDrawable(enable: false);
        }

        public void Update(IEnumerable<Vector3> vertices)
        {
            if (vertices == null)
            {
                vertices = Enumerable.Empty<Vector3>();
            }

            var drawable = DrawableFactoryProvider.DrawableFactory.CreateHighlightedPointCloud(
                    vertices, Color.LightBlue.ToVector3(), PointRadius);

            HighlightDrawable.SwapDrawable(drawable);

            Updated?.Invoke();
        }

        public IEnumerable<IDrawable> GetDrawables()
        {
            yield return HighlightDrawable;
        }
    }
}
