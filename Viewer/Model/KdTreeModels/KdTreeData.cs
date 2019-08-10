using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Drawables;
using GeometricAlgorithms.Domain.Tasks;
using GeometricAlgorithms.MeshQuerying;
using GeometricAlgorithms.Viewer.Interfaces;
using GeometricAlgorithms.Viewer.Model.FaceModels;
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

        public MeshQuerying.KdTree KdTree { get; private set; }
        public KdTreeConfiguration Configuration { get; set; }

        public KdTreeRadiusQueryData RadiusQuerydata { get; private set; }
        public KdTreeNearestQueryData NearestQuerydata { get; private set; }

        public readonly ApproximatedFaceData ApproximatedFaceData;


        public KdTreeData(IDrawableFactoryProvider drawableFactoryProvider, IFuncExecutor funcExecutor)
        {
            DrawableFactoryProvider = drawableFactoryProvider;
            FuncExecutor = funcExecutor;

            EnableDraw = false;
            Configuration = KdTreeConfiguration.Default;
            KdTree = new MeshQuerying.KdTree(Mesh.CreateEmpty(), Configuration);

            RadiusQuerydata = new KdTreeRadiusQueryData(drawableFactoryProvider, funcExecutor);
            NearestQuerydata = new KdTreeNearestQueryData(drawableFactoryProvider, funcExecutor);

            ApproximatedFaceData = new ApproximatedFaceData(drawableFactoryProvider, funcExecutor);
        }

        public void Reset()
        {
            Reset(KdTree.Mesh);
        }

        public void Reset(Mesh model)
        {
            var buildKdTree = FuncExecutor.Execute((progress) =>
              {
                  return new MeshQuerying.KdTree(model, Configuration, progress);
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

                ApproximatedFaceData.Reset(KdTree);
            });
        }

        public override void Draw(ACamera camera)
        {
            base.Draw(camera);

            RadiusQuerydata.Draw(camera);
            NearestQuerydata.Draw(camera);

            ApproximatedFaceData.Draw(camera);
        }
    }
}
