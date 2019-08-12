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
    public class NormalApproximationModel : IHasDrawables, IUpdatable<Mesh>
    {
        private readonly NormalApproximatorFromFaces Approximator;
        private readonly IFuncExecutor FuncExecutor;

        public readonly NormalModel NormalData;

        public event Action Updated;

        public Mesh SourceMesh { get; private set; }

        public bool CanApproximateFromFaces => NormalData.Mesh?.Faces != null;

        public NormalApproximationModel(IDrawableFactoryProvider drawableFactoryProvider, IFuncExecutor funcExecutor)
        {
            FuncExecutor = funcExecutor;

            Approximator = new NormalApproximatorFromFaces();
            NormalData = new NormalModel(drawableFactoryProvider);
        }

        public void Update(Mesh mesh)
        {
            SourceMesh = mesh;
            NormalData.Update(Mesh.CreateEmpty());

            Updated?.Invoke();
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

                NormalData.Update(approximatedMesh);
            });
        }

        public IEnumerable<IDrawable> GetDrawables()
        {
            return NormalData.GetDrawables();
        }
    }
}
