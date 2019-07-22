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
    public partial class KdTreeSettings : Form
    {
        public KdTreeData KdTreeData { get; set; }

        public KdTreeSettings(KdTreeData kdTreeData)
        {
            InitializeComponent();

            KdTreeData = kdTreeData;

            pointsPerTreeLeafNumericUpDown.Value = kdTreeData.Configuration.MaximumPointsPerLeaf;
        }

        private void BtnApplySettings_Click(object sender, EventArgs e)
        {
            KdTreeData.Configuration.MaximumPointsPerLeaf = (int)pointsPerTreeLeafNumericUpDown.Value;
            KdTreeData.Reset();
        }
    }
}
