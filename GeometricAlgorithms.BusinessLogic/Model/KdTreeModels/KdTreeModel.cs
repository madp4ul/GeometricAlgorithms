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
    public class KdTreeModel : IHasDrawables, IUpdatable<Mesh>, IUpdatable<KdTreeConfiguration>
    {
        private readonly IDrawableFactoryProvider DrawableFactoryProvider;
        private readonly IFuncExecutor FuncExecutor;

        public KdTree KdTree { get; private set; }
        public KdTreeConfiguration Configuration { get; private set; }

        private readonly ContainerDrawable KdTreeBoxDrawable;

        public event Action Updated;

        public bool DrawKdTree { get => KdTreeBoxDrawable.EnableDraw; set => KdTreeBoxDrawable.EnableDraw = value; }

        public readonly KdTreeRadiusQueryModel RadiusQuery;
        public readonly KdTreeNearestQueryModel NearestQuery;

        public KdTreeModel(IDrawableFactoryProvider drawableFactoryProvider, IFuncExecutor funcExecutor)
        {
            DrawableFactoryProvider = drawableFactoryProvider;
            FuncExecutor = funcExecutor;

            KdTreeBoxDrawable = new ContainerDrawable(enable: false);
            Configuration = KdTreeConfiguration.Default;

            RadiusQuery = new KdTreeRadiusQueryModel(drawableFactoryProvider, funcExecutor);
            NearestQuery = new KdTreeNearestQueryModel(drawableFactoryProvider, funcExecutor);
        }

        public void Update(KdTreeConfiguration configuration)
        {
            Configuration = configuration;

            Update(KdTree.Mesh);
        }

        public void Update(Mesh mesh)
        {
            var buildKdTree = FuncExecutor.Execute((progress) =>
            {
                return new KdTree(mesh, Configuration, progress);
            });

            buildKdTree.GetResult(kdTree =>
            {
                KdTree = kdTree;

                var boxes = KdTree.GetBoundingBoxes().ToArray();

                IDrawable newDrawable = boxes.Length == 0
                    ? new EmptyDrawable()
                    : CreateKdTreeDrawable(boxes);

                KdTreeBoxDrawable.SwapDrawable(newDrawable);

                RadiusQuery.Update(kdTree);
                NearestQuery.Update(kdTree);

                Updated?.Invoke();
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

            foreach (var item in RadiusQuery.GetDrawables()
                .Concat(NearestQuery.GetDrawables()))
            {
                yield return item;
            }
        }
    }
}
