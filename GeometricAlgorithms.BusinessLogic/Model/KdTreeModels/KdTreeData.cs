using GeometricAlgorithms.BusinessLogic.Model.FaceModels;
using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Drawables;
using GeometricAlgorithms.Domain.Tasks;
using GeometricAlgorithms.MeshQuerying;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.BusinessLogic.Model.KdTreeModels
{
    public class KdTreeData
    {
        private readonly IDrawableFactoryProvider DrawableFactoryProvider;
        private readonly IFuncExecutor FuncExecutor;

        public MeshQuerying.KdTree KdTree { get; private set; }
        public KdTreeConfiguration Configuration { get; set; }

        public KdTreeRadiusQueryData RadiusQuerydata { get; private set; }
        public KdTreeNearestQueryData NearestQuerydata { get; private set; }

        public readonly ApproximatedFaceData ApproximatedFaceData;

        private readonly ContainerDrawable KdTreeBoxDrawable;
        public bool DrawKdTree { get => KdTreeBoxDrawable.EnableDraw; set => KdTreeBoxDrawable.EnableDraw = value; }


        public KdTreeData(IDrawableFactoryProvider drawableFactoryProvider, IFuncExecutor funcExecutor)
        {
            DrawableFactoryProvider = drawableFactoryProvider;
            FuncExecutor = funcExecutor;

            KdTreeBoxDrawable = new ContainerDrawable(enable: false);
            Configuration = KdTreeConfiguration.Default;

            RadiusQuerydata = new KdTreeRadiusQueryData(drawableFactoryProvider, funcExecutor);
            NearestQuerydata = new KdTreeNearestQueryData(drawableFactoryProvider, funcExecutor);

            ApproximatedFaceData = new ApproximatedFaceData(drawableFactoryProvider, funcExecutor);
        }

        public void Reset()
        {
            Reset(KdTree.Mesh);
        }

        public void Reset(Mesh mesh)
        {
            var buildKdTree = FuncExecutor.Execute((progress) =>
              {
                  return new MeshQuerying.KdTree(mesh, Configuration, progress);
              });


            buildKdTree.GetResult(kdTree =>
            {
                KdTree = kdTree;

                var boxes = KdTree.GetBoundingBoxes().ToArray();

                IDrawable newDrawable = boxes.Length == 0
                    ? new EmptyDrawable()
                    : CreateKdTreeDrawable(boxes);

                KdTreeBoxDrawable.SwapDrawable(newDrawable);

                RadiusQuerydata.Reset(KdTree);
                NearestQuerydata.Reset(KdTree);

                ApproximatedFaceData.Reset(KdTree);
            });
        }

        private IDrawable CreateKdTreeDrawable(BoundingBox[] boxes)
        {
            float maxSideLength = boxes[0].Diagonal.MaximumComponent();

            //have a minimum lightness and let the rest be dictated by box volume relative to the root box
            Vector3 colorGenerator(BoundingBox box) => new Vector3(1, (box.Diagonal.MaximumComponent() / maxSideLength), 0);

            return DrawableFactoryProvider.DrawableFactory.CreateBoundingBoxRepresentation(boxes, colorGenerator);
        }

        public IEnumerable<IDrawable> GetDrawables()
        {
            yield return KdTreeBoxDrawable;
            foreach (var drawable in RadiusQuerydata.GetDrawables()
                .Concat(NearestQuerydata.GetDrawables())
                .Concat(ApproximatedFaceData.GetDrawables())
                )
            {
                yield return drawable;
            }
        }
    }
}
