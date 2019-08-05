using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Cameras;
using GeometricAlgorithms.Domain.Drawables;
using GeometricAlgorithms.Domain.Tasks;
using GeometricAlgorithms.KdTree;
using GeometricAlgorithms.Viewer.Interfaces;
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

        public KdTree<VertexNormal> KdTree { get; private set; }
        public KdTreeConfiguration Configuration { get; set; }

        public KdTreeRadiusQueryData RadiusQuerydata { get; private set; }
        public KdTreeNearestQueryData NearestQuerydata { get; private set; }

        public KdTreeData(IDrawableFactoryProvider drawableFactoryProvider, IFuncExecutor funcExecutor)
        {
            DrawableFactoryProvider = drawableFactoryProvider;
            FuncExecutor = funcExecutor;

            EnableDraw = false;
            Configuration = KdTreeConfiguration.Default;
            KdTree = new KdTree<VertexNormal>(Mesh<VertexNormal>.CreateEmpty(), Configuration);

            RadiusQuerydata = new KdTreeRadiusQueryData(drawableFactoryProvider, funcExecutor);
            NearestQuerydata = new KdTreeNearestQueryData(drawableFactoryProvider, funcExecutor);
        }

        public KdTreeData(
            Mesh<VertexNormal> model,
            IDrawableFactoryProvider drawableFactoryProvider,
            IFuncExecutor funcExecutor)
            : this(drawableFactoryProvider, funcExecutor)
        {
            Reset(model);
        }

        public void Reset()
        {
            Reset(KdTree.Model);
        }

        public void Reset(Mesh<VertexNormal> model)
        {
            var buildKdTree = FuncExecutor.Execute((progress) =>
              {
                  return new KdTree<VertexNormal>(model, Configuration, progress);
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
                    float maxSideLength = boxes[0].Diagonal.MaximumComponent();

                    //have a minimum lightness and let the rest be dictated by box volume relative to the root box
                    Vector3 colorGenerator(BoundingBox box) => new Vector3(1, (box.Diagonal.MaximumComponent() / maxSideLength), 0);

                    Drawable = DrawableFactoryProvider.DrawableFactory.CreateBoundingBoxRepresentation(boxes, colorGenerator);
                }

                RadiusQuerydata.Reset(KdTree);
                NearestQuerydata.Reset(KdTree);
            });
        }

        public override void Draw(ACamera camera)
        {
            base.Draw(camera);

            RadiusQuerydata.Draw(camera);
            NearestQuerydata.Draw(camera);
        }
    }
}
