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
    public class NormalOrientationModel : IUpdatable<KdTree>
    {
        private readonly IFuncExecutor FuncExecutor;

        public KdTree KdTree { get; private set; }
        public bool CanCalculateOrientation => KdTree != null && KdTree.Mesh.HasNormals;
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
            if (KdTree == null || !KdTree.Mesh.HasNormals)
            {
                throw new InvalidOperationException();
            }

            var mirroredNormals = KdTree.Mesh.UnitNormals
                .Select(n => -n)
                .ToList();

            return KdTree.Mesh.Copy(replaceNormals: mirroredNormals);
        }

        public void Update(KdTree kdTree)
        {
            KdTree = kdTree;

            Finder = CanCalculateOrientation ? new NormalOrientationFinder(kdTree) : null;

            Updated?.Invoke();
        }
    }
}
