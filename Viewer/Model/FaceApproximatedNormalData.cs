using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Viewer.Interfaces;

namespace GeometricAlgorithms.Viewer.Model
{
    public class FaceApproximatedNormalData : NormalData
    {
        public FaceApproximatedNormalData(IDrawableFactoryProvider drawableFactoryProvider) : base(drawableFactoryProvider)
        {
        }

        protected override IEnumerable<Vector3> SelectNormals(Mesh mesh)
        {
            return mesh.FaceApproximatedNormals;
        }
    }
}
