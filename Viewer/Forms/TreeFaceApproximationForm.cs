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
    public partial class TreeFaceApproximationForm : Form
    {
        private readonly Workspace Workspace;
        private TreeFaceApproximationModel TreeFaceApproximation => Workspace.TreeFaceApproximation;

        public TreeFaceApproximationForm(Workspace workspace)
        {
            InitializeComponent();

            if (!DesignMode)
            {
                Workspace = workspace;
                TreeFaceApproximation.Updated += UpdateApproximationOptions;
                workspace.ApproximatedFaces.Updated += SetEnableButtonUseApproximatedFaces;
                TreeFaceApproximation.EdgeTreeEnumeration.Updated += SetEnableButtonInspectTree;
            }
        }

        private void UpdateApproximationOptions()
        {
            SetEnableButtonUseApproximatedFaces();
            SetEnableButtonInspectTree();

            if (!TreeFaceApproximation.CanApproximate)
            {
                Close();
            }
        }

        private void TreeFaceApproximationForm_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                PutFormValuesIntoModel();
                UpdateApproximationOptions();
            }
        }

        private void TreeFaceApproximationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            HideFunctionValues();
            TreeFaceApproximation.Updated -= UpdateApproximationOptions;
            Workspace.ApproximatedFaces.Updated -= SetEnableButtonUseApproximatedFaces;
            TreeFaceApproximation.EdgeTreeEnumeration.Updated -= SetEnableButtonInspectTree;
        }

        private void SetEnableButtonUseApproximatedFaces()
        {
            btnUseFaces.Enabled = Workspace.ApproximatedFaces.HasFaces;
        }

        private void SetEnableButtonInspectTree()
        {
            btnInspectTree.Enabled = TreeFaceApproximation.EdgeTreeEnumeration.HasEnumerator;
        }

        private void PutFormValuesIntoModel()
        {
            TreeFaceApproximation.MaximumPointsPerLeaf = (int)numericMaxPointsPerLeaf.Value;

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

        private void NumericMaxPointsPerLeaf_ValueChanged(object sender, EventArgs e)
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
