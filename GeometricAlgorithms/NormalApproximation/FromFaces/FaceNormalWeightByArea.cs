using GeometricAlgorithms.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.NormalApproximation.FromFaces
{
    class FaceNormalWeightByArea
    {
        public int[] Indices { get; private set; }
        public float Weight { get; private set; }
        public Vector3 UnitNormal { get; private set; }

        public FaceNormalWeightByArea(IReadOnlyList<Vector3> positions, IFace face)
        {
            Indices = face.ToArray();

            if (Indices.Length != 3)
            {
                throw new InvalidOperationException("The face does not have a unique normal");
            }

            CalculateValues(positions, face);
        }

        protected virtual void CalculateValues(IReadOnlyList<Vector3> positions, IFace face)
        {
            Vector3 planeDir1 = positions[Indices[0]] - positions[Indices[1]];
            Vector3 planeDir2 = positions[Indices[0]] - positions[Indices[2]];

            Vector3 normal = Vector3.Cross(planeDir1, planeDir2);

            float normalLength = normal.Length;

            Weight = normalLength / 2;
            UnitNormal = normal / normalLength;
        }
    }
}
