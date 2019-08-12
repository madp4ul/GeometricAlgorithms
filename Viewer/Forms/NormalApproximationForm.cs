using GeometricAlgorithms.BusinessLogic.Model;
using GeometricAlgorithms.BusinessLogic.Model.NormalModels;
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
    public partial class NormalApproximationForm : Form
    {
        private readonly Workspace Workspace;
        private NormalApproximationModel ApproximatedNormalData => Workspace.ApproximatedNormalData;


        public NormalApproximationForm(Workspace workspace)
        {
            InitializeComponent();

            if (!DesignMode)
            {
                Workspace = workspace;
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
                Workspace.PointData.Update(ApproximatedNormalData.NormalData.Mesh);
            }
        }
    }
}
