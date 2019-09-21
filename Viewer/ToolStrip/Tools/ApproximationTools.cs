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
        public readonly Workspace Workspace;

        public ApproximationTools(Workspace workspace)
        {
            Workspace = workspace ?? throw new ArgumentNullException(nameof(workspace));
        }

        public bool EnableApproximateNormalsFromFaces()
        {
            return Workspace.ApproximatedNormals.CanApproximateFromFaces;
        }

        private NormalApproximationForm NormalApproximationForm = null;
        public void OpenNormalApproximationForm(System.Windows.Forms.IWin32Window owner)
        {
            if (NormalApproximationForm == null)
            {
                NormalApproximationForm = new NormalApproximationForm(Workspace);
                NormalApproximationForm.FormClosed += (o, e) => NormalApproximationForm = null;
                NormalApproximationForm.Show(owner);
            }
            else
            {
                NormalApproximationForm.Focus();
            }
        }


        private FaceApproximationForm FaceApproximationForm = null;
        public void OpenMarchingCubesApproximationForm(System.Windows.Forms.IWin32Window owner)
        {
            if (FaceApproximationForm == null)
            {
                FaceApproximationForm = new FaceApproximationForm(Workspace);
                FaceApproximationForm.FormClosed += (o, e) => FaceApproximationForm = null;
                FaceApproximationForm.Show(owner);
            }
            else
            {
                FaceApproximationForm.Focus();
            }
        }

        private OctreeRefinementApproximationForm TreeFaceApproximationForm = null;
        public void OpenTreeFaceApproximationForm(System.Windows.Forms.IWin32Window owner)
        {
            if (TreeFaceApproximationForm == null)
            {
                TreeFaceApproximationForm = new OctreeRefinementApproximationForm(Workspace);
                TreeFaceApproximationForm.FormClosed += (o, e) => TreeFaceApproximationForm = null;
                TreeFaceApproximationForm.Show(owner);
            }
            else
            {
                TreeFaceApproximationForm.Focus();
            }
        }

        public bool EnableApproximateFaces()
        {
            return Workspace.FaceApproximation.CanApproximate;
        }
    }
}
