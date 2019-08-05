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
            this.kdTreeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showKdTreeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openKdTreeSettingStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewer = new GeometricAlgorithms.Viewer.GeometricAlgorithmViewer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.backgroundWorkerProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.backgroundWorkerStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.showOriginalNormalsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showApproximatedNormalsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.menuStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewerToolStripMenuItem,
            this.kdTreeToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(159, 24);
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
            this.showOriginalNormalsToolStripMenuItem,
            this.showApproximatedNormalsToolStripMenuItem});
            this.viewerToolStripMenuItem.Name = "viewerToolStripMenuItem";
            this.viewerToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.viewerToolStripMenuItem.Text = "Viewer";
            // 
            // showPointCloudToolStripMenuItem
            // 
            this.showPointCloudToolStripMenuItem.Checked = true;
            this.showPointCloudToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showPointCloudToolStripMenuItem.Name = "showPointCloudToolStripMenuItem";
            this.showPointCloudToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.showPointCloudToolStripMenuItem.Text = "Show Vertex Locations";
            // 
            // kdTreeToolStripMenuItem
            // 
            this.kdTreeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showKdTreeToolStripMenuItem,
            this.openKdTreeSettingStripMenuItem});
            this.kdTreeToolStripMenuItem.Name = "kdTreeToolStripMenuItem";
            this.kdTreeToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.kdTreeToolStripMenuItem.Text = "Kd-Tree";
            // 
            // showKdTreeToolStripMenuItem
            // 
            this.showKdTreeToolStripMenuItem.Name = "showKdTreeToolStripMenuItem";
            this.showKdTreeToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.showKdTreeToolStripMenuItem.Text = "Show Kd-Tree";
            // 
            // openKdTreeSettingStripMenuItem
            // 
            this.openKdTreeSettingStripMenuItem.Name = "openKdTreeSettingStripMenuItem";
            this.openKdTreeSettingStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.openKdTreeSettingStripMenuItem.Text = "Query";
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
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backgroundWorkerProgressBar,
            this.backgroundWorkerStatusLabel});
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
            this.backgroundWorkerStatusLabel.Name = "backgroundWorkerStatusLabel";
            this.backgroundWorkerStatusLabel.Size = new System.Drawing.Size(39, 17);
            this.backgroundWorkerStatusLabel.Text = "Ready";
            // 
            // showOriginalNormalsToolStripMenuItem
            // 
            this.showOriginalNormalsToolStripMenuItem.Checked = true;
            this.showOriginalNormalsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showOriginalNormalsToolStripMenuItem.Name = "showOriginalNormalsToolStripMenuItem";
            this.showOriginalNormalsToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.showOriginalNormalsToolStripMenuItem.Text = "Show Original Normals";
            // 
            // showApproximatedNormalsToolStripMenuItem
            // 
            this.showApproximatedNormalsToolStripMenuItem.Checked = true;
            this.showApproximatedNormalsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showApproximatedNormalsToolStripMenuItem.Name = "showApproximatedNormalsToolStripMenuItem";
            this.showApproximatedNormalsToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.showApproximatedNormalsToolStripMenuItem.Text = "Show Approximated Normals";
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
        private System.Windows.Forms.ToolStripMenuItem showKdTreeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openKdTreeSettingStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar backgroundWorkerProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel backgroundWorkerStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem showOriginalNormalsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showApproximatedNormalsToolStripMenuItem;
    }
}

