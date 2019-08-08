using GeometricAlgorithms.Domain;
using GeometricAlgorithms.NormalApproximation.FromFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.NormalApproximation
{
    public class NormalApproximatorFromFaces
    {
        public Vector3[] GetNormals(IReadOnlyList<Vector3> positions, IReadOnlyList<IFace> faces)
        {
            FaceNormalWeightSum[] faceNormalSums = new FaceNormalWeightSum[positions.Count];

            for (int i = 0; i < faceNormalSums.Length; i++)
            {
                faceNormalSums[i] = new FaceNormalWeightSum();
            }

            var faceNormalWeights = faces.Select(f => new FaceNormalWeightByArea(positions, f));

            foreach (var faceNormalWeight in faceNormalWeights)
            {
                foreach (var index in faceNormalWeight.Indices)
                {
                    faceNormalSums[index].Add(faceNormalWeight);
                }
            }

            return faceNormalSums.Select(s => s.GetAverage()).ToArray();
        }
    }
}
