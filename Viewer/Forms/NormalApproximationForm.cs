using GeometricAlgorithms.Viewer.Model;
using GeometricAlgorithms.Viewer.Model.NormalModels;
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
    public partial class NormalApproximationForm : Form
    {
        private readonly PointData PointData;
        private ApproximatedNormalData ApproximatedNormalData => PointData.ApproximatedNormalData;


        public NormalApproximationForm(PointData pointData)
        {
            InitializeComponent();

            if (!DesignMode)
            {
                PointData = pointData;
            }
        }

        private void NormalApproximationForm_Load(object sender, EventArgs e)
        {
            rbUseFaces.Enabled = ApproximatedNormalData.CanApproximateFromFaces;

            if (rbUseFaces.Enabled)
            {
                rbUseFaces.Checked = true;
            }
            else
            {
                rbUsePositions.Checked = true;
            }
        }

        private void BtnApproximateNormals_Click(object sender, EventArgs e)
        {
            if (rbUseFaces.Checked && ApproximatedNormalData.CanApproximateFromFaces)
            {
                ApproximatedNormalData.CalculateApproximationFromFaces();
            }
            else if (rbUsePositions.Checked)
            {
                throw new NotImplementedException();
            }
        }

        private void BtnUseApproximateNormals_Click(object sender, EventArgs e)
        {
            if (ApproximatedNormalData.NormalData.HasNormals)
            {
                PointData.Reset(ApproximatedNormalData.NormalData.Mesh);
            }
        }
    }
}
