using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Tasks;
using GeometricAlgorithms.MeshQuerying;
using GeometricAlgorithms.NormalOrientation;

namespace GeometricAlgorithms.BusinessLogic.Model.NormalModels
{
    public class NormalOrientationModel : IUpdatable<ATree>
    {
        private readonly IFuncExecutor FuncExecutor;

        public ATree Tree { get; private set; }
        public bool CanCalculateOrientation => Tree != null && Tree.Mesh.HasNormals;
        public bool CanMirrorNormals => CanCalculateOrientation;

        private NormalOrientationFinder Finder;

        public event Action Updated;

        public NormalOrientationModel(IFuncExecutor funcExecutor)
        {
            FuncExecutor = funcExecutor;
            Finder = null;
        }

        public void CalculateIsNormalOrientationOutwards(Action<bool> receiveResult)
        {
            if (!CanCalculateOrientation)
            {
                throw new InvalidOperationException("update kdtree before calling");
            }

            FuncExecutor.Execute(progress => Finder.NormalsAreOrientedOutwards(progress))
                .GetResult(receiveResult);
        }

        public Mesh CreateMeshWithMirrorNormals()
        {
            if (Tree == null || !Tree.Mesh.HasNormals)
            {
                throw new InvalidOperationException();
            }

            var mirroredNormals = Tree.Mesh.UnitNormals
                .Select(n => -n)
                .ToList();

            return Tree.Mesh.Copy(replaceNormals: mirroredNormals);
        }

        public void Update(ATree tree)
        {
            Tree = tree;

            Finder = CanCalculateOrientation ? new NormalOrientationFinder(tree) : null;

            Updated?.Invoke();
        }
    }
}
