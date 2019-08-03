using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeometricAlgorithms.Viewer.Model.KdTreeModels;

namespace GeometricAlgorithms.Viewer.Forms.KdTreeControls
{
    public partial class KdTreeConfigurationControl : UserControl
    {
        public KdTreeData KdTreeData { get; set; }

        public KdTreeConfigurationControl()
        {
            InitializeComponent();
        }

        private void ButtonApplyPointsPerLeaf_Click(object sender, EventArgs e)
        {
            KdTreeData.Configuration.MaximumPointsPerLeaf = (int)numericPointsPerLeaf.Value;
            KdTreeData.Reset();
        }

        private void KdTreeConfigurationControl_Load(object sender, EventArgs e)
        {
            //Take default values from model
            numericPointsPerLeaf.Value = KdTreeData.Configuration.MaximumPointsPerLeaf;
        }
    }
}
