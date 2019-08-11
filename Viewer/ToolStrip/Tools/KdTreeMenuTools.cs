using GeometricAlgorithms.Viewer.Forms;
using GeometricAlgorithms.Viewer.Model.KdTreeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.Viewer.ToolStrip.Tools
{
    class KdTreeMenuTools
    {
        public KdTreeData KdTreeData { get; set; }


        public KdTreeMenuTools(KdTreeData kdTreeData)
        {
            KdTreeData = kdTreeData ?? throw new ArgumentNullException(nameof(kdTreeData));
        }

        public void SetKdTreeVisibility(bool showKdTree)
        {
            KdTreeData.DrawKdTree = showKdTree;
        }

        private KdTreeQueriesForm QueriesWindow = null;
        public void OpenKdTreeQueriesWindow(System.Windows.Forms.IWin32Window owner)
        {
            if (QueriesWindow == null)
            {
                QueriesWindow = new KdTreeQueriesForm(KdTreeData);
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
