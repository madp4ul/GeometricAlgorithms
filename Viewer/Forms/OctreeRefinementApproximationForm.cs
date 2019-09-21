using GeometricAlgorithms.BusinessLogic.Model;
using GeometricAlgorithms.BusinessLogic.Model.FaceModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeometricAlgorithms.Viewer.Forms
{
    public partial class OctreeRefinementApproximationForm : Form
    {
        private readonly Workspace Workspace;
        private TreeFaceApproximationModel2 TreeFaceApproximation => Workspace.TreeFaceApproximation2;

        public OctreeRefinementApproximationForm(Workspace workspace)
        {
            InitializeComponent();

            if (!DesignMode)
            {
                Workspace = workspace;
                TreeFaceApproximation.Updated += UpdateApproximationOptions;
                workspace.ApproximatedFaces.Updated += SetEnableButtonUseApproximatedFaces;
            }
        }

        private void UpdateApproximationOptions()
        {
            SetEnableButtonUseApproximatedFaces();
            SetLabelSamplesComputed();

            if (!TreeFaceApproximation.CanApproximate)
            {
                Close();
            }
        }

        private void OctreeRefinementApproximationForm_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                PutFormValuesIntoModel();
                UpdateApproximationOptions();
            }
        }

        private void OctreeRefinementApproximationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            HideFunctionValues();
            TreeFaceApproximation.Updated -= UpdateApproximationOptions;
            Workspace.ApproximatedFaces.Updated -= SetEnableButtonUseApproximatedFaces;
        }

        private void SetLabelSamplesComputed()
        {
            int computedSamples = TreeFaceApproximation.EdgeTree.ImplicitSurfaceProvider.FunctionValueCount;

            lbSamplesComputed.Text = $"{computedSamples} samples computed";
        }

        private void SetEnableButtonUseApproximatedFaces()
        {
            btnUseFaces.Enabled = Workspace.ApproximatedFaces.HasFaces;
        }

        private void PutFormValuesIntoModel()
        {
            TreeFaceApproximation.SampleLimit = (int)numericUpDownSampleLimit.Value;

            TreeFaceApproximation.DrawInnerFunctionValues = cbShowInsideSamples.Checked;
            TreeFaceApproximation.DrawOuterFunctionValues = cbShowOutsideSamples.Checked;
        }

        private void BtnStartApproximation_Click(object sender, EventArgs e)
        {
            if (TreeFaceApproximation.CanApproximate)
            {
                PutFormValuesIntoModel();
                TreeFaceApproximation.CalculateApproximation();
            }
        }

        private void BtnUseFaces_Click(object sender, EventArgs e)
        {
            if (Workspace.ApproximatedFaces.HasFaces)
            {
                Workspace.Update(Workspace.ApproximatedFaces.Mesh);
            }
        }

        private void NumericUpDownSampleLimit_ValueChanged(object sender, EventArgs e)
        {
            PutFormValuesIntoModel();
        }

        private void HideFunctionValues()
        {
            TreeFaceApproximation.DrawOuterFunctionValues = false;
            TreeFaceApproximation.DrawInnerFunctionValues = false;
        }

        private void CbShowInsideSamples_CheckedChanged(object sender, EventArgs e)
        {
            PutFormValuesIntoModel();
        }

        private void CbShowOutsideSamples_CheckedChanged(object sender, EventArgs e)
        {
            PutFormValuesIntoModel();
        }
    }
}
