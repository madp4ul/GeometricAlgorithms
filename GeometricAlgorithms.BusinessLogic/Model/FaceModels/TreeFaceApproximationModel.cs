using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Drawables;
using GeometricAlgorithms.Domain.Tasks;
using GeometricAlgorithms.ImplicitSurfaces;
using GeometricAlgorithms.ImplicitSurfaces.MarchingOctree;
using GeometricAlgorithms.MeshQuerying;
using GeometricAlgorithms.BusinessLogic.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.BusinessLogic.Model.FaceModels
{
    public class TreeFaceApproximationModel : IHasDrawables, IUpdatable<Mesh>, IUpdatable<IFiniteImplicitSurface>
    {
        private const float RoomAroundModel = 0.1f;

        private readonly IDrawableFactoryProvider DrawableFactoryProvider;
        private readonly IFuncExecutor FuncExecutor;

        public readonly TreeEnumerationModel EdgeTreeEnumeration;

        private IImplicitSurface ImplicitSurface;

        public Mesh Mesh { get; private set; }
        public EdgeTree EdgeTree { get; private set; }

        public int FunctionValueRadius { get; set; }

        public int MaximumPointsPerLeaf { get; set; }

        private readonly ContainerDrawable InnerFunctionValuesDrawable;
        public bool DrawInnerFunctionValues
        {
            get => InnerFunctionValuesDrawable.EnableDraw;
            set => InnerFunctionValuesDrawable.EnableDraw = value;
        }

        private readonly ContainerDrawable OuterFunctionValuesDrawable;

        public event Action Updated;
        public event Action<Mesh> MeshCalculated;

        public bool DrawOuterFunctionValues
        {
            get => OuterFunctionValuesDrawable.EnableDraw;
            set => OuterFunctionValuesDrawable.EnableDraw = value;
        }

        public bool CanApproximate => Mesh?.HasNormals ?? false;

        public TreeFaceApproximationModel(
            IDrawableFactoryProvider drawableFactoryProvider,
            IFuncExecutor funcExecutor,
            IRefreshableView refreshableView)
        {
            DrawableFactoryProvider = drawableFactoryProvider;
            FuncExecutor = funcExecutor;
            FunctionValueRadius = 4;
            MaximumPointsPerLeaf = 5;
            InnerFunctionValuesDrawable = new ContainerDrawable();
            OuterFunctionValuesDrawable = new ContainerDrawable();

            EdgeTreeEnumeration = new TreeEnumerationModel(drawableFactoryProvider, refreshableView);
        }

        public void Update(Mesh mesh)
        {
            Mesh = mesh;

            Reset();

            Updated?.Invoke();
        }

        public void Update(IFiniteImplicitSurface surface)
        {
            ImplicitSurface = surface;

            Reset();

            Updated?.Invoke();
        }

        private void Reset()
        {
            InnerFunctionValuesDrawable.SwapDrawable(new EmptyDrawable());
            OuterFunctionValuesDrawable.SwapDrawable(new EmptyDrawable());
        }

        public void CalculateApproximation()
        {
            if (Mesh == null || ImplicitSurface == null)
            {
                throw new InvalidOperationException("update first");
            }

            var surface = ImplicitSurface;

            GetOctree().GetResult(octree =>
            {
                GetEdgetree(octree, surface).GetResult(edgetree =>
                {
                    EdgeTree = edgetree;
                    var result = edgetree.GetResult();

                    SetInnerFunctionValuesDrawable(result.GetInnerValues());
                    SetOuterFunctionValuesDrawable(result.GetouterValues());

                    EdgeTreeEnumeration.Update(edgetree);

                    var mesh = new Mesh(result.GetPositions(), result.GetFaces());

                    MeshCalculated?.Invoke(mesh);
                });
            });
        }

        private IFuncExecution<EdgeTree> GetEdgetree(Octree octree, IImplicitSurface surface)
        {
            return FuncExecutor.Execute(progress =>
            {
                return new EdgeTree(octree, surface);
            });
        }

        private IFuncExecution<Octree> GetOctree()
        {
            return FuncExecutor.Execute(progress =>
             {
                 return new Octree(
                     Mesh,
                     new TreeConfiguration(
                         maximumPointsPerLeaf: MaximumPointsPerLeaf,
                         minimizeContainers: false,
                         meshContainerScale: 1f + RoomAroundModel),
                     progress);
             });
        }

        private void SetInnerFunctionValuesDrawable(FunctionValue[] functionValues)
        {
            var innerValues = functionValues.Where(fv => fv.IsInside);

            var positions = innerValues.Select(f => f.Position).ToArray();
            var colors = Enumerable.Repeat(Color.Red.ToVector3(), positions.Length);

            InnerFunctionValuesDrawable.SwapDrawable(
                    DrawableFactoryProvider.DrawableFactory.CreatePointCloud(positions, FunctionValueRadius, colors));
        }

        private void SetOuterFunctionValuesDrawable(FunctionValue[] functionValues)
        {
            var outerValues = functionValues.Where(fv => !fv.IsInside);

            var positions = outerValues.Select(f => f.Position).ToArray();
            var colors = Enumerable.Repeat(Color.Green.ToVector3(), positions.Length);

            OuterFunctionValuesDrawable.SwapDrawable(
                    DrawableFactoryProvider.DrawableFactory.CreatePointCloud(positions, FunctionValueRadius, colors));
        }

        public IEnumerable<IDrawable> GetDrawables()
        {
            yield return InnerFunctionValuesDrawable;
            yield return OuterFunctionValuesDrawable;
        }
    }
}
