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
        private NormalApproximationModel ApproximatedNormalData => Workspace.ApproximatedNormals;


        public NormalApproximationForm(Workspace workspace)
        {
            InitializeComponent();

            if (!DesignMode)
            {
                Workspace = workspace;
                ApproximatedNormalData.Updated += UpdateApproximationOptions;
                ApproximatedNormalData.Normals.Updated += SetEnableButtonUseApproximatedNormals;
            }
        }

        private void SetEnableButtonUseApproximatedNormals()
        {
            btnUseApproximateNormals.Enabled = ApproximatedNormalData.Normals.HasNormals;
        }

        private void NormalApproximationForm_Load(object sender, EventArgs e)
        {
            UpdateApproximationOptions();
            SetEnableButtonUseApproximatedNormals();

            if (rbUseFaces.Enabled)
            {
                rbUseFaces.Checked = true;
            }
            else
            {
                rbUsePositions.Checked = true;
            }
        }

        private void NormalApproximationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ApproximatedNormalData.Updated -= UpdateApproximationOptions;
            ApproximatedNormalData.Normals.Updated -= SetEnableButtonUseApproximatedNormals;
        }

        private void UpdateApproximationOptions()
        {
            rbUseFaces.Enabled = ApproximatedNormalData.CanApproximateFromFaces;
            rbUseFaces.Checked = rbUseFaces.Checked && rbUseFaces.Enabled;

            SetEnableButtonStartApproximation();

            //If normal approximation not possible on current mesh
            if (ApproximatedNormalData.SourceMesh.Positions.Count == 0)
            {
                Close();
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
            if (ApproximatedNormalData.Normals.HasNormals)
            {
                Workspace.Update(ApproximatedNormalData.Normals.Mesh);
            }
        }

        private void SetEnableButtonStartApproximation()
        {
            btnApproximateNormals.Enabled = rbUseFaces.Checked ^ rbUsePositions.Checked;
        }

        private void RbUseFaces_CheckedChanged(object sender, EventArgs e)
        {
            SetEnableButtonStartApproximation();
        }

        private void RbUsePositions_CheckedChanged(object sender, EventArgs e)
        {
            SetEnableButtonStartApproximation();
        }
    }
}
