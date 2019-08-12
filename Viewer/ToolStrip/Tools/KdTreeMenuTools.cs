using GeometricAlgorithms.BusinessLogic.Model;
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
        public Workspace Workspace { get; set; }


        public KdTreeMenuTools(Workspace workspace)
        {
            Workspace = workspace ?? throw new ArgumentNullException(nameof(workspace));
        }

        public void SetKdTreeVisibility(bool showKdTree)
        {
            Workspace.KdTreeData.DrawKdTree = showKdTree;
        }

        private KdTreeQueriesForm QueriesWindow = null;
        public void OpenKdTreeQueriesWindow(System.Windows.Forms.IWin32Window owner)
        {
            if (QueriesWindow == null)
            {
                QueriesWindow = new KdTreeQueriesForm(Workspace);
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
