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
    public partial class NearestQueryControl : UserControl
    {
        public KdTreeNearestQueryData QueryData { get; set; }

        public NearestQueryControl()
        {
            InitializeComponent();
            AutoRefreshTimer.Enabled = cbAutoRefresh.Checked;
        }

        private void RadiusQueryControl_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                queryPointCountNumericUpDown.Value = (decimal)QueryData.PointCount;
            }
        }

        private void AutoRefreshTimer_Tick(object sender, EventArgs e)
        {
            if (!DesignMode
                && QueryData.CanQuery
                && QueryData.QueryHasChangedSinceLastCalculation
                && !QueryData.IsCalculating)
            {
                QueryData.CalculateQueryResult();
            }
        }


        private void CbAutoRefresh_CheckedChanged(object sender, EventArgs e)
        {
            AutoRefreshTimer.Enabled = cbAutoRefresh.Checked;
        }

        private void BtnStartNeaestSearch_Click(object sender, EventArgs e)
        {
            if (QueryData.CanQuery)
            {
                QueryData.CalculateQueryResult();
            }
        }

        private void CbShowQueryResult_CheckedChanged(object sender, EventArgs e)
        {
            QueryData.QueryResult.Show = cbShowQueryResult.Checked;
        }

        private void CbShowQueryCenter_CheckedChanged(object sender, EventArgs e)
        {
            QueryData.QueryCenterPoint.Show = cbShowQueryCenter.Checked;
        }

        public void SetShowQuery(bool showQuery)
        {
            if (showQuery)
            {
                QueryData.QueryCenterPoint.Show = cbShowQueryResult.Checked;
                QueryData.QueryResult.Show = cbShowQueryResult.Checked;
            }
            else
            {
                QueryData.QueryCenterPoint.Show = false;
                QueryData.QueryResult.Show = false;
            }
        }

        private void QueryPointCountNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            QueryData.SetPointCount((int)queryPointCountNumericUpDown.Value);
        }
    }
}
