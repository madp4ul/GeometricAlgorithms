using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeometricAlgorithms.Viewer.Model;
using GeometricAlgorithms.Viewer.ToolStrip.Tools;

namespace GeometricAlgorithms.Viewer.ToolStrip.Configurators
{
    class KdTreeMenuConfigurator : MenuConfigurator
    {
        private readonly MainWindow MainWindow;

        public KdTreeMenuConfigurator(ModelData model, MainWindow mainWindow) : base(model)
        {
            MainWindow = mainWindow;
        }

        public override void Configure(MenuStrip menuStrip)
        {
            var treeMenuTools = new KdTreeMenuTools(Model.Workspace.KdTree);

            var menu = GetMenu(menuStrip.Items, "kdTreeToolStripMenuItem");

            MakeClickToggle(GetMenu(menu.DropDownItems, "showKdTreeBranchesToolStripMenuItem"), treeMenuTools.SetShowKdTreeBranches);
            MakeClickToggle(GetMenu(menu.DropDownItems, "showKdTreeLeavesToolStripMenuItem"), treeMenuTools.SetShowKdTreeLeaves);

            MakeClickAction(
                GetMenu(menu.DropDownItems, "openKdTreeSettingStripMenuItem"),
                () => treeMenuTools.OpenKdTreeQueriesWindow(MainWindow));
        }
    }
}
