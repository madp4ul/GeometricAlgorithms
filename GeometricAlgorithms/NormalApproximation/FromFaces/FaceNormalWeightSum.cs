using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.NormalApproximation.FromFaces
{
    class FaceNormalWeightSum
    {
        private List<FaceNormalWeightByArea> Sum = new List<FaceNormalWeightByArea>();

        public void Add(FaceNormalWeightByArea normal)
        {
            Sum.Add(normal);
        }

        public Vector3 GetAverage()
        {
            Vector3 averageDirection = Vector3.Zero;
            foreach (var elem in Sum)
            {
                averageDirection += (elem.Weight * elem.UnitNormal);
            }

            return averageDirection.Normalized();
        }
    }
}
