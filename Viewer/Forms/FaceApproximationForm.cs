using GeometricAlgorithms.Viewer.Model.FaceModels;
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
        private readonly ApproximatedFaceData ApproximatedFaceData;

        public FaceApproximationForm(ApproximatedFaceData approximatedFaceData)
        {
            InitializeComponent();

            if (!DesignMode)
            {
                ApproximatedFaceData = approximatedFaceData;
            }
        }

        private void FaceApproximationForm_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                ApproximatedFaceData.UsedNearestPointCount = (int)samplesNumericUpDown.Value;
                ApproximatedFaceData.DrawInnerFunctionValues = cbShowInsideSamples.Checked;
                ApproximatedFaceData.DrawOuterFunctionValues = cbShowOutsideSamples.Checked;

                SetTotalSamplesLabelText();
            }
        }

        private void BtnStartMarchingCubes_Click(object sender, EventArgs e)
        {
            ApproximatedFaceData.CalculateApproximation();
        }

        private void SamplesNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            ApproximatedFaceData.UsedNearestPointCount = (int)samplesNumericUpDown.Value;
            SetTotalSamplesLabelText();
        }

        private void SetTotalSamplesLabelText()
        {
            int samplesPerSide = (int)samplesNumericUpDown.Value;
            int totalSamples = samplesPerSide * samplesPerSide * samplesPerSide;

            totalSamplesLabel.Text = $"{totalSamples} samples in total";
        }

        private void CbShowInsideSamples_CheckedChanged(object sender, EventArgs e)
        {
            ApproximatedFaceData.DrawInnerFunctionValues = cbShowInsideSamples.Checked;
        }

        private void CbShowOutsideSamples_CheckedChanged(object sender, EventArgs e)
        {
            ApproximatedFaceData.DrawOuterFunctionValues = cbShowOutsideSamples.Checked;
        }

        private void FaceApproximationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            HideFunctionValues();
        }

        private void HideFunctionValues()
        {
            ApproximatedFaceData.DrawOuterFunctionValues = false;
            ApproximatedFaceData.DrawInnerFunctionValues = false;
        }
    }
}
