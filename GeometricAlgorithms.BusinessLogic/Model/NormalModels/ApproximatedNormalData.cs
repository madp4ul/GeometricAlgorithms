using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Drawables;
using GeometricAlgorithms.Domain.Tasks;
using GeometricAlgorithms.NormalApproximation;

namespace GeometricAlgorithms.BusinessLogic.Model.NormalModels
{
    public class ApproximatedNormalData
    {
        private readonly NormalApproximatorFromFaces Approximator;
        private readonly IFuncExecutor FuncExecutor;

        public readonly NormalData NormalData;

        public Mesh SourceMesh { get; private set; }

        public bool CanApproximateFromFaces => NormalData.Mesh?.Faces != null;

        public ApproximatedNormalData(IDrawableFactoryProvider drawableFactoryProvider, IFuncExecutor funcExecutor)
        {
            FuncExecutor = funcExecutor;

            Approximator = new NormalApproximatorFromFaces();
            NormalData = new NormalData(drawableFactoryProvider);
        }

        public void Reset(Mesh mesh)
        {
            SourceMesh = mesh;
            NormalData.Reset(Mesh.CreateEmpty());
        }

        public void CalculateApproximationFromFaces()
        {
            var normalCalculation = FuncExecutor.Execute((progress) => Approximator.GetNormals(
                SourceMesh.Positions,
                SourceMesh.Faces,
                progress));

            normalCalculation.GetResult((normals) =>
            {
                var approximatedMesh = SourceMesh.Copy(replaceNormals: normals);

                NormalData.Reset(approximatedMesh);
            });
        }

        public IEnumerable<IDrawable> GetDrawables()
        {
            return NormalData.GetDrawables();
        }
    }
}
