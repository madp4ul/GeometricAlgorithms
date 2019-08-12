using GeometricAlgorithms.BusinessLogic.Model;
using GeometricAlgorithms.BusinessLogic.Model.KdTreeModels;
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
    public partial class KdTreeQueriesForm : Form
    {
        public KdTreeModel KdTree { get; set; }

        public KdTreeQueriesForm(KdTreeModel kdTree)
        {
            InitializeComponent();

            if (!DesignMode)
            {
                KdTree = kdTree;

                radiusQueryControl.QueryData = kdTree.RadiusQuery;
                nearestQueryControl.QueryData = kdTree.NearestQuery;
                kdTreeConfigurationControl.KdTree = kdTree;
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
            KdTree.RadiusQuery.HideAll();
            KdTree.NearestQuery.HideAll();
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
