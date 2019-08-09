using GeometricAlgorithms.Domain;
using GeometricAlgorithms.Domain.Tasks;
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
        private const string ProgressDescription = "Calculating normals";

        public Vector3[] GetNormals(IReadOnlyList<Vector3> positions, IReadOnlyList<Triangle> faces, IProgressUpdater progressUpdater = null)
        {
            progressUpdater?.UpdateStatus(0, ProgressDescription);

            FaceNormalWeightSum[] faceNormalSums = new FaceNormalWeightSum[positions.Count];

            for (int i = 0; i < faceNormalSums.Length; i++)
            {
                faceNormalSums[i] = new FaceNormalWeightSum();
            }

            progressUpdater?.UpdateStatus(20, ProgressDescription);

            var faceNormalWeights = faces.Select(f => new FaceNormalWeightByArea(positions, f));

            foreach (var faceNormalWeight in faceNormalWeights)
            {
                foreach (var index in faceNormalWeight.Indices)
                {
                    faceNormalSums[index].Add(faceNormalWeight);
                }
            }

            progressUpdater?.UpdateStatus(50, ProgressDescription);

            var result = faceNormalSums.Select(s => s.GetAverage()).ToArray();

            progressUpdater?.UpdateStatus(100, ProgressDescription + " complete");

            return result;
        }
    }
}
