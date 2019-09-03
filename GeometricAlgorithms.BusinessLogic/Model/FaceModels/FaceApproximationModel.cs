using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometricAlgorithms.BusinessLogic.Extensions;
using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Drawables;
using GeometricAlgorithms.Domain.Tasks;
using GeometricAlgorithms.ImplicitSurfaces;
using GeometricAlgorithms.ImplicitSurfaces.MarchingCubes;
using GeometricAlgorithms.MeshQuerying;

namespace GeometricAlgorithms.BusinessLogic.Model.FaceModels
{
    public class FaceApproximationModel : IHasDrawables, IUpdatable<IFiniteImplicitSurface>
    {
        private const float RoomAroundModel = 0.02f;

        private readonly IDrawableFactoryProvider DrawableFactoryProvider;
        private readonly IFuncExecutor FuncExecutor;

        public int FunctionValueRadius { get; set; }
        public int SamplesOnLongestSideSide { get => CubeMarcher?.StepsAlongLongestSide ?? 0; set => CubeMarcher?.SetSteps(value); }
        public int TotalSamples => CubeMarcher?.TotalValues ?? 0;

        private CubeMarcher CubeMarcher;

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

        public bool CanApproximate => CubeMarcher != null;

        public FaceApproximationModel(IDrawableFactoryProvider drawableFactoryProvider, IFuncExecutor funcExecutor)
        {
            DrawableFactoryProvider = drawableFactoryProvider;
            FuncExecutor = funcExecutor;
            SamplesOnLongestSideSide = 16;
            FunctionValueRadius = 4;
            InnerFunctionValuesDrawable = new ContainerDrawable();
            OuterFunctionValuesDrawable = new ContainerDrawable();
        }

        public void Update(IFiniteImplicitSurface surface)
        {
            CubeMarcher = surface != null
                ? CubeMarcher.AroundBox(surface.DefinedArea, RoomAroundModel, surface, SamplesOnLongestSideSide)
                : null;

            InnerFunctionValuesDrawable.SwapDrawable(new EmptyDrawable());
            OuterFunctionValuesDrawable.SwapDrawable(new EmptyDrawable());

            Updated?.Invoke();
        }

        public void CalculateApproximation()
        {
            if (!CanApproximate)
            {
                throw new InvalidOperationException("update with tree first");
            }

            var currentCubeMarcher = CubeMarcher;

            //Calculate function values
            FuncExecutor.Execute(progress => currentCubeMarcher.GetFunctionValues(progress))
                .GetResult((functionValueGrid) =>
                {
                    SetInnerFunctionValuesDrawable(functionValueGrid.FunctionValues);
                    SetOuterFunctionValuesDrawable(functionValueGrid.FunctionValues);

                    //Use function values to calculate surface
                    FuncExecutor.Execute(progress => currentCubeMarcher.GetSurface(functionValueGrid, progress))
                        .GetResult(mesh => MeshCalculated?.Invoke(mesh));
                });
        }

        private void SetInnerFunctionValuesDrawable(FunctionValue[] functionValues)
        {
            var innerValues = functionValues.Where(fv => fv.IsInside());

            var positions = innerValues.Select(f => f.Position).ToArray();
            var colors = Enumerable.Repeat(Color.Red.ToVector3(), positions.Length);

            InnerFunctionValuesDrawable.SwapDrawable(
                    DrawableFactoryProvider.DrawableFactory.CreatePointCloud(positions, FunctionValueRadius, colors));
        }

        private void SetOuterFunctionValuesDrawable(FunctionValue[] functionValues)
        {
            var outerValues = functionValues.Where(fv => !fv.IsInside());

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
