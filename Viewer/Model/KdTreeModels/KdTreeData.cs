using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.VertexTypes;
using GeometricAlgorithms.KdTree;
using GeometricAlgorithms.MonoGame.Forms.Drawables;
using GeometricAlgorithms.Viewer.Providers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Viewer.Model.KdTreeModels
{
    public class KdTreeData : ToggleableDrawable
    {
        readonly IDrawableFactoryProvider DrawableFactoryProvider;

        public KdTree<GenericVertex> KdTree { get; set; }
        public KdTreeConfiguration Configuration { get; set; }

        public KdTreeData(IDrawableFactoryProvider drawableFactoryProvider)
        {
            DrawableFactoryProvider = drawableFactoryProvider;

            EnableDraw = false;
            Configuration = KdTreeConfiguration.Default;
            KdTree = new KdTree<GenericVertex>(new GenericVertex[0], Configuration);
        }

        public KdTreeData(GenericVertex[] points, IDrawableFactoryProvider drawableFactoryProvider) : this(drawableFactoryProvider)
        {
            Reset(points);
        }

        public void Reset()
        {
            Reset(KdTree.Vertices.ToArray());
        }

        public void Reset(GenericVertex[] points)
        {
            KdTree = new KdTree<GenericVertex>(points.ToArray(), Configuration);

            if (Drawable != null)
            {
                Drawable.Dispose();
            }

            var boxes = KdTree.GetBoundingBoxes().ToArray();

            if (boxes.Length == 0)
            {
                Drawable = new EmptyDrawable();
            }
            else
            {
                float maxVolume = boxes[0].Volume;

                //have a minimum lightness and let the rest be dictated by box volume relative to the root box
                Color colorGenerator(BoundingBox box) => Color.FromArgb(255, (int)(25 + 230 * (box.Volume / maxVolume)), 0, 0);

                Drawable = DrawableFactoryProvider.DrawableFactory.CreateBoundingBoxRepresentation(boxes, colorGenerator);
            }
        }
    }
}
