using GeometricAlgorithms.ImplicitSurfaces;
using GeometricAlgorithms.MeshQuerying;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.BusinessLogic.Model.FaceModels
{
    public class ImplicitSurfaceModel : IUpdatable<ATree>
    {

        private ScalarProductSurface ScalarProductSurface;

        public IFiniteImplicitSurface ImplicitSurface => ScalarProductSurface;


        public bool CanApproximate { get; private set; }


        public ImplicitSurfaceModel()
        {
            _UsedNearestPointCount = 10;
        }

        private int _UsedNearestPointCount;
        public int UsedNearestPointCount
        {
            get => _UsedNearestPointCount;
            set
            {
                _UsedNearestPointCount = value;
                if (ImplicitSurface != null)
                {
                    ScalarProductSurface.UsedNearestPointCount = value;
                }
            }
        }

        public event Action Updated;

        public void Update(ATree tree)
        {
            CanApproximate = tree.Mesh.HasNormals;

            ScalarProductSurface = CanApproximate
                ? new ScalarProductSurface(tree, UsedNearestPointCount)
                : null;

            Updated?.Invoke();
        }
    }
}
