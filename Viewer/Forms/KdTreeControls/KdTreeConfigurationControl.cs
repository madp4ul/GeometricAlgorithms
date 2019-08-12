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
using GeometricAlgorithms.MeshQuerying;
using GeometricAlgorithms.BusinessLogic.Model;

namespace GeometricAlgorithms.Viewer.Forms.KdTreeControls
{
    public partial class KdTreeConfigurationControl : UserControl
    {
        public Workspace Workspace { get; set; }

        public KdTreeConfigurationControl()
        {
            InitializeComponent();
        }

        private void KdTreeConfigurationControl_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                //Take default values from model
                numericPointsPerLeaf.Value = Workspace.KdTreeData.Configuration.MaximumPointsPerLeaf;
            }
        }

        private void ButtonApplyPointsPerLeaf_Click(object sender, EventArgs e)
        {
            var config = new KdTreeConfiguration
            {
                MaximumPointsPerLeaf = (int)numericPointsPerLeaf.Value
            };

            Workspace.KdTreeData.Update(config);
        }
    }
}
