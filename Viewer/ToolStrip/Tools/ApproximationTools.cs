using GeometricAlgorithms.BusinessLogic.Model;
using GeometricAlgorithms.Viewer.Forms;
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
            return PointData.ApproximatedNormalData.CanApproximateFromFaces;
        }

        private NormalApproximationForm NormalApproximationForm = null;
        public void OpenNormalApproximationForm(System.Windows.Forms.IWin32Window owner)
        {
            if (FaceApproximationForm == null)
            {
                NormalApproximationForm = new NormalApproximationForm(PointData);
                NormalApproximationForm.FormClosed += (o, e) => FaceApproximationForm = null;
                NormalApproximationForm.Show(owner);
            }
            else
            {
                FaceApproximationForm.Focus();
            }
        }


        private FaceApproximationForm FaceApproximationForm = null;
        public void OpenFaceApproximationForm(System.Windows.Forms.IWin32Window owner)
        {
            if (FaceApproximationForm == null)
            {
                FaceApproximationForm = new FaceApproximationForm(PointData);
                FaceApproximationForm.FormClosed += (o, e) => FaceApproximationForm = null;
                FaceApproximationForm.Show(owner);
            }
            else
            {
                FaceApproximationForm.Focus();
            }
        }

        public bool EnableApproximateFaces()
        {
            return PointData.KdTreeData.ApproximatedFaceData.CanApproximate;
        }
    }
}
