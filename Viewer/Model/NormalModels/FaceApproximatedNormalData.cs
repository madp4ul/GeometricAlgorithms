using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Tasks;
using GeometricAlgorithms.NormalApproximation;
using GeometricAlgorithms.Viewer.Interfaces;

namespace GeometricAlgorithms.Viewer.Model.NormalModels
{
    public class FaceApproximatedNormalData : NormalData
    {
        private readonly NormalApproximatorFromFaces Approximator;
        private readonly IFuncExecutor FuncExecutor;

        public bool CanApproximate => Mesh?.FileFaces != null;

        public FaceApproximatedNormalData(IDrawableFactoryProvider drawableFactoryProvider, IFuncExecutor funcExecutor)
            : base(drawableFactoryProvider)
        {
            FuncExecutor = funcExecutor;

            Approximator = new NormalApproximatorFromFaces();
        }

        public void CalculateApproximation()
        {
            var normalCalculation = FuncExecutor.Execute((progress) => Approximator.GetNormals(Mesh.Positions, Mesh.FileFaces, progress));

            normalCalculation.GetResult((normals) =>
            {
                Mesh.FaceApproximatedNormals = normals;

                Reset();
            });
        }

        protected override IEnumerable<Vector3> SelectNormals(Mesh mesh)
        {
            return mesh.FaceApproximatedNormals;
        }
    }
}
