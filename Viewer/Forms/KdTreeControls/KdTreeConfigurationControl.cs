using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeometricAlgorithms.BusinessLogic.Model.KdTreeModels;

namespace GeometricAlgorithms.Viewer.Forms.KdTreeControls
{
    public partial class KdTreeConfigurationControl : UserControl
    {
        public KdTreeData KdTreeData { get; set; }

        public KdTreeConfigurationControl()
        {
            InitializeComponent();
        }

        private void KdTreeConfigurationControl_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                //Take default values from model
                numericPointsPerLeaf.Value = KdTreeData.Configuration.MaximumPointsPerLeaf;
            }
        }

        private void ButtonApplyPointsPerLeaf_Click(object sender, EventArgs e)
        {
            KdTreeData.Configuration.MaximumPointsPerLeaf = (int)numericPointsPerLeaf.Value;
            KdTreeData.Reset();
        }
    }
}
