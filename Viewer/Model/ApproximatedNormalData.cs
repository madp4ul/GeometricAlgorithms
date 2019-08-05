using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Viewer.Interfaces;

namespace GeometricAlgorithms.Viewer.Model
{
    public class ApproximatedNormalData : NormalData
    {
        public ApproximatedNormalData(IDrawableFactoryProvider drawableFactoryProvider) : base(drawableFactoryProvider)
        {
        }

        protected override Vector3? SelectNormal(VertexNormal vertexNormal)
        {
            return vertexNormal.ApproximatedNormal;
        }
    }
}
