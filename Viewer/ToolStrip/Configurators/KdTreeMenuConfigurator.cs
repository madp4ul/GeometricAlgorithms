using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeometricAlgorithms.Viewer.Model;

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
            var treeMenuTools = new KdTreeMenuTools(Model.Workspace.PointData.KdTreeData);

            var menu = GetMenu(menuStrip.Items, "kdTreeToolStripMenuItem");

            MakeClickToggle(GetMenu(menu.DropDownItems, "showKdTreeToolStripMenuItem"), treeMenuTools.SetKdTreeVisibility);

            MakeClickAction(
                GetMenu(menu.DropDownItems, "openKdTreeSettingStripMenuItem"),
                () => treeMenuTools.OpenKdTreeQueriesWindow(MainWindow));
        }
    }
}
