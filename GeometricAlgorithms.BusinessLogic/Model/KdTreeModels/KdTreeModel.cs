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
    public class KdTreeModel : IHasDrawables, IUpdatable<Mesh>, IUpdatable<TreeConfiguration>
    {
        private readonly IDrawableFactoryProvider DrawableFactoryProvider;
        private readonly IFuncExecutor FuncExecutor;

        public ATree Tree { get; private set; }
        public TreeConfiguration Configuration { get; private set; }

        private readonly ContainerDrawable KdTreeBranchesDrawable;
        private readonly ContainerDrawable KdTreeLeavesDrawable;

        public event Action Updated;

        public bool DrawKdTreeBranches { get => KdTreeBranchesDrawable.EnableDraw; set => KdTreeBranchesDrawable.EnableDraw = value; }
        public bool DrawKdTreeLeaves { get => KdTreeLeavesDrawable.EnableDraw; set => KdTreeLeavesDrawable.EnableDraw = value; }

        public readonly KdTreeRadiusQueryModel RadiusQuery;
        public readonly KdTreeNearestQueryModel NearestQuery;

        public KdTreeModel(IDrawableFactoryProvider drawableFactoryProvider, IFuncExecutor funcExecutor)
        {
            DrawableFactoryProvider = drawableFactoryProvider;
            FuncExecutor = funcExecutor;

            KdTreeBranchesDrawable = new ContainerDrawable(enable: false);
            KdTreeLeavesDrawable = new ContainerDrawable(enable: false);
            Configuration = new TreeConfiguration();

            RadiusQuery = new KdTreeRadiusQueryModel(drawableFactoryProvider, funcExecutor);
            NearestQuery = new KdTreeNearestQueryModel(drawableFactoryProvider, funcExecutor);
        }

        public void Update(TreeConfiguration configuration)
        {
            Configuration = configuration;

            Update(Tree.Mesh);
        }

        public void Update(Mesh mesh)
        {
            var buildKdTree = FuncExecutor.Execute((progress) =>
            {
                return new KdTree(mesh, Configuration, progress);
            });

            buildKdTree.GetResult(tree =>
            {
                Tree = tree;

                CreateLeavesDrawable(tree);
                CreateBranchesDrawable(tree);

                RadiusQuery.Update(tree);
                NearestQuery.Update(tree);

                Updated?.Invoke();
            });
        }

        private void CreateLeavesDrawable(ATree tree)
        {
            var boxes = tree.GetLeafBoudingBoxes().ToArray();

            KdTreeLeavesDrawable.SwapDrawable(CreateKdTreeDrawable(boxes));
        }

        private void CreateBranchesDrawable(ATree tree)
        {
            var boxes = tree.GetBranchBoudingBoxes().ToArray();

            KdTreeBranchesDrawable.SwapDrawable(CreateKdTreeDrawable(boxes));
        }

        private IDrawable CreateKdTreeDrawable(BoundingBox[] boxes)
        {
            if (boxes.Length == 0)
            {
                return new EmptyDrawable();
            }

            float maxSideLength = boxes[0].Diagonal.MaximumComponent();

            //have a minimum lightness and let the rest be dictated by box volume relative to the root box
            Vector3 colorGenerator(BoundingBox box) => new Vector3(1, (box.Diagonal.MaximumComponent() / maxSideLength), 0);

            return DrawableFactoryProvider.DrawableFactory.CreateBoundingBoxRepresentation(boxes, colorGenerator);
        }

        public IEnumerable<IDrawable> GetDrawables()
        {
            yield return KdTreeBranchesDrawable;
            yield return KdTreeLeavesDrawable;

            foreach (var item in RadiusQuery.GetDrawables()
                .Concat(NearestQuery.GetDrawables()))
            {
                yield return item;
            }
        }
    }
}
