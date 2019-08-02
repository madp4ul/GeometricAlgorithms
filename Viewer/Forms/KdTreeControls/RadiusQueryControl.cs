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
using GeometricAlgorithms.Domain.Tasks;

namespace GeometricAlgorithms.Viewer.Forms.KdTreeControls
{
    public partial class RadiusQueryControl : UserControl
    {
        public KdTreeRadiusQueryData QueryData { get; set; }
        public GeometricAlgorithmViewer Viewer { get; set; }


        public RadiusQueryControl()
        {
            InitializeComponent();

            LiveUpdateTimer.Interval = 100;
            LiveUpdateTimer.Tick += LiveUpdateTimer_Tick;
            LiveUpdateTimer.Start();
        }

        private void LiveUpdateTimer_Tick(object sender, EventArgs e)
        {
            if (QueryData.QueryHasChangedSinceLastCalculation && !QueryData.IsCalculating)
            {
                QueryData.CalculateQueryResult();
            }
        }

        private void RadiusQueryControl_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                queryRadiusNumericUpDown.Value = (decimal)QueryData.Radius;
            }
        }

        private void BtnStartRadiusSearch_Click(object sender, EventArgs e)
        {
            QueryData.CalculateQueryResult();
        }

        private void CbShowQueryResult_CheckedChanged(object sender, EventArgs e)
        {
            QueryData.ShowQueryResult = cbShowQueryResult.Checked;
        }

        private void CbShowQueryCenter_CheckedChanged(object sender, EventArgs e)
        {
            QueryData.ShowQueryHelper = cbShowQueryCenter.Checked;
        }

        public void SetShowQuery(bool showQuery)
        {
            if (showQuery)
            {
                QueryData.ShowQueryHelper = cbShowQueryResult.Checked;
                QueryData.ShowQueryResult = cbShowQueryResult.Checked;
            }
            else
            {
                QueryData.ShowQueryHelper = false;
                QueryData.ShowQueryResult = false;
            }
        }

        private void QueryRadiusNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            QueryData.SetRadius((float)queryRadiusNumericUpDown.Value);
        }
    }
}
