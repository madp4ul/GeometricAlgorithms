using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Drawables;
using GeometricAlgorithms.Domain.Tasks;
using GeometricAlgorithms.ImplicitSurfaces;
using GeometricAlgorithms.ImplicitSurfaces.MarchingCubes;
using GeometricAlgorithms.MeshQuerying;
using GeometricAlgorithms.Viewer.Extensions;
using GeometricAlgorithms.Viewer.Interfaces;

namespace GeometricAlgorithms.Viewer.Model.FaceModels
{
    public class ApproximatedFaceData
    {
        private readonly IDrawableFactoryProvider DrawableFactoryProvider;
        private readonly IFuncExecutor FuncExecutor;

        public readonly FaceData FaceData;

        public KdTree KdTree { get; private set; }

        public int FunctionValueRadius { get; set; }
        public int UsedNearestPointCount { get; set; }
        public int StepsPerSide { get; set; }

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


        public bool CanApproximate => KdTree != null;

        public ApproximatedFaceData(IDrawableFactoryProvider drawableFactoryProvider, IFuncExecutor funcExecutor)
        {
            DrawableFactoryProvider = drawableFactoryProvider;
            FuncExecutor = funcExecutor;
            UsedNearestPointCount = 10;
            StepsPerSide = 16;
            FunctionValueRadius = 4;
            InnerFunctionValuesDrawable = new ContainerDrawable();
            OuterFunctionValuesDrawable = new ContainerDrawable();

            FaceData = new FaceData(drawableFactoryProvider);
        }

        public void Reset(KdTree kdTree)
        {
            KdTree = kdTree;

            FaceData.Reset(Mesh.CreateEmpty());
        }

        public void CalculateApproximation()
        {
            if (KdTree == null)
            {
                throw new InvalidOperationException("Reset with tree first");
            }

            var approximation = new ScalarProductSurface(KdTree, UsedNearestPointCount);

            var cubeMarcher = CubeMarcher.AroundBox(KdTree.MeshContainer, 0.1f, approximation, StepsPerSide);

            //Calculate function values
            FuncExecutor.Execute(progress => cubeMarcher.GetFunctionValues(progress))
                .GetResult((functionValueGrid) =>
                {
                    SetInnerFunctionValuesDrawable(functionValueGrid.FunctionValues);
                    SetOuterFunctionValuesDrawable(functionValueGrid.FunctionValues);

                    //Use function values to calculate surface
                    FuncExecutor.Execute(progress => cubeMarcher.GetSurface(functionValueGrid, progress))
                        .GetResult(mesh => FaceData.Reset(mesh));
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
