using GeometricAlgorithms.Viewer.Model.KdTreeModels;
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
    public partial class KdTreeOptions : Form
    {
        public KdTreeData KdTreeData { get; set; }
        public GeometricAlgorithmViewer Viewer { get; set; }

        public KdTreeOptions(KdTreeData kdTreeData, GeometricAlgorithmViewer viewer)
        {
            InitializeComponent();

            KdTreeData = kdTreeData;
            Viewer = viewer;

            radiusQueryControl.QueryData = kdTreeData.RadiusQuerydata;
            radiusQueryControl.Viewer = viewer;

            //Take default values from model
            pointsPerTreeLeafNumericUpDown.Value = kdTreeData.Configuration.MaximumPointsPerLeaf;
        }

        private void KdTreeOptions_Load(object sender, EventArgs e)
        {
            UpdateActiveTabPage();
        }

        private void BtnApplySettings_Click(object sender, EventArgs e)
        {
            KdTreeData.Configuration.MaximumPointsPerLeaf = (int)pointsPerTreeLeafNumericUpDown.Value;
            KdTreeData.Reset();
        }

        private void KdTreeSettings_FormClosed(object sender, FormClosedEventArgs e)
        {
            HideAllKdTreeQueries();
        }

        private void HideAllKdTreeQueries()
        {
            KdTreeData.RadiusQuerydata.HideAll();
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateActiveTabPage();
        }

        private void UpdateActiveTabPage()
        {
            radiusQueryControl.SetShowQuery(radiusQueryControl.Visible);

        }
    }
}
