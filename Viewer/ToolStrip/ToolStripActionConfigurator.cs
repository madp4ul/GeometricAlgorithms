using GeometricAlgorithms.Viewer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeometricAlgorithms.Viewer.ToolStrip
{
    class ToolStripActionConfigurator
    {
        public MainWindow MainWindow { get; set; }
        public MenuStrip MenuStrip { get; set; }

        public DataModel Model { get; set; }

        public GeometricAlgorithmViewer Viewer { get; set; }

        public ToolStripActionConfigurator(MainWindow mainWindow, MenuStrip menuStrip, GeometricAlgorithmViewer viewer, DataModel model)
        {
            MainWindow = mainWindow ?? throw new ArgumentNullException(nameof(mainWindow));
            MenuStrip = menuStrip ?? throw new ArgumentNullException(nameof(menuStrip));
            Viewer = viewer ?? throw new ArgumentNullException(nameof(viewer));
            Model = model ?? throw new ArgumentNullException(nameof(model));
        }

        public void Configure()
        {
            ConfigureFileOptions(GetMenu(MenuStrip.Items, "fileToolStripMenuItem"));
            ConfigureViewerOptions(GetMenu(MenuStrip.Items, "viewerToolStripMenuItem"));
            ConfigureKdTreeOptions(GetMenu(MenuStrip.Items, "kdTreeToolStripMenuItem"));
        }

        private void ConfigureFileOptions(ToolStripMenuItem fileOptionItem)
        {
            var fileMenuTools = new FileMenuTools(Model);

            GetItem(fileOptionItem.DropDownItems, "openFileToolStripMenuItem")
                .Click += RefreshAfter((o, e) => fileMenuTools.OpenFile());

            GetItem(fileOptionItem.DropDownItems, "saveFileToolStripMenuItem")
                .Click += RefreshAfter((o, e) => fileMenuTools.SaveToFile());
        }

        private void ConfigureViewerOptions(ToolStripMenuItem viewerOptionItem)
        {
            var viewerMenuTools = new ViewerMenuTools(Model.Workspace);

            GetItem(viewerOptionItem.DropDownItems, "showPointCloudToolStripMenuItem")
                .Click += RefreshAfter((o, e) =>
                    {
                        ToolStripMenuItem sender = (ToolStripMenuItem)o;
                        sender.Checked = !sender.Checked;

                        viewerMenuTools.SetPointCloudVisibility(sender.Checked);
                    });
        }

        private void ConfigureKdTreeOptions(ToolStripMenuItem kdTreeOptionItem)
        {
            var treeMenuTools = new KdTreeMenuTools(Model.Workspace.PointData.KdTreeData);

            GetItem(kdTreeOptionItem.DropDownItems, "showKdTreeToolStripMenuItem")
                .Click += RefreshAfter((o, e) =>
                {
                    ToolStripMenuItem sender = (ToolStripMenuItem)o;
                    sender.Checked = !sender.Checked;

                    treeMenuTools.SetKdTreeVisibility(sender.Checked);
                });

            GetItem(kdTreeOptionItem.DropDownItems, "openKdTreeSettingStripMenuItem")
                .Click += RefreshAfter((o, e) =>
                {
                    treeMenuTools.OpenKdTreeSettings(MainWindow);
                });
        }

        private EventHandler RefreshAfter(Action<object, EventArgs> action)
        {
            return (o, e) =>
            {
                action(o, e);
                Viewer.Invalidate();
            };
        }

        private ToolStripItem GetItem(ToolStripItemCollection collection, string name)
        {
            return collection[name] ?? throw new NotImplementedException(
                "Menustrip item not found. Name probably changed in designer.");
        }

        private ToolStripMenuItem GetMenu(ToolStripItemCollection collection, string name)
        {
            var item = GetItem(collection, name);
            return item as ToolStripMenuItem ?? throw new NotImplementedException(
                "Item was found but it was no MenuItem. Something probably changed in designer.");
        }
    }
}
