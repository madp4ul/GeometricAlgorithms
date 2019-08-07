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

        public ModelData Model { get; set; }

        public ToolStripActionConfigurator(MainWindow mainWindow, MenuStrip menuStrip, ModelData model)
        {
            MainWindow = mainWindow ?? throw new ArgumentNullException(nameof(mainWindow));
            MenuStrip = menuStrip ?? throw new ArgumentNullException(nameof(menuStrip));
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

            GetMenu(fileOptionItem.DropDownItems, "openFileToolStripMenuItem")
                .Click += (o, e) => fileMenuTools.OpenFile();

            GetMenu(fileOptionItem.DropDownItems, "saveFileToolStripMenuItem")
                .Click += (o, e) => fileMenuTools.SaveToFile();
        }

        private void ConfigureViewerOptions(ToolStripMenuItem viewerOptionItem)
        {
            var viewerMenuTools = new ViewerMenuTools(Model.Workspace);

            //Get Menu Items
            var showNormals = GetMenu(viewerOptionItem.DropDownItems, "showOriginalNormalsToolStripMenuItem");
            var showFaceApproximatedNormals = GetMenu(viewerOptionItem.DropDownItems, "showFaceApproximatedNormalsToolStripMenuItem");

            var showFaces = GetMenu(viewerOptionItem.DropDownItems, "showOriginalMeshToolStripMenuItem");
            var showFacesAsWireframe = GetMenu(showFaces.DropDownItems, "showOriginalFacesAsWireframeToolStripMenuItem");

            //Set enabled on open menu
            viewerOptionItem.DropDownOpening += (o, e) =>
            {
                showNormals.Enabled = viewerMenuTools.EnableOptionSetNormalVisiblity();
                showFaceApproximatedNormals.Enabled = viewerMenuTools.EnableOptionSetApproximatedNormalVisiblity();
                showFaces.Enabled = viewerMenuTools.EnableOptionSetOriginalFacesVisiblity();
            };

            //Configure action on click
            GetMenu(viewerOptionItem.DropDownItems, "showPointCloudToolStripMenuItem")
                       .Click += ToggleClick(viewerMenuTools.SetPointCloudVisibility);

            showNormals.Click += ToggleClick(viewerMenuTools.SetNormalVisiblity, showNormals.Checked);
            showFaceApproximatedNormals.Click += ToggleClick(
                viewerMenuTools.SetApproximatedNormalVisiblity,
                showFaceApproximatedNormals.Checked);

            showFaces.Click += ToggleClick(viewerMenuTools.SetOriginalFacesVisiblity, showFaces.Checked);
            showFacesAsWireframe.Click += ToggleClick(viewerMenuTools.SetDrawOriginalFacesAsWireframe, showFacesAsWireframe.Checked);
        }

        private void ConfigureKdTreeOptions(ToolStripMenuItem kdTreeOptionItem)
        {
            var treeMenuTools = new KdTreeMenuTools(Model.Workspace.PointData.KdTreeData);

            GetMenu(kdTreeOptionItem.DropDownItems, "showKdTreeToolStripMenuItem")
                .Click += ToggleClick(treeMenuTools.SetKdTreeVisibility);

            GetMenu(kdTreeOptionItem.DropDownItems, "openKdTreeSettingStripMenuItem")
                .Click += (o, e) =>
                {
                    treeMenuTools.OpenKdTreeQueriesWindow(MainWindow);
                };
        }

        private EventHandler ToggleClick(Action<bool> toToggle, bool? initialValue = null)
        {
            if (initialValue.HasValue)
            {
                toToggle(initialValue.Value);
            }

            return (o, e) =>
            {
                ToolStripMenuItem sender = (ToolStripMenuItem)o;
                sender.Checked = !sender.Checked;

                toToggle(sender.Checked);
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
