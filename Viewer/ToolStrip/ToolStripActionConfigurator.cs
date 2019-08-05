﻿using GeometricAlgorithms.Viewer.Model;
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

            GetItem(fileOptionItem.DropDownItems, "openFileToolStripMenuItem")
                .Click += (o, e) => fileMenuTools.OpenFile();

            GetItem(fileOptionItem.DropDownItems, "saveFileToolStripMenuItem")
                .Click += (o, e) => fileMenuTools.SaveToFile();
        }

        private void ConfigureViewerOptions(ToolStripMenuItem viewerOptionItem)
        {
            var viewerMenuTools = new ViewerMenuTools(Model.Workspace);

            GetItem(viewerOptionItem.DropDownItems, "showPointCloudToolStripMenuItem")
                .Click += ToggleClick(viewerMenuTools.SetPointCloudVisibility);

            var showNormals = GetItem(viewerOptionItem.DropDownItems, "showOriginalNormalsToolStripMenuItem");
            var showApproximatedNormals = GetItem(viewerOptionItem.DropDownItems, "showApproximatedNormalsToolStripMenuItem");

            viewerOptionItem.DropDownOpening += (o, e) =>
            {
                showNormals.Enabled = viewerMenuTools.EnableOptionSetNormalVisiblity();
                showApproximatedNormals.Enabled = viewerMenuTools.EnableOptionSetApproximatedNormalVisiblity();
            };

            showNormals.Click += ToggleClick(viewerMenuTools.SetNormalVisiblity);
            showApproximatedNormals.Click += ToggleClick(viewerMenuTools.SetApproximatedNormalVisiblity);
        }

        private void ConfigureKdTreeOptions(ToolStripMenuItem kdTreeOptionItem)
        {
            var treeMenuTools = new KdTreeMenuTools(Model.Workspace.PointData.KdTreeData);

            GetItem(kdTreeOptionItem.DropDownItems, "showKdTreeToolStripMenuItem")
                .Click += ToggleClick(treeMenuTools.SetKdTreeVisibility);

            GetItem(kdTreeOptionItem.DropDownItems, "openKdTreeSettingStripMenuItem")
                .Click += (o, e) =>
                {
                    treeMenuTools.OpenKdTreeQueriesWindow(MainWindow);
                };
        }

        private EventHandler ToggleClick(Action<bool> toToggle)
        {
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
