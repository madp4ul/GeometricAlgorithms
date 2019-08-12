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

        public readonly NormalModel Normals;

        public event Action Updated;

        public Mesh SourceMesh { get; private set; }

        public bool CanApproximateFromFaces => SourceMesh?.Faces != null;

        public NormalApproximationModel(IDrawableFactoryProvider drawableFactoryProvider, IFuncExecutor funcExecutor)
        {
            FuncExecutor = funcExecutor;

            Approximator = new NormalApproximatorFromFaces();
            Normals = new NormalModel(drawableFactoryProvider);
        }

        public void Update(Mesh mesh)
        {
            SourceMesh = mesh;
            Normals.Update(Mesh.CreateEmpty());

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

                Normals.Update(approximatedMesh);
            });
        }

        public IEnumerable<IDrawable> GetDrawables()
        {
            return Normals.GetDrawables();
        }
    }
}
