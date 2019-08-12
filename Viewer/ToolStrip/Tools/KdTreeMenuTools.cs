using GeometricAlgorithms.BusinessLogic.Model;
using GeometricAlgorithms.BusinessLogic.Model.KdTreeModels;
using GeometricAlgorithms.Viewer.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Viewer.ToolStrip.Tools
{
    class KdTreeMenuTools
    {
        public KdTreeModel KdTree { get; set; }


        public KdTreeMenuTools(KdTreeModel kdTree)
        {
            KdTree = kdTree ?? throw new ArgumentNullException(nameof(kdTree));
        }

        public void SetKdTreeVisibility(bool showKdTree)
        {
            KdTree.DrawKdTree = showKdTree;
        }

        private KdTreeQueriesForm QueriesWindow = null;
        public void OpenKdTreeQueriesWindow(System.Windows.Forms.IWin32Window owner)
        {
            if (QueriesWindow == null)
            {
                QueriesWindow = new KdTreeQueriesForm(KdTree);
                QueriesWindow.FormClosed += (o, e) => QueriesWindow = null;
                QueriesWindow.Show(owner);
            }
            else
            {
                QueriesWindow.Focus();
            }
        }
    }
}
