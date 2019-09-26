using GeometricAlgorithms.BusinessLogic.Extensions;
using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Drawables;
using GeometricAlgorithms.Domain.Tasks;
using GeometricAlgorithms.ImplicitSurfaces;
using GeometricAlgorithms.ImplicitSurfaces.MarchingOctree;
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
        private readonly IDrawableFactoryProvider DrawableFactoryProvider;
        private readonly IFuncExecutor FuncExecutor;

        private IImplicitSurface ImplicitSurface;

        public Mesh Mesh { get; private set; }
        public EdgeTree EdgeTree { get; private set; }

        public int FunctionValueRadius { get; set; }
        public int SampleLimit { get; set; }


        private readonly ContainerDrawable InnerFunctionValuesDrawable;
        public bool DrawInnerFunctionValues
        {
            get => InnerFunctionValuesDrawable.EnableDraw;
            set => InnerFunctionValuesDrawable.EnableDraw = value;
        }

        private readonly ContainerDrawable OuterFunctionValuesDrawable;
        public bool DrawOuterFunctionValues
        {
            get => OuterFunctionValuesDrawable.EnableDraw;
            set => OuterFunctionValuesDrawable.EnableDraw = value;
        }

        public event Action Updated;
        public event Action<Mesh> MeshCalculated;

        public bool CanApproximate => Mesh?.HasNormals ?? false;

        public TreeFaceApproximationModel(
            IDrawableFactoryProvider drawableFactoryProvider,
            IFuncExecutor funcExecutor)
        {
            DrawableFactoryProvider = drawableFactoryProvider;
            FuncExecutor = funcExecutor;
            FunctionValueRadius = 4;
            SampleLimit = 1000;
            InnerFunctionValuesDrawable = new ContainerDrawable(enable: false);
            OuterFunctionValuesDrawable = new ContainerDrawable(enable: false);
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
            if (ImplicitSurface != null && Mesh != null)
            {
                EdgeTree = EdgeTree.CreateWithWithMostPointsFirst(ImplicitSurface, Mesh);
                SetAllFunctionValueDrawables(EdgeTree?.ImplicitSurfaceProvider.GetFunctionValues());
            }

        }

        public void CalculateApproximation()
        {
            if (EdgeTree == null)
            {
                throw new InvalidOperationException("update first");
            }

            var surface = ImplicitSurface;
            var edgeTree = EdgeTree;

            var refine = FuncExecutor.Execute(progress =>
            {
                edgeTree.RefineEdgeTree(SampleLimit);

                return true;
            });

            refine.GetResult(_ =>
            {
                var approximate = FuncExecutor.Execute(progress => edgeTree.CreateApproximation());

                approximate.GetResult(mesh => SetApproximation(mesh, edgeTree));
            });
        }

        private void SetApproximation(Mesh mesh, EdgeTree edgeTree)
        {
            SetAllFunctionValueDrawables(edgeTree.ImplicitSurfaceProvider.GetFunctionValues());

            MeshCalculated?.Invoke(mesh);
        }

        private void SetAllFunctionValueDrawables(FunctionValue[] functionValues)
        {
            if (functionValues != null)
            {
                var innerFv = functionValues.Where(f => f.IsInside).ToArray();
                var outerFv = functionValues.Where(f => !f.IsInside).ToArray();

                SetInnerFunctionValuesDrawable(innerFv);
                SetOuterFunctionValuesDrawable(outerFv);
            }
            else
            {
                InnerFunctionValuesDrawable.SwapDrawable(new EmptyDrawable());
                OuterFunctionValuesDrawable.SwapDrawable(new EmptyDrawable());
            }
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
