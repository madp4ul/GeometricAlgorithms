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
    public class FaceApproximationModel : IHasDrawables, IUpdatable<MeshQuerying.KdTree>
    {
        private const float RoomAroundModel = 0.02f;

        private readonly IDrawableFactoryProvider DrawableFactoryProvider;
        private readonly IFuncExecutor FuncExecutor;

        public readonly FacesModel FaceData;

        public KdTree KdTree { get; private set; }

        public int FunctionValueRadius { get; set; }
        public int UsedNearestPointCount
        {
            get => ImplicitSurface?.UsedNearestPointCount ?? 0; set
            {
                if (ImplicitSurface != null)
                {
                    ImplicitSurface.UsedNearestPointCount = value;
                }
            }
        }
        public int SamplesOnLongestSideSide { get => CubeMarcher?.StepsAlongLongestSide ?? 0; set => CubeMarcher?.SetSteps(value); }
        public int TotalSamples => CubeMarcher?.TotalValues ?? 0;

        private ScalarProductSurface ImplicitSurface;
        private CubeMarcher CubeMarcher;


        private readonly ContainerDrawable InnerFunctionValuesDrawable;
        public bool DrawInnerFunctionValues
        {
            get => InnerFunctionValuesDrawable.EnableDraw;
            set => InnerFunctionValuesDrawable.EnableDraw = value;
        }

        private readonly ContainerDrawable OuterFunctionValuesDrawable;

        public event Action Updated;

        public bool DrawOuterFunctionValues
        {
            get => OuterFunctionValuesDrawable.EnableDraw;
            set => OuterFunctionValuesDrawable.EnableDraw = value;
        }

        public bool CanApproximate => KdTree?.Mesh.UnitNormals != null;

        public FaceApproximationModel(IDrawableFactoryProvider drawableFactoryProvider, IFuncExecutor funcExecutor)
        {
            DrawableFactoryProvider = drawableFactoryProvider;
            FuncExecutor = funcExecutor;
            UsedNearestPointCount = 10;
            SamplesOnLongestSideSide = 16;
            FunctionValueRadius = 4;
            InnerFunctionValuesDrawable = new ContainerDrawable();
            OuterFunctionValuesDrawable = new ContainerDrawable();

            CubeMarcher = CubeMarcher.CreateDummy();
            FaceData = new FacesModel(drawableFactoryProvider);
        }

        public void Update(KdTree kdTree)
        {
            KdTree = kdTree;
            ImplicitSurface = new ScalarProductSurface(kdTree, UsedNearestPointCount);
            CubeMarcher = CubeMarcher.AroundBox((BoundingBox)KdTree.MeshContainer, RoomAroundModel, ImplicitSurface, SamplesOnLongestSideSide);

            InnerFunctionValuesDrawable.SwapDrawable(new EmptyDrawable());
            OuterFunctionValuesDrawable.SwapDrawable(new EmptyDrawable());
            FaceData.Update(Mesh.CreateEmpty());

            Updated?.Invoke();
        }

        public void CalculateApproximation()
        {
            if (KdTree == null)
            {
                throw new InvalidOperationException("Reset with tree first");
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
                        .GetResult(mesh => FaceData.Update(mesh));
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

            foreach (var drawable in FaceData.GetDrawables())
            {
                yield return drawable;
            }
        }
    }
}
