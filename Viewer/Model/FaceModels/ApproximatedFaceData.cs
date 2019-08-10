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
using GeometricAlgorithms.MeshQuerying;
using GeometricAlgorithms.Viewer.Extensions;
using GeometricAlgorithms.Viewer.Interfaces;

namespace GeometricAlgorithms.Viewer.Model.FaceModels
{
    public class ApproximatedFaceData : FaceData
    {
        private readonly IFuncExecutor FuncExecutor;

        public KdTree KdTree { get; private set; }

        public int UsedNearestPointCount { get; set; }

        public IDrawable FunctionValuesDrawable { get; set; }

        public bool CanApproximate => KdTree != null;

        public ApproximatedFaceData(IDrawableFactoryProvider drawableFactoryProvider, IFuncExecutor funcExecutor)
            : base(drawableFactoryProvider)
        {
            FuncExecutor = funcExecutor;

            UsedNearestPointCount = 10;

            FunctionValuesDrawable = new EmptyDrawable();
        }

        public void Reset(KdTree kdTree)
        {
            KdTree = kdTree;

            Reset(kdTree.Mesh);
        }

        public void CalculateApproximation()
        {
            if (KdTree == null)
            {
                throw new InvalidOperationException("Reset with tree first");
            }

            var approximation = new ScalarProductSurface(KdTree, UsedNearestPointCount);

            var cubeMarcher = CubeMarcher.AroundBox(KdTree.MeshContainer, 0.1f, approximation, 40);

            var faceCalculation = FuncExecutor.Execute((progress) => cubeMarcher.GetFunctionValues());

            faceCalculation.GetResult((functionValues) =>
            {
                var positions = functionValues.Result.Select(kv => kv.Key);
                var colors = functionValues.Result.Select(kv => kv.Value > 0 ? Color.Green.ToVector3() : Color.Red.ToVector3());

                FunctionValuesDrawable.Dispose();
                FunctionValuesDrawable = DrawableFactoryProvider.DrawableFactory
                .CreatePointCloud(positions, 5, colors);
            });
        }

        public override void Draw(ACamera camera)
        {
            base.Draw(camera);
            FunctionValuesDrawable.Draw(camera);
        }
    }
}
