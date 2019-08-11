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

        public int UsedNearestPointCount { get; set; }

        private readonly ContainerDrawable FunctionValuesDrawable;
        public bool DrawFunctionValues
        {
            get => FunctionValuesDrawable.EnableDraw;
            set => FunctionValuesDrawable.EnableDraw = value;
        }


        public bool CanApproximate => KdTree != null;

        public ApproximatedFaceData(IDrawableFactoryProvider drawableFactoryProvider, IFuncExecutor funcExecutor)
        {
            DrawableFactoryProvider = drawableFactoryProvider;
            FuncExecutor = funcExecutor;
            UsedNearestPointCount = 10;
            FunctionValuesDrawable = new ContainerDrawable();

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

            var cubeMarcher = CubeMarcher.AroundBox(KdTree.MeshContainer, 0.1f, approximation, 40);

            var faceCalculation = FuncExecutor.Execute((progress) => cubeMarcher.MarchCubes());

            faceCalculation.GetResult((mesh) =>
            {
                var positions = cubeMarcher.FunctionValueGrid.FunctionValues.Select(f => f.Position);
                var colors = cubeMarcher.FunctionValueGrid.FunctionValues
                    .Select(f => f.Value > 0 ? Color.Green.ToVector3() : Color.Red.ToVector3());

                //Show function value samples
                FunctionValuesDrawable.SwapDrawable(
                    DrawableFactoryProvider.DrawableFactory.CreatePointCloud(positions, 5, colors));

                //Show approximated face data
                FaceData.Reset(mesh);
            });
        }

        public IEnumerable<IDrawable> GetDrawables()
        {
            yield return FunctionValuesDrawable;

            foreach (var drawable in FaceData.GetDrawables())
            {
                yield return drawable;
            }
        }
    }
}
