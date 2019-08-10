using GeometricAlgorithms.Viewer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Viewer.ToolStrip.Tools
{
    class ApproximationTools
    {
        public readonly PointData PointData;

        public ApproximationTools(PointData pointData)
        {
            PointData = pointData ?? throw new ArgumentNullException(nameof(pointData));
        }

        public bool EnableApproximateNormalsFromFaces()
        {
            return PointData.FaceApproximatedNormalData.CanApproximate;
        }

        public void ApproximateNormalsFromFaces()
        {
            PointData.FaceApproximatedNormalData.CalculateApproximation();
        }

        public void ApproximateFaces()
        {
            PointData.KdTreeData.ApproximatedFaceData.CalculateApproximation();
        }

        public bool EnableApproximateFaces()
        {
            return PointData.KdTreeData.ApproximatedFaceData.CanApproximate;
        }
    }
}
