using GeometricAlgorithms.Viewer.Forms;
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


        private FaceApproximationForm QueriesWindow = null;
        public void ApproximateFaces(System.Windows.Forms.IWin32Window owner)
        {
            if (QueriesWindow == null)
            {
                QueriesWindow = new FaceApproximationForm(PointData.KdTreeData.ApproximatedFaceData);
                QueriesWindow.FormClosed += (o, e) => QueriesWindow = null;
                QueriesWindow.Show(owner);
            }
            else
            {
                QueriesWindow.Focus();
            }
        }

        public bool EnableApproximateFaces()
        {
            return PointData.KdTreeData.ApproximatedFaceData.CanApproximate;
        }
    }
}
