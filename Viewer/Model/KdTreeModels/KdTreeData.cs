using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Cameras;
using GeometricAlgorithms.Domain.Drawables;
using GeometricAlgorithms.Domain.Tasks;
using GeometricAlgorithms.Domain.Vertices;
using GeometricAlgorithms.KdTree;
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
        private readonly IDrawableFactoryProvider DrawableFactoryProvider;
        private readonly IFuncExecutor FuncExecutor;

        public KdTree<GenericVertex> KdTree { get; private set; }
        public KdTreeConfiguration Configuration { get; set; }

        public KdTreeRadiusQueryData RadiusQuerydata { get; private set; }

        public KdTreeData(IDrawableFactoryProvider drawableFactoryProvider, IFuncExecutor funcExecutor)
        {
            DrawableFactoryProvider = drawableFactoryProvider;
            FuncExecutor = funcExecutor;

            EnableDraw = false;
            Configuration = KdTreeConfiguration.Default;
            KdTree = new KdTree<GenericVertex>(new GenericVertex[0], Configuration);

            RadiusQuerydata = new KdTreeRadiusQueryData(drawableFactoryProvider, funcExecutor);
        }

        public KdTreeData(
            GenericVertex[] points,
            IDrawableFactoryProvider drawableFactoryProvider,
            IFuncExecutor funcExecutor)
            : this(drawableFactoryProvider, funcExecutor)
        {
            Reset(points);
        }

        public void Reset()
        {
            Reset(KdTree.Vertices.ToArray());
        }

        public void Reset(GenericVertex[] points)
        {
            var buildKdTree = FuncExecutor.Execute((progress) =>
              {
                  return new KdTree<GenericVertex>(points, Configuration, progress);
              });


            buildKdTree.GetResult(kdTree =>
            {
                KdTree = kdTree;

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
                    Vector3 colorGenerator(BoundingBox box) => new Vector3(1, 0.2f + 0.8f * (box.Volume / maxVolume), 0);

                    Drawable = DrawableFactoryProvider.DrawableFactory.CreateBoundingBoxRepresentation(boxes, colorGenerator);
                }

                RadiusQuerydata.Reset(KdTree);
            });
        }

        public override void Draw(ACamera camera)
        {
            base.Draw(camera);

            RadiusQuerydata.Draw(camera);
        }
    }
}
