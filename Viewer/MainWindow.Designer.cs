namespace GeometricAlgorithms.Viewer
{
    partial class MainWindow
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showPointCloudToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showNormalsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showFacesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showFacesAsWireframeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showNormalApproximationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showFaceApproximationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showFaceApproximationAsWireframeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kdTreeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showKdTreeBranchesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showKdTreeLeavesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openKdTreeSettingStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.approximationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.normalApproximationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.approximateFacesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.marchingCubesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alongOctreeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.normalOrientationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.automaticNormalOrientationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mirrorNormalsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.backgroundWorkerProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.backgroundWorkerStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.viewer = new GeometricAlgorithms.Viewer.GeometricAlgorithmViewer();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewerToolStripMenuItem,
            this.kdTreeToolStripMenuItem,
            this.approximationToolStripMenuItem,
            this.normalOrientationToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1017, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileToolStripMenuItem,
            this.saveFileToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openFileToolStripMenuItem.Text = "Open";
            // 
            // saveFileToolStripMenuItem
            // 
            this.saveFileToolStripMenuItem.Name = "saveFileToolStripMenuItem";
            this.saveFileToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.saveFileToolStripMenuItem.Text = "Save";
            // 
            // viewerToolStripMenuItem
            // 
            this.viewerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showPointCloudToolStripMenuItem,
            this.showNormalsToolStripMenuItem,
            this.showFacesToolStripMenuItem,
            this.showNormalApproximationToolStripMenuItem,
            this.showFaceApproximationToolStripMenuItem});
            this.viewerToolStripMenuItem.Name = "viewerToolStripMenuItem";
            this.viewerToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.viewerToolStripMenuItem.Text = "Viewer";
            // 
            // showPointCloudToolStripMenuItem
            // 
            this.showPointCloudToolStripMenuItem.Checked = true;
            this.showPointCloudToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showPointCloudToolStripMenuItem.Name = "showPointCloudToolStripMenuItem";
            this.showPointCloudToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.showPointCloudToolStripMenuItem.Text = "Show vertex locations";
            // 
            // showNormalsToolStripMenuItem
            // 
            this.showNormalsToolStripMenuItem.Checked = true;
            this.showNormalsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showNormalsToolStripMenuItem.Name = "showNormalsToolStripMenuItem";
            this.showNormalsToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.showNormalsToolStripMenuItem.Text = "Show normals";
            // 
            // showFacesToolStripMenuItem
            // 
            this.showFacesToolStripMenuItem.Checked = true;
            this.showFacesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showFacesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showFacesAsWireframeToolStripMenuItem});
            this.showFacesToolStripMenuItem.Name = "showFacesToolStripMenuItem";
            this.showFacesToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.showFacesToolStripMenuItem.Text = "Show faces";
            // 
            // showFacesAsWireframeToolStripMenuItem
            // 
            this.showFacesAsWireframeToolStripMenuItem.Checked = true;
            this.showFacesAsWireframeToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showFacesAsWireframeToolStripMenuItem.Name = "showFacesAsWireframeToolStripMenuItem";
            this.showFacesAsWireframeToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.showFacesAsWireframeToolStripMenuItem.Text = "Show as wireframe";
            // 
            // showNormalApproximationToolStripMenuItem
            // 
            this.showNormalApproximationToolStripMenuItem.Checked = true;
            this.showNormalApproximationToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showNormalApproximationToolStripMenuItem.Name = "showNormalApproximationToolStripMenuItem";
            this.showNormalApproximationToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.showNormalApproximationToolStripMenuItem.Text = "Show normal approximation";
            // 
            // showFaceApproximationToolStripMenuItem
            // 
            this.showFaceApproximationToolStripMenuItem.Checked = true;
            this.showFaceApproximationToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showFaceApproximationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showFaceApproximationAsWireframeToolStripMenuItem});
            this.showFaceApproximationToolStripMenuItem.Name = "showFaceApproximationToolStripMenuItem";
            this.showFaceApproximationToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.showFaceApproximationToolStripMenuItem.Text = "Show face approximation";
            // 
            // showFaceApproximationAsWireframeToolStripMenuItem
            // 
            this.showFaceApproximationAsWireframeToolStripMenuItem.Checked = true;
            this.showFaceApproximationAsWireframeToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showFaceApproximationAsWireframeToolStripMenuItem.Name = "showFaceApproximationAsWireframeToolStripMenuItem";
            this.showFaceApproximationAsWireframeToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.showFaceApproximationAsWireframeToolStripMenuItem.Text = "Show as wireframe";
            // 
            // kdTreeToolStripMenuItem
            // 
            this.kdTreeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showKdTreeBranchesToolStripMenuItem,
            this.showKdTreeLeavesToolStripMenuItem,
            this.openKdTreeSettingStripMenuItem});
            this.kdTreeToolStripMenuItem.Name = "kdTreeToolStripMenuItem";
            this.kdTreeToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.kdTreeToolStripMenuItem.Text = "Kd-Tree";
            // 
            // showKdTreeBranchesToolStripMenuItem
            // 
            this.showKdTreeBranchesToolStripMenuItem.Name = "showKdTreeBranchesToolStripMenuItem";
            this.showKdTreeBranchesToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.showKdTreeBranchesToolStripMenuItem.Text = "Show Kd-Tree branches";
            // 
            // showKdTreeLeavesToolStripMenuItem
            // 
            this.showKdTreeLeavesToolStripMenuItem.Name = "showKdTreeLeavesToolStripMenuItem";
            this.showKdTreeLeavesToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.showKdTreeLeavesToolStripMenuItem.Text = "Show Kd-Tree leaves";
            // 
            // openKdTreeSettingStripMenuItem
            // 
            this.openKdTreeSettingStripMenuItem.Name = "openKdTreeSettingStripMenuItem";
            this.openKdTreeSettingStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.openKdTreeSettingStripMenuItem.Text = "Query";
            // 
            // approximationToolStripMenuItem
            // 
            this.approximationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.normalApproximationToolStripMenuItem,
            this.approximateFacesToolStripMenuItem});
            this.approximationToolStripMenuItem.Name = "approximationToolStripMenuItem";
            this.approximationToolStripMenuItem.Size = new System.Drawing.Size(98, 20);
            this.approximationToolStripMenuItem.Text = "Approximation";
            // 
            // normalApproximationToolStripMenuItem
            // 
            this.normalApproximationToolStripMenuItem.Name = "normalApproximationToolStripMenuItem";
            this.normalApproximationToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.normalApproximationToolStripMenuItem.Text = "Normals";
            // 
            // approximateFacesToolStripMenuItem
            // 
            this.approximateFacesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.marchingCubesToolStripMenuItem,
            this.alongOctreeToolStripMenuItem});
            this.approximateFacesToolStripMenuItem.Name = "approximateFacesToolStripMenuItem";
            this.approximateFacesToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.approximateFacesToolStripMenuItem.Text = "Faces";
            // 
            // marchingCubesToolStripMenuItem
            // 
            this.marchingCubesToolStripMenuItem.Name = "marchingCubesToolStripMenuItem";
            this.marchingCubesToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.marchingCubesToolStripMenuItem.Text = "Marching Cubes";
            // 
            // alongOctreeToolStripMenuItem
            // 
            this.alongOctreeToolStripMenuItem.Name = "alongOctreeToolStripMenuItem";
            this.alongOctreeToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.alongOctreeToolStripMenuItem.Text = "Along Octree";
            // 
            // normalOrientationToolStripMenuItem
            // 
            this.normalOrientationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.automaticNormalOrientationToolStripMenuItem,
            this.mirrorNormalsToolStripMenuItem});
            this.normalOrientationToolStripMenuItem.Name = "normalOrientationToolStripMenuItem";
            this.normalOrientationToolStripMenuItem.Size = new System.Drawing.Size(122, 20);
            this.normalOrientationToolStripMenuItem.Text = "Normal Orientation";
            // 
            // automaticNormalOrientationToolStripMenuItem
            // 
            this.automaticNormalOrientationToolStripMenuItem.Checked = true;
            this.automaticNormalOrientationToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.automaticNormalOrientationToolStripMenuItem.Name = "automaticNormalOrientationToolStripMenuItem";
            this.automaticNormalOrientationToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.automaticNormalOrientationToolStripMenuItem.Text = "Automatic orientation";
            // 
            // mirrorNormalsToolStripMenuItem
            // 
            this.mirrorNormalsToolStripMenuItem.Name = "mirrorNormalsToolStripMenuItem";
            this.mirrorNormalsToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.mirrorNormalsToolStripMenuItem.Text = "Mirror normals";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backgroundWorkerProgressBar,
            this.backgroundWorkerStatusLabel,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 519);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1017, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // backgroundWorkerProgressBar
            // 
            this.backgroundWorkerProgressBar.Name = "backgroundWorkerProgressBar";
            this.backgroundWorkerProgressBar.Size = new System.Drawing.Size(100, 16);
            this.backgroundWorkerProgressBar.Step = 1;
            // 
            // backgroundWorkerStatusLabel
            // 
            this.backgroundWorkerStatusLabel.AutoSize = false;
            this.backgroundWorkerStatusLabel.Name = "backgroundWorkerStatusLabel";
            this.backgroundWorkerStatusLabel.Size = new System.Drawing.Size(250, 17);
            this.backgroundWorkerStatusLabel.Text = "Ready";
            // 
            // viewer
            // 
            this.viewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.viewer.Location = new System.Drawing.Point(0, 24);
            this.viewer.Name = "viewer";
            this.viewer.Size = new System.Drawing.Size(1017, 496);
            this.viewer.TabIndex = 0;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(66, 17);
            this.toolStripStatusLabel1.Text = "mesh info1";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1017, 541);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.viewer);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(700, 400);
            this.Name = "MainWindow";
            this.Text = "Point Viewer";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GeometricAlgorithmViewer viewer;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showPointCloudToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kdTreeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showKdTreeBranchesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openKdTreeSettingStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar backgroundWorkerProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel backgroundWorkerStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem showNormalsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showNormalApproximationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showFacesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showFacesAsWireframeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showFaceApproximationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showFaceApproximationAsWireframeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem approximationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem normalApproximationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem approximateFacesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem normalOrientationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem automaticNormalOrientationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mirrorNormalsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showKdTreeLeavesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem marchingCubesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem alongOctreeToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}

