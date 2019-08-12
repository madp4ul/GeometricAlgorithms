using GeometricAlgorithms.BusinessLogic.Model;
using GeometricAlgorithms.BusinessLogic.Model.FaceModels;
using GeometricAlgorithms.Viewer.Model;
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
    public partial class FaceApproximationForm : Form
    {
        private readonly Workspace Workspace;
        private FaceApproximationModel ApproximatedFaceData => Workspace.ApproximatedFaceData;

        public FaceApproximationForm(Workspace workspace)
        {
            InitializeComponent();

            if (!DesignMode)
            {
                Workspace = workspace;
            }
        }

        private void FaceApproximationForm_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                PutFormValuesIntoModel();

                SetTotalSamplesLabelText();
            }
        }

        private void FaceApproximationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            HideFunctionValues();
        }

        private void PutFormValuesIntoModel()
        {
            ApproximatedFaceData.UsedNearestPointCount = (int)neighboursPerValuenumericUpDown.Value;
            ApproximatedFaceData.SamplesOnLongestSideSide = (int)samplesNumericUpDown.Value;

            ApproximatedFaceData.DrawInnerFunctionValues = cbShowInsideSamples.Checked;
            ApproximatedFaceData.DrawOuterFunctionValues = cbShowOutsideSamples.Checked;
        }


        private void BtnStartMarchingCubes_Click(object sender, EventArgs e)
        {
            if (ApproximatedFaceData.CanApproximate)
            {
                PutFormValuesIntoModel();
                ApproximatedFaceData.CalculateApproximation();
            }
        }

        private void SamplesNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            PutFormValuesIntoModel();

            SetTotalSamplesLabelText();
        }

        private void SetTotalSamplesLabelText()
        {
            totalSamplesLabel.Text = $"{ApproximatedFaceData.TotalSamples} samples in total";
        }

        private void CbShowInsideSamples_CheckedChanged(object sender, EventArgs e)
        {
            PutFormValuesIntoModel();
        }

        private void CbShowOutsideSamples_CheckedChanged(object sender, EventArgs e)
        {
            PutFormValuesIntoModel();
        }

        private void HideFunctionValues()
        {
            ApproximatedFaceData.DrawOuterFunctionValues = false;
            ApproximatedFaceData.DrawInnerFunctionValues = false;
        }

        private void NeighboursPerValuenumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            PutFormValuesIntoModel();
        }

        private void BtnUseApproximatedFaces_Click(object sender, EventArgs e)
        {
            if (ApproximatedFaceData.FaceData.HasFaces)
            {
                Workspace.PointData.Update(ApproximatedFaceData.FaceData.Mesh);
            }
        }
    }
}
