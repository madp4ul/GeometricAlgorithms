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
    public partial class KdTreeQueries : Form
    {
        public KdTreeData KdTreeData { get; set; }

        public KdTreeQueries(KdTreeData kdTreeData)
        {
            InitializeComponent();

            if (!DesignMode)
            {
                KdTreeData = kdTreeData;

                radiusQueryControl.QueryData = kdTreeData.RadiusQuerydata;
                nearestQueryControl.QueryData = kdTreeData.NearestQuerydata;
                kdTreeConfigurationControl.KdTreeData = kdTreeData;
            }
        }

        private void KdTreeOptions_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                UpdateActiveTabPage();
            }
        }

        private void KdTreeSettings_FormClosed(object sender, FormClosedEventArgs e)
        {
            HideAllKdTreeQueries();
        }

        private void HideAllKdTreeQueries()
        {
            KdTreeData.RadiusQuerydata.HideAll();
            KdTreeData.NearestQuerydata.HideAll();
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateActiveTabPage();
        }

        private void UpdateActiveTabPage()
        {
            radiusQueryControl.SetShowQuery(radiusQueryControl.Visible);
            nearestQueryControl.SetShowQuery(nearestQueryControl.Visible);
        }
    }
}
